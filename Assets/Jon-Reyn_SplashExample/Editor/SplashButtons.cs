using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script was made by Jonathan Reynolds for Example Purposes. 
/// Feel Free to edit this as needed but keep this Summary reference.
/// </summary>


namespace ExampleNamespace.SplashScreen
{
    [Serializable]
    [CreateAssetMenu(fileName = "SplashButtons", menuName = "ScriptableObjects/SplashButtons", order = 1)]
    public class SplashButtons : ScriptableObject
    {
        public string SceneFolder;
        public List<SplashSceneName> SceneNames;
        public List<SplashButton> Buttons;
    }

    [Serializable]
    public class SplashSceneName
    {
        public string Name;

        [Tooltip("(Optional) Icon shown in the Splash Screen.")]
        public Texture2D Image;

        public SplashSceneName(string _name)
        {
            Name = _name;
            Image = null;
        }
    }

    [Serializable]
    public class SplashButton
    {
        public bType ButtonType;
        public string ButtonText;
        public string Link;

        [Tooltip("(Optional) Icon shown in the Splash Screen.")]
        public Texture2D Image;
        

        public enum bType
        {
            WebLink,
            File
        }

        public SplashButton(string text, string link)
        {
            ButtonText = text;
            Link = link;
        }
    }

}


