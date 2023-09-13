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
            var SOC = new ScriptableObjectCreate();
            var SOA = new ScriptableObjectAssign();
            var path = "Assets/Script/Data/student.asset";
            
            SOC.CreateList("Song.Code",path,"students");
            for (int i = 0; i < 10; i++)
            {
                SOC.CreateAdd("Song.Code",
                    path,
                    "student",
                    i.ToString());
            }

            SOA.AssignList(path);
            // var SOA = new ScriptableObjectAssign();
            // var dictionary = new Dictionary<string, string>();
            // dictionary.Add("name","肖杰");
            // dictionary.Add("age","22");
            // SOA.Assign("Assets/Script/Data/student.asset",dictionary);
        }
    }
}