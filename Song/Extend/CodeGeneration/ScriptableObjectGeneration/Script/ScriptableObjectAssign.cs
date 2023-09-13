using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
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
            var rootName = Path.GetFileNameWithoutExtension(path);
            Debug.Log(rootName);
            
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="datas">数据</param>
        public void Assign(string path,Dictionary<string,string> datas)
        {
            var readContent = File.ReadLines(path);
            var saveContent = Assign(readContent, datas);
            File.WriteAllText(path,saveContent);
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="contents">内容</param>
        /// <param name="datas">数据</param>
        /// <returns>存储数据</returns>
        public string Assign( IEnumerable<string> contents, Dictionary<string, string> datas)
        {
            var saveContent = "";
            var changeValue = " ";
            foreach (var s in contents)
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
            return saveContent;
        }
    }
}