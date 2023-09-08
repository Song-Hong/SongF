using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Song.Extend.CodeGeneration
{
    public class ScriptableObjectAssign
    {
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="datas">数据</param>
        public void Assign(string path,Dictionary<string,string> datas)
        {
            using (TextWriter writer = new StreamWriter(path)) 
            {
                var serializer = new Serializer();
                serializer.Serialize(new Emitter(writer,2), datas); 
            }
        }
    }
}