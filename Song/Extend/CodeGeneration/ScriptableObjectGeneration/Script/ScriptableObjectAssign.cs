using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

namespace Song.Extend.CodeGeneration
{
    public class ScriptableObjectAssign
    {
        public void AssignAll(string path,List<Dictionary<string, string>> data)
        {
            
        }

        public void AssignList(string path)
        {
            
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="datas">数据</param>
        public void Assign(string path,Dictionary<string,string> datas)
        {
            var readContent = File.ReadLines(path);
            var saveContent = "";
            var changeValue = " ";
            foreach (var s in readContent)
            {
                foreach (var key in datas.Keys)
                {
                    if (s.Contains(key))
                    {   
                        saveContent += $"  {key}: {datas[key]}\n";
                        changeValue = key;
                        break;
                    }
                }
                if (!string.IsNullOrWhiteSpace(changeValue))
                {
                    datas.Remove(changeValue);
                    changeValue = null;
                }
                else
                {
                    saveContent += s+"\n";
                }
            }
            File.WriteAllText(path,saveContent);
        }
    }
}