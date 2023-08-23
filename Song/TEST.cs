using System;
using UnityEditor;
using UnityEngine;

using SOG = Song.Extend.CodeGeneration.ScriptableObjectCodeGeneration;

namespace Song
{
    public class TEST:EditorWindow
    {
        [MenuItem("Song/TEST")]
        public static void ShowTest()
        {
            var window = GetWindow<TEST>();
            window.titleContent = new GUIContent("测试");
            window.Show();
        }

        private void CreateGUI()
        {
            var json = 
                "{\n" +
                "\"namespace\":\"Song.CodeGeneration\",\n" +
                "\"classname\":\"Test\"\n" +
                "}";
            var s = new SOG();
            s.Generation<SOG.Single>(json);
        }
    }
}