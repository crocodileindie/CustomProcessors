using RTG.CustomProcessors;
using RTG.CustomProcessors.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace RTG.Processors
{
	public class EditorProcessorController : UnityEditor.AssetModificationProcessor
	{
		[InitializeOnLoadMethod]
		public static void InitializeOnLoad()
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode)
				return;
			EditorSceneManager.sceneSaving += EditorSceneManagerOnSceneSaving;
			Init();
		}

		public static void ProcessComponent(Component component)
		{
			if (component != null)
				Processors.Process(component);
		}

		public static void ProcessGameObject(GameObject gameObject)
		{
			var components = gameObject.GetComponentsInChildren(typeof(Component), true);
			foreach (var component in components)
			{
				ProcessComponent(component);
			}
		}

		public static void ProcessScriptableObject(ScriptableObject scriptableObject)
		{
			if (scriptableObject != null)
				Processors.Process(scriptableObject);
		}

		public static void ProcessScene(Scene scene)
		{
			for (int i = 0; i < scene.rootCount; i++)
			{
				var root = scene.GetRootGameObjects()[i];
				ProcessGameObject(root);
			}
		}

		public static void ProcessLoadedScenes()
		{
			for (int i = 0; i < EditorSceneManager.loadedSceneCount; i++)
			{
				Scene scene = EditorSceneManager.GetSceneAt(i);
				ProcessScene(scene);
			}
		}

		private static string[] OnWillSaveAssets(string[] paths)
		{
			foreach (var path in paths)
			{
				if (Path.GetExtension(path).ToLower() == ".prefab")
				{
					ProcessGameObject(AssetDatabase.LoadAssetAtPath<GameObject>(path));
				}
				if (Path.GetExtension(path) == ".asset")
				{
					ProcessScriptableObject(AssetDatabase.LoadAssetAtPath<ScriptableObject>(path));
				}
			}
			return paths;
		}

		/// <summary>
		/// when saving scene parses all objects for components and ...
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="path"></param>
		private static void EditorSceneManagerOnSceneSaving(Scene scene, string path)
		{
			ProcessScene(scene);
		}

		public static void Init()
		{
			Processors.Init();
		}
	}

	public static class Processors
	{
		#region DATA

		private static Dictionary<Type, List<IProcessor>> s_Processors = new Dictionary<Type, List<IProcessor>>();
		private static Dictionary<Type, Type> s_TypeToProcessorTypeDictionary = new Dictionary<Type, Type>();
		private static Dictionary<Type, Type> s_AttributeTypeToProcessorTypeDictionary = new Dictionary<Type, Type>();

		#endregion DATA

		#region INTERFACE

		public static void Init()
		{
			s_Processors.Clear();

			//TODO potential params:
			// don't process if prefab instance

			//TODO save SOs

			GenerateProcessors();
		}

		public static void Process(UnityEngine.Object obj)
		{
			List<IProcessor> processors = null;
			if (s_Processors.TryGetValue(obj.GetType(), out processors))
			{
				SerializedObject serializedObject = new SerializedObject(obj);
				foreach (var processor in processors)
				{
					processor.Process(obj);
				}
				serializedObject.ApplyModifiedPropertiesWithoutUndo();
			}
		}

		#endregion INTERFACE

		#region INTERNAL

		private static void AddPropertyProcessor(Type classType, FieldInfo fieldInfo, Attribute attribute)
		{
			List<IProcessor> processors = null;
			if (!s_Processors.TryGetValue(classType, out processors))
			{
				processors = new List<IProcessor>();
				s_Processors[classType] = processors;
			}
			//generate processor
			processors.Add((PropertyProcessor)Activator.CreateInstance(s_AttributeTypeToProcessorTypeDictionary[attribute.GetType()], new object[] { fieldInfo, attribute
			}));
		}

		/// <summary>
		/// Creates a dictionary of key: type value:  ( contains lists of fields with processor attributes )
		/// </summary>
		private static void GenerateProcessors()
		{
			BuildProcessorTypeForTypeDictionaries();

			//iterate over all fields
			var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

			//check all types
			var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly a) => a.GetTypes());
			var typesThatCanBeProcessed = s_AttributeTypeToProcessorTypeDictionary.Keys.ToArray();

			//get only serializable types
			foreach (var type in from x in types where x.IsSerializable || x.IsSubclassOf(typeof(Component)) || x.IsSubclassOf(typeof(ScriptableObject)) select x)
			{
				//check class
				Type processorType;
				if (s_TypeToProcessorTypeDictionary.TryGetValue(type, out processorType))
				{
					AddComponentProcessor(type);
				}
				else
				{
					var t = typesThatCanBeProcessed.Intersect(type.GetCustomAttributes(true).Select(o => o.GetType())).ToArray();
					foreach (var attributeType in t)
					{
						AddComponentProcessor(type, attributeType);
					}
				}

				//check all fields
				//get only serializable fields
				var fields = from x in type.GetFields(flags) where (x.IsPublic || x.GetCustomAttributes(typeof(SerializeField), false).Length > 0) select x;
				foreach (var fieldInfo in fields)
				{
					var t = typesThatCanBeProcessed.Intersect(fieldInfo.GetCustomAttributes(false).Select(o => o.GetType())).ToArray();

					Assert.IsTrue(t.Length < 2); //TODO need to handle this case

					if (t.Length > 0)
					{
						var attribute = fieldInfo.GetCustomAttributes(t[0], false)[0];

						AddPropertyProcessor(type, fieldInfo, (Attribute)attribute);
					}
				}
			}
		}

		private static void AddComponentProcessor(Type type)
		{
			List<IProcessor> processors = null;
			if (!s_Processors.TryGetValue(type, out processors))
			{
				processors = new List<IProcessor>();
				s_Processors[type] = processors;
			}
			//generate procesor
			processors.Add((ComponentProcessor)Activator.CreateInstance(s_TypeToProcessorTypeDictionary[type]));
		}

		private static void AddComponentProcessor(Type type, Type attributeType)
		{
			List<IProcessor> processors = null;
			if (!s_Processors.TryGetValue(type, out processors))
			{
				processors = new List<IProcessor>();
				s_Processors[type] = processors;
			}
			//generate processor
			processors.Add((ComponentProcessor)Activator.CreateInstance(s_AttributeTypeToProcessorTypeDictionary[attributeType]));
		}

		private static void BuildProcessorTypeForTypeDictionaries()
		{
			s_TypeToProcessorTypeDictionary.Clear();
			s_AttributeTypeToProcessorTypeDictionary.Clear();

			var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly a) => a.GetTypes());

			//get list of PropertyProcessor types
			// (get list of all types that derive from PropertyProcessor)
			var processors = from x in types
							 where x.GetInterfaces().Any(t => t.Equals(typeof(IProcessor)))
							 select x;

			//generate dictionary of attribute->PropertyProcessor
			// (get CustomProperyAttribute on ProperyProcessor)
			// (make sure its type is valid)
			foreach (var processor in processors)
			{
				//look for CustomPropertyAttribute
				foreach (var customProcessorAttribute in processor.GetCustomAttributes(typeof(CustomProcessorAttribute), false))
				{
					var a = customProcessorAttribute as CustomProcessorAttribute;
					if (a.Type.IsSubclassOf(typeof(UnityEngine.PropertyAttribute)))
						s_AttributeTypeToProcessorTypeDictionary.Add(a.Type, processor);
					else
						s_TypeToProcessorTypeDictionary.Add(a.Type, processor);
					//TODO
					//add warning if already defined
				}
			}
		}

		#endregion INTERNAL
	}
}