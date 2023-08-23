using OfficeOpenXml.Style;
using UnityEngine;

namespace Song.Extend.ExcelEditor
{
    public class ExcelCell
    {
        /// <summary>
        /// 内容
        /// </summary>
        private string _content;
        
        /// <summary>
        /// 字体尺寸
        /// </summary>
        private float _fontSize;

        /// <summary>
        /// 背景颜色
        /// </summary>
        private Color _backGroundColor;
        
        /// <summary>
        /// 颜色
        /// </summary>
        private Color _color;
        
        /// <summary>
        /// 字体对齐
        /// </summary>
        private ExcelHorizontalAlignment _alignment;
    }
}