using System;
using Song.Tools;
using UnityEditor;
using UnityEditor.PackageManager.UI;
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
            
            var s = new SOG();
            var read = System.IO.File.ReadAllText("Assets/Song/Extend/CodeGeneration/ScriptableObjectGeneration/Config/student.json");
            var generation = s.Generation(read);
            Debug.Log("创建文件:"+generation);
            
            read = System.IO.File.ReadAllText("Assets/Song/Extend/CodeGeneration/ScriptableObjectGeneration/Config/students.json");
            generation = s.Generation(read);
            Debug.Log("创建文件:"+generation);
            
            window.Close();
        }
    }
}