using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Song.Extend.ExcelEditor
{
    public partial class ExcelView:EditorWindow
    {
        private const string Path = "Assets/Song/Extend/ExcelEditor/View/ExcelView.uxml";

        [MenuItem("Song/Extend/Excel")]
        public static void ShowExcelView()
        {
            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Song/Extend/ExcelEditor/Art/SongExcel.png");
            var window = GetWindow<ExcelView>();
            window.titleContent = new GUIContent("Excel Editor",icon);
            window.Show();
        }

        private void CreateGUI()
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(Path);
            uxml.CloneTree(rootVisualElement);

            new ExcelData("/Users/song/Downloads/自走棋数据/棋子数据.xlsx");
        }
    }
}