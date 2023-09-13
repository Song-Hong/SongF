using UnityEditor;
using UnityEngine;

namespace Song.Extend.CodeGeneration
{
    public class ScriptableObjectCreate
    {
        /// <summary>
        /// 创建列表ScriptableObject
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="className">类名</param>
        public void CreateList(string path,string className)
        {
            var assets = ScriptableObject.CreateInstance($"Game.CodeGeneration.{className}");
            AssetDatabase.CreateAsset(assets, path);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// 创建列表ScriptableObject
        /// </summary>
        /// <param name="namespaceName">命名空间名称</param>
        /// <param name="path">文件路径</param>
        /// <param name="className">类名</param>
        public void CreateList(string namespaceName,string path, string className)
        {
            var assets = ScriptableObject.CreateInstance($"{namespaceName}.{className}");
            AssetDatabase.CreateAsset(assets, path);
            AssetDatabase.SaveAssets();
        }
        
        /// <summary>
        /// 创建ScriptableObject并添加为子文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="className"></param>
        public void CreateAdd(string path,string className)
        =>CreateAdd("Game.CodeGeneration",path,className,className);
        
        /// <summary>
        /// 创建ScriptableObject并添加为子文件
        /// </summary>
        /// <param name="namespaceName">命名空间名称</param>
        /// <param name="path">文件路径</param>
        /// <param name="className">类名</param>
        public void CreateAdd(string namespaceName,string path, string className)
        =>CreateAdd(namespaceName,path,className,className);

        /// <summary>
        /// 创建ScriptableObject并添加为子文件
        /// </summary>
        /// <param name="namespaceName">命名空间名称</param>
        /// <param name="path">文件路径</param>
        /// <param name="className">类名</param>
        /// <param name="name">文件名</param>
        public void CreateAdd(string namespaceName,string path, string className,string name)
        {
            var asset = ScriptableObject.CreateInstance($"{namespaceName}.{className}");
            asset.name = name;
            AssetDatabase.AddObjectToAsset(asset, path);
            AssetDatabase.SaveAssets();
        }
    }
}