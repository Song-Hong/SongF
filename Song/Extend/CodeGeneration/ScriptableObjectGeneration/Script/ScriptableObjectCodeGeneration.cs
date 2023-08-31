using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using Song.Tools;
using UnityEditor;
using UnityEngine;

namespace Song.Extend.CodeGeneration
{
    public class ScriptableObjectCodeGeneration
    {
        private const string TempPath = "Assets/Song/Extend/CodeGeneration/ScriptableObjectGeneration/Config/Temp.txt";
        private const string SavePath = "Assets/Script/CodeGeneration";
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="data">json 数据</param>
        public string Generation(string data)
        {
            var classContent = File.ReadAllText(TempPath);
            var className = "";
            var content = "";
            var jsonReader = new JsonReader(data);
            var json = JsonMapper.ToObject<Dictionary<string,string>>(jsonReader);
            foreach (var entry in json)
            {
                if(string.IsNullOrWhiteSpace(entry.Key) || string.IsNullOrWhiteSpace(entry.Value))
                    continue;
                if (string.CompareOrdinal(entry.Key.ToLower(), "namespace") == 0)
                {
                    classContent = classContent.Replace("NAMESPACE",entry.Value);
                    continue;
                }
                if (string.CompareOrdinal(entry.Key.ToLower(), "class") == 0)
                {
                    className = entry.Value;
                    classContent = classContent.Replace("CLASSNAME",entry.Value);
                    continue;
                }
                content += $"\n\t\tpublic {entry.Value} {entry.Key};\n";
            }
            classContent = classContent.Replace("CONTENT",content);
            var filePath = $"{SavePath}/{className}.cs";
            
            //标记
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var info = 
                       $"/*\n" +
                       $"=======此代码为自动生成=======\n" +
                       $"生成工具 Song.CodeGeneration\n" +
                       $"生成时间:{time}\n" +
                       $"===========================\n" +
                       $"*/\n";
            classContent = classContent.Replace("INFOMATION",info);
            
            File.WriteAllText(filePath,classContent);
            AssetDatabase.Refresh();
            
            return filePath;
        }
    }
}