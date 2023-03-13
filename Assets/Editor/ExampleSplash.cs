using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This script was made by Jonathan Reynolds for Example Purposes. 
/// Feel Free to edit this as needed but keep this Summary reference.
/// </summary>


namespace ExampleNamespace
{
    [InitializeOnLoad]
    public class ExampleSplash : EditorWindow
    {


        public static string newSceneName = "";

        public static string newButtonName = "";
        public static string newButtonLink = "";

        public static bool showOnStartup = true;



        public SplashScreen.SplashButtons splashItems;


        List<Item> ExampleList = new List<Item>();

        class Item
        {
            public string name;
            public int number;
        }

        public void ExampleLinq()
        {
            Item foundItem = ExampleList.FirstOrDefault(x => x.name == "Example");

            
            Item[] foundItemsArray = ExampleList.Where(y => y.number == 5).ToArray();


            List<Item> foundItemsList = ExampleList.Where(y => y.number == 5).ToList();
        }


        static ExampleSplash()
        {

            EditorApplication.update -= ShowSplashScreen;
            EditorApplication.update += ShowSplashScreen;
        }

        [UnityEditor.MenuItem("SplashExample/SplashScreen", false, 1)]
        public static void ShowWindow()
        {
            Splash();
        }

        public static void ShowSplashScreen()
        {
            EditorApplication.update -= ShowSplashScreen;

            if (EditorPrefs.GetBool("AWBOI_ShowSplash", true))
                Splash();

        }

        public static void Splash()
        {
            //Load Json here

            newSceneName = SceneManager.GetActiveScene().name;
            EditorWindow splashWindow = EditorWindow.GetWindow<ExampleSplash>("Splash Example");
            splashWindow.minSize = new Vector2(445, 479.66f);
            splashWindow.maxSize = new Vector2(445, 479.66f);

            splashWindow.Show();
        }


        public void OnGUI()
        {


            EditorGUILayout.BeginVertical();


            EditorGUILayout.LabelField("Scenes:");


            EditorGUILayout.BeginVertical("Box");

            foreach (var scene in splashItems.SceneNames)
            {
                Color tmpColor = GUI.color;
                if (scene.Image != null)
                {
                    GUI.color = new Color(1, 1, 1, 0);
                    SceneButton(new GUIContent(scene.Image), scene.Name);
                }
                else
                {
                    SceneButton(new GUIContent(scene.Name), scene.Name);
                }
                GUI.color = tmpColor;

            }

            EditorGUILayout.EndHorizontal();



            EditorGUILayout.LabelField("Links:");
            EditorGUILayout.BeginVertical("Box");
            foreach (var button in splashItems.Buttons)
            {
                ButtonLink(button);
            }
            EditorGUILayout.EndVertical();


            EditorGUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            ToggleSplashOnStart();
        }

        public void ButtonLink(ExampleNamespace.SplashScreen.SplashButton button)
        {
            GUIContent content = null;
            Color tmpColor = GUI.color;
            if (button.Image != null)
            {
                content = new GUIContent(button.Image);
                GUI.color = new Color(1, 1, 1, 0);
            }
            else
            {
                content = new GUIContent(button.ButtonText);
            }

            if (GUILayout.Button(new GUIContent(button.ButtonText)))
            {
                switch (button.ButtonType)
                {
                    case SplashScreen.SplashButton.bType.WebLink:
                        Application.OpenURL(button.Link);
                        break;
                    case SplashScreen.SplashButton.bType.File:
                        if (button.Link != "")
                            Application.OpenURL($"{Application.dataPath}/{button.Link}");
                        break;
                }

            }
            GUI.color = tmpColor;
        }

        public void SceneButton(GUIContent content, string SceneName)
        {

            if (GUILayout.Button(content))
            {
                CheckOpenScenes();
                EditorSceneManager.OpenScene($"{Application.dataPath}/{splashItems.SceneFolder}/{SceneName}.unity");
            }
        }

        public void CheckOpenScenes()
        {
            if (EditorSceneManager.GetActiveScene().isDirty && EditorUtility.DisplayDialog("Save Scene", "This will Save all open Scenes and close them, Continue?", "Ok", "Cancel"))
            {
                int countLoaded = EditorSceneManager.sceneCount;
                UnityEngine.SceneManagement.Scene[] loadedScenes = new UnityEngine.SceneManagement.Scene[countLoaded];

                for (int i = 0; i < countLoaded; i++)
                {
                    loadedScenes[i] = EditorSceneManager.GetSceneAt(i);
                }

                foreach (var scene in loadedScenes)
                {
                    EditorSceneManager.SaveScene(scene);
                    EditorSceneManager.CloseScene(scene, true);
                }

            }
        }

        public void ToggleSplashOnStart()
        {

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorPrefs.SetBool("AWBOI_ShowSplash", GUILayout.Toggle(EditorPrefs.GetBool("AWBOI_ShowSplash"), "Show Me at Startup"));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }





    }
}
