using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using UnityEngine;

namespace Song.Extend.ExcelEditor
{
    public class ExcelData
    {
        private Dictionary<string, List<List<string>>> _data;

        public ExcelData()
        {
            _data = new Dictionary<string, List<List<string>>>();
        }

        public ExcelData(string filePath):this()
        {
            // 加载Excel文件
            using var excelPackage = new ExcelPackage(new FileInfo(filePath));
            var worksheet = excelPackage.Workbook.Worksheets["主表"]; 

            // 读取单元格样式
            var cell = worksheet.Cells[2, 3];
            var style = cell.Style;                             

            // 获取字体  
            var fontName = style.Font.Name;   
            var fontSize = style.Font.Size;

            //获取颜色
            var fillBackgroundColor = style.Fill.BackgroundColor; 
            var fontColor = style.Font.Color;            
            
            Debug.Log(cell.Value);
            Debug.Log(fontName);
            Debug.Log(fontSize);
            Debug.Log(fillBackgroundColor);
        }

    }
}