using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Song.Extend.UXMLExtend.Chart
{
    /// <summary>
    /// Pie Chart 饼状图
    /// </summary>
    public class PieChart:VisualElement
    {
        public new class UxmlFactory : UxmlFactory<PieChart, UxmlTraits> { }

        /// <summary>
        /// pie max value 饼状图全部数值总和
        /// </summary>
        public float MaxValue { get; private set; }

        /// <summary>
        /// Pie chart proportion data 饼状图所占比例数据
        /// </summary>
        private Dictionary<string, Pie> _data;
        
        /// <summary>
        /// Initialize 初始化
        /// </summary>
        public PieChart()
        {
            _data = new Dictionary<string, Pie>();
        }

        /// <summary>
        /// Quick Set 快速设置
        /// </summary>
        /// <param name="dataName">data name 数据名</param>
        public Pie this[string dataName]
        {
            get => _data.ContainsKey(dataName) ? _data[dataName] : null;
            set
            {
                if(_data.ContainsKey(dataName))
                    _data[dataName] = value;
                else
                {
                    AddData(dataName, value.Value, value.Color);
                }
            }
        }

        /// <summary>
        /// set max value 设置最大值
        /// </summary>
        /// <param name="value">max value 最大值</param>
        public void SetMaxValue(float value)
        {
            float maxValue = 0;
            foreach (var pie in _data.Values)
            {
                maxValue += pie.Value;
            }

            if (value > maxValue)
                MaxValue = value;
        }

        /// <summary>
        /// add data 添加数据
        /// </summary>
        /// <param name="pie">pie data 饼图数据</param>
        public void AddData(Pie pie)
        {
            if(_data.ContainsKey(pie.name)) return;
            pie.style.position = Position.Absolute;
            pie.style.width = style.width;
            pie.style.height = style.height;
            _data.Add(pie.name,pie);
            MaxValue += pie.Value;
            ReDraw();
        }

        /// <summary>
        /// add data 添加数据
        /// </summary>
        /// <param name="dataName">pie data name 数据名</param>
        /// <param name="value">pie data value 数据值</param>
        /// <param name="color">pie data color 数据颜色</param>
        public void AddData(string dataName,float value,Color color)
        {
            if(_data.ContainsKey(dataName)) return;
            var pie = new Pie(value, color)
            {
                name=dataName,
                style =
                {
                    position  = Position.Absolute,
                    width = style.width,
                    height = style.height,
                }
            };
            _data.Add(dataName,pie);
            MaxValue += value;
            ReDraw();
        }
        
        /// <summary>
        /// remove pie data 删除数据
        /// </summary>
        /// <param name="dataName">数据名</param>
        public void RemoveData(string dataName)
        {
            if(!_data.ContainsKey(dataName)) return;
            MaxValue-=_data[dataName].Value;
            _data.Remove(dataName);
        }
        
        /// <summary>
        /// update data 更新数据
        /// </summary>
        /// <param name="dataName">pie data name 数据名</param>
        /// <param name="value">pie data value 数据值</param>
        /// <param name="color">pie data color 数据颜色</param>
        public void UpdateData(string dataName,float value,Color color)
        {
            if(!_data.ContainsKey(dataName)) return;
            MaxValue -= _data[dataName].Value;
            _data[dataName].Value = value;
            _data[dataName].Color = color;
            MaxValue += value;
            ReDraw();
        }

        /// <summary>
        /// search data pie 获取饼图组件
        /// </summary>
        /// <param name="dataName">data name 饼图名称</param>
        /// <returns>pie 饼图组件</returns>
        public Pie SearchData(string dataName)
        => _data.TryGetValue(dataName, out var value) ? value : null;
        
        /// <summary>
        /// search data pie color 获取饼图组件颜色
        /// </summary>
        /// <param name="dataName">data name 饼图名称</param>
        /// <returns>pie color 饼图组件颜色</returns>
        public Color SearchColor(string dataName)
        =>_data.TryGetValue(dataName, out var result) ? result.Color : Color.clear;
        
        /// <summary>
        /// search data pie value 获取饼图组件值
        /// </summary>
        /// <param name="dataName">data name 饼图名称</param>
        /// <returns>pie value 饼图组件值</returns>
        public float SearchValue(string dataName)
            =>_data.TryGetValue(dataName, out var result) ? result.Value : 0;
        
        /// <summary>
        /// redraw 重新绘制
        /// </summary>
        private void ReDraw()
        {
            Clear();
            float startAngle = 0;
            foreach (var pie in _data.Values)
            {
                var endAngle = (pie.Value/MaxValue)*360+startAngle;
                pie.SetAngle(startAngle,endAngle);
                startAngle = endAngle;
                Add(pie);
            }
        }
    }
}