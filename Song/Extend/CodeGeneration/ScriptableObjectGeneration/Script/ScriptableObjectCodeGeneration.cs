using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace Song.Extend.CodeGeneration
{
    public class ScriptableObjectCodeGeneration
    {
        public interface ICodeType{}

        public class Single : ICodeType{}

        public class List : ICodeType{}

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="data">json 数据</param>
        /// <typeparam name="T">生成类型</typeparam>
        public void Generation<T>(string data) where T:ICodeType
        {
            var jsonReader = new JsonReader(data);
            var jsonData = JsonMapper.ToObject<Dictionary<string,string>>(jsonReader);
            Debug.Log(jsonData["namespace"]);
            foreach (KeyValuePair<string,string> o in jsonData)
            {
                Debug.Log(o.Key+":"+o.Value);
            }
            if (typeof(T) == typeof(Single))
            {
                
            }
        }
    }
}