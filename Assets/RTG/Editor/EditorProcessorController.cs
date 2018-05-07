
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTG.Processors
{
	public sealed class EditorProcessorController : UnityEditor.AssetModificationProcessor
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
}