using System;
using System.Collections.Generic;
using Song.Extend.CodeGeneration;
using Song.Tools;
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
            
            var s = new SOG();
            var read = System.IO.File.ReadAllText("Assets/Song/Extend/CodeGeneration/ScriptableObjectGeneration/Config/student.json");
            var generation = s.Generation(read);
            Debug.Log("创建文件:"+generation);
            
            read = System.IO.File.ReadAllText("Assets/Song/Extend/CodeGeneration/ScriptableObjectGeneration/Config/students.json");
            generation = s.Generation(read);
            Debug.Log("创建文件:"+generation);
            
            window.Close();
        }

        [MenuItem("Song/TEST1")]
        public static void ShowTest1()
        {
            var dictionary = new Dictionary<string,string> ();
            dictionary.Add("你好","1");
            dictionary.Add("你是","2");
            dictionary.Add("1234","3");

            var SOA = new ScriptableObjectAssign();
            SOA.Assign("Assets/1.yaml",dictionary);
        }
    }
}