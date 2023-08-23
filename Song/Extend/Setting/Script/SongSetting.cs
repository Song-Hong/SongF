using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Song.Extend.Setting
{
    public partial class SongSetting:EditorWindow
    {
        [MenuItem("Song/Setting")]
        public static void ShowSongSetting()
        {
            var wnd = GetWindow<SongSetting>();
            wnd.titleContent = new GUIContent("SongSetting");
            wnd.minSize = new Vector2(720, 500);
            wnd.Show();
        }

        private void CreateGUI()
        {
            //显示 UI
            var path = AssetDatabase.GUIDToAssetPath("ab0047a3e8024b6f985963991115ec74");
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            uxml.CloneTree(rootVisualElement);
            
            InitData(); //初始化数据
        }
    }
}