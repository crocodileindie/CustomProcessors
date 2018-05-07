using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace RTG.Test
{


	public class GetComponentTest : IPrebuildSetup
	{
		public void Setup()
		{
			EditorSceneManager.OpenScene("assets/Test/Editor/CustomProcessorTests/test.unity");
			var grandParent = GameObject.Find("GrandParent");
			Assert.IsTrue(grandParent != null);
			foreach (var t in grandParent.GetComponentsInChildren<ProcessorTestComponent>())
			{
				t.Clear();
			}
			Assert.IsTrue(EditorSceneManager.SaveScene(grandParent.scene));
		}

		private static GameObject Find(string name)
		{
			for (int sceneIndex = 0; sceneIndex < EditorSceneManager.loadedSceneCount; sceneIndex++)
			{
				var scene = EditorSceneManager.GetSceneAt(sceneIndex);
				for (int i = 0; i < scene.rootCount; i++)
				{
					var root = scene.GetRootGameObjects()[i];
					var xForms = root.GetComponentsInChildren<Transform>(true);
					foreach (var transform in xForms)
					{
						if (transform.name == name)
							return transform.gameObject;
					}
				}
			}
			return null;
		}

		[Test] public void GrandParent_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("GrandParent"), true); }
		[Test] public void GrandParent_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("GrandParent"), true); }
		[Test] public void GrandParent_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("GrandParent"), true); }
		[Test] public void GrandParent_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("GrandParent"), true); }

		[Test] public void Parent_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Parent")); }
		[Test] public void Parent_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Parent")); }
		[Test] public void Parent_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Parent")); }
		[Test] public void Parent_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Parent")); }
		[Test] public void Parent_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Parent")); }
		[Test] public void Parent_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Parent")); }
		[Test] public void Parent_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Parent"), true); }
		[Test] public void Parent_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Parent"), true); }
		[Test] public void Parent_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Parent"), true); }

		[Test] public void Parent_2_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Parent_2"), true); }
		[Test] public void Parent_2_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Parent_2"), true); }
		[Test] public void Parent_2_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Parent_2"), true); }
		[Test] public void Parent_2_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Parent_2"), true); }

		[Test] public void Cube_A_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_A"), true); }
		[Test] public void Cube_A_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_A"), true); }
		[Test] public void Cube_A_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_A"), true); }
		[Test] public void Cube_A_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_A"), true); }

		[Test] public void Cube_B_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_B"), true); }
		[Test] public void Cube_B_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_B"), true); }
		[Test] public void Cube_B_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_B"), true); }
		[Test] public void Cube_B_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_B"), true); }

		[Test] public void Cube_C_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_C"), true); }
		[Test] public void Cube_C_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_C"), true); }
		[Test] public void Cube_C_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_C"), true); }
		[Test] public void Cube_C_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_C"), true); }
	}
	public class GetComponentPrefabTest : IPrebuildSetup
	{
		private const string PrefabPath = "Assets/Test/Editor/CustomProcessorTests/GrandParent.prefab";
		private GameObject prefab;
		public void Setup()
		{
			var grandParent = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
			Assert.IsTrue(grandParent != null);
			foreach (var t in grandParent.GetComponentsInChildren<ProcessorTestComponent>())
			{
				t.Clear();
			}
			EditorUtility.SetDirty(grandParent);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		private GameObject Find(string name)
		{
			prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
			Assert.IsNotNull(prefab);
			var xForms = prefab.GetComponentsInChildren<Transform>(true);
			foreach (var transform in xForms)
			{
				if (transform.name == name)
					return transform.gameObject;
			}

			return null;
		}


		[Test] public void GrandParent_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("GrandParent")); }
		[Test] public void GrandParent_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("GrandParent"), true); }
		[Test] public void GrandParent_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("GrandParent"), true); }
		[Test] public void GrandParent_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("GrandParent")); }
		[Test] public void GrandParent_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("GrandParent"), true); }
		[Test] public void GrandParent_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("GrandParent"), true); }

		[Test] public void Parent_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Parent")); }
		[Test] public void Parent_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Parent")); }
		[Test] public void Parent_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Parent")); }
		[Test] public void Parent_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Parent")); }
		[Test] public void Parent_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Parent")); }
		[Test] public void Parent_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Parent")); }
		[Test] public void Parent_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Parent"), true); }
		[Test] public void Parent_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Parent"), true); }
		[Test] public void Parent_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Parent")); }
		[Test] public void Parent_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Parent"), true); }

		[Test] public void Parent_2_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Parent_2")); }
		[Test] public void Parent_2_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Parent_2"), true); }
		[Test] public void Parent_2_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Parent_2"), true); }
		[Test] public void Parent_2_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Parent_2")); }
		[Test] public void Parent_2_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Parent_2"), true); }
		[Test] public void Parent_2_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Parent_2"), true); }

		[Test] public void Cube_A_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Cube_A")); }
		[Test] public void Cube_A_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_A"), true); }
		[Test] public void Cube_A_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_A"), true); }
		[Test] public void Cube_A_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_A")); }
		[Test] public void Cube_A_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_A"), true); }
		[Test] public void Cube_A_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_A"), true); }

		[Test] public void Cube_B_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Cube_B")); }
		[Test] public void Cube_B_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_B"), true); }
		[Test] public void Cube_B_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_B"), true); }
		[Test] public void Cube_B_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_B")); }
		[Test] public void Cube_B_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_B"), true); }
		[Test] public void Cube_B_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_B"), true); }

		[Test] public void Cube_C_MyPublicComponent() { GetComponentUtilities.TestMyPublicComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPublicComponent() { GetComponentUtilities.TestMyChildPublicComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyParentPublicComponent() { GetComponentUtilities.TestMyParentPublicComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyPrivateComponent() { GetComponentUtilities.TestMyPrivateComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPrivateComponent() { GetComponentUtilities.TestMyChildPrivateComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyParentPrivateComponent() { GetComponentUtilities.TestMyParentPrivateComponent(Find("Cube_C")); }
		[Test] public void Cube_C_MyPublicComponentsArray() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPublicComponentsArray() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyParentPublicComponentsArray() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyPublicComponentsArrayInactive() { GetComponentUtilities.TestMyPublicComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPublicComponentsArrayInactive() { GetComponentUtilities.TestMyChildPublicComponentsArray(Find("Cube_C"), true); }
		[Test] public void Cube_C_MyParentPublicComponentsArrayInactive() { GetComponentUtilities.TestMyParentPublicComponentsArray(Find("Cube_C"), true); }
		[Test] public void Cube_C_MyPrivateComponentsArray() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPrivateComponentsArray() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyParentPrivateComponentsArray() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyPrivateComponentsArray(Find("Cube_C")); }
		[Test] public void Cube_C_MyChildPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyChildPrivateComponentsArray(Find("Cube_C"), true); }
		[Test] public void Cube_C_MyParentPrivateComponentsArrayInactive() { GetComponentUtilities.TestMyParentPrivateComponentsArray(Find("Cube_C"), true); }
	}

	public class GetComponentUtilities
	{
		public static void TestMyPublicComponent(GameObject obj)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var result = test.GetComponent<Renderer>();
			if (result == null)
				Assert.IsNull(test.myPublicComponent);
			else
				Assert.AreEqual(result, test.myPublicComponent);
		}

		public static void TestMyChildPublicComponent(GameObject obj)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var result = test.GetComponentInChildren<Renderer>();
			if (result == null)
				Assert.IsNull(test.childPublicComponent);
			else
				Assert.AreEqual(result, test.childPublicComponent);
		}

		public static void TestMyParentPublicComponent(GameObject obj)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var result = test.GetComponentInParent<Renderer>();
			if (result == null)
				Assert.IsNull(test.parentPublicComponent);
			else
				Assert.AreEqual(result, test.parentPublicComponent);
		}

		public static void TestMyPrivateComponent(GameObject obj)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			SerializedObject so = new SerializedObject(test);
			var prop = so.FindProperty("myPrivateComponent");
			var result = test.GetComponent<Renderer>();
			if (result == null)
				Assert.IsNull(prop.objectReferenceValue);
			else
				Assert.AreEqual(result, prop.objectReferenceValue);
		}

		public static void TestMyChildPrivateComponent(GameObject obj)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			SerializedObject so = new SerializedObject(test);
			var prop = so.FindProperty("childPrivateComponent");
			var result = test.GetComponentInChildren<Renderer>();
			if (result == null)
				Assert.IsNull(prop.objectReferenceValue);
			else
				Assert.AreEqual(result, prop.objectReferenceValue);
		}

		public static void TestMyParentPrivateComponent(GameObject obj)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			SerializedObject so = new SerializedObject(test);
			var prop = so.FindProperty("parentPrivateComponent");
			var result = test.GetComponentInParent<Renderer>();
			if (result == null)
				Assert.IsNull(prop.objectReferenceValue);
			Assert.AreEqual(result, prop.objectReferenceValue);
		}

		public static void TestMyPublicComponentsArray(GameObject obj)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var list = test.GetComponents<Renderer>();
			if (list == null)
			{
				Assert.IsNull(test.myPublicComponentsArray);
				Assert.IsNull(test.myPublicComponentsList);
			}
			IList arr = test.myPublicComponentsArray;
			IList lis = test.myPublicComponentsList;

			if (list == null)
			{
				Assert.IsNull(arr);
				Assert.IsNull(lis);
			}
			else
			{
				Assert.IsTrue(arr.Count == list.Length);
				Assert.IsTrue(lis.Count == list.Length);
				for (var index = 0; index < list.Length; index++)
				{
					Assert.AreEqual(list[index], arr[index]);
					Assert.AreEqual(list[index], lis[index]);
				}
			}
		}

		public static void TestMyChildPublicComponentsArray(GameObject obj, bool inactive = false)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var list = test.GetComponentsInChildren<Renderer>(inactive);

			IList arr = inactive ? test.childPublicComponentsArrayInactive : test.childPublicComponentsArray;
			IList lis = inactive ? test.childPublicComponentsListInactive : test.childPublicComponentsList;

			if (list == null)
			{
				Assert.IsNull(arr);
				Assert.IsNull(lis);
			}
			else
			{
				Assert.IsTrue(arr.Count == list.Length);
				Assert.IsTrue(lis.Count == list.Length);
				for (var index = 0; index < list.Length; index++)
				{
					Assert.AreEqual(list[index], arr[index]);
					Assert.AreEqual(list[index], lis[index]);
				}
			}
		}

		public static void TestMyParentPublicComponentsArray(GameObject obj, bool inactive = false)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var list = test.GetComponentsInParent<Renderer>(inactive);

			IList arr = inactive ? test.parentPublicComponentsArrayInactive : test.parentPublicComponentsArray;
			IList lis = inactive ? test.parentPublicComponentsListInactive : test.parentPublicComponentsList;

			if (list == null)
			{
				Assert.IsNull(arr);
				Assert.IsNull(lis);
			}
			else
			{
				Assert.IsTrue(arr.Count == list.Length);
				Assert.IsTrue(lis.Count == list.Length);
				for (var index = 0; index < list.Length; index++)
				{
					Assert.AreEqual(list[index], arr[index]);
					Assert.AreEqual(list[index], lis[index]);
				}
			}
		}

		public static void TestMyPrivateComponentsArray(GameObject obj)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var list = test.GetComponents<Renderer>();
			SerializedObject so = new SerializedObject(test);
			Assert.IsNotNull(so);
			var propArray = so.FindProperty("myPrivateComponentsArray");
			Assert.IsNotNull(propArray);
			var propList = so.FindProperty("myPrivateComponentsList");
			Assert.IsNotNull(propList);

			if (list == null)
			{
				Assert.IsNull(propArray.objectReferenceValue);
				Assert.IsNull(propList.objectReferenceValue);
			}
			else
			{
				Assert.IsTrue(propArray.arraySize == list.Length);
				Assert.IsTrue(propList.arraySize == list.Length);
				for (var index = 0; index < list.Length; index++)
				{
					Assert.AreEqual(list[index], propArray.GetArrayElementAtIndex(index).objectReferenceValue);
					Assert.AreEqual(list[index], propArray.GetArrayElementAtIndex(index).objectReferenceValue);
				}
			}


		}

		public static void TestMyChildPrivateComponentsArray(GameObject obj, bool inactive = false)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var list = test.GetComponentsInChildren<Renderer>(inactive);
			SerializedObject so = new SerializedObject(test);
			Assert.IsNotNull(so);
			var propArray = inactive ? so.FindProperty("childPrivateComponentsArrayInactive") : so.FindProperty("childPrivateComponentsArray");
			Assert.IsNotNull(propArray);
			var propList = inactive ? so.FindProperty("childPrivateComponentsListInactive") : so.FindProperty("childPrivateComponentsList");
			Assert.IsNotNull(propList);

			if (list == null)
			{
				Assert.IsNull(propArray.objectReferenceValue);
				Assert.IsNull(propList.objectReferenceValue);
			}
			else
			{
				Assert.IsTrue(propArray.arraySize == list.Length);
				Assert.IsTrue(propList.arraySize == list.Length);
				for (var index = 0; index < list.Length; index++)
				{
					Assert.AreEqual(list[index], propArray.GetArrayElementAtIndex(index).objectReferenceValue);
					Assert.AreEqual(list[index], propArray.GetArrayElementAtIndex(index).objectReferenceValue);
				}
			}
		}

		public static void TestMyParentPrivateComponentsArray(GameObject obj, bool inactive = false)
		{
			Assert.IsTrue(obj != null);
			var test = obj.GetComponent<ProcessorTestComponent>();
			Assert.IsTrue(test != null);
			var list = test.GetComponentsInParent<Renderer>(inactive);
			SerializedObject so = new SerializedObject(test);
			Assert.IsNotNull(so);
			var propArray = inactive ? so.FindProperty("parentPrivateComponentsArrayInactive") : so.FindProperty("parentPrivateComponentsArray");
			Assert.IsNotNull(propArray);
			var propList = inactive ? so.FindProperty("parentPrivateComponentsListInactive") : so.FindProperty("parentPrivateComponentsList");
			Assert.IsNotNull(propList);

			if (list == null)
			{
				Assert.IsNull(propArray.objectReferenceValue);
				Assert.IsNull(propList.objectReferenceValue);
			}
			else
			{
				Assert.IsTrue(propArray.arraySize == list.Length);
				Assert.IsTrue(propList.arraySize == list.Length);
				for (var index = 0; index < list.Length; index++)
				{
					Assert.AreEqual(list[index], propArray.GetArrayElementAtIndex(index).objectReferenceValue);
					Assert.AreEqual(list[index], propArray.GetArrayElementAtIndex(index).objectReferenceValue);
				}
			}
		}
	}
}
