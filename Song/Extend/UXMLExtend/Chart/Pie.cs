using UnityEngine;
using UnityEngine.UIElements;

namespace Song.Extend.UXMLExtend.Chart
{
    public class Pie:VisualElement
    {
        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color;
        /// <summary>
        /// 数值
        /// </summary>
        public float Value;
        /// <summary>
        /// 绘制线条粗细
        /// </summary>
        public float LineWidth = 16.0f;
        /// <summary>
        /// 开始角度
        /// </summary>
        public float StartAngle { get; private set; }
        /// <summary>
        /// 结束角度
        /// </summary>
        public float EndAngle { get; private set;}

        /// <summary>
        /// initialize 初始化
        /// </summary>
        /// <param name="dataName">data name 数据名</param>
        /// <param name="value">value 数值</param>
        /// <param name="color">color 颜色</param>
        public Pie(string dataName,float value,Color color)
        {
            name = dataName;
            Color = color;
            Value = value;
            StartAngle = 0;
            EndAngle = 0;
            generateVisualContent += GenerateVisualContent;
        }
        
        /// <summary>
        /// initialize 初始化
        /// </summary>
        /// <param name="value">value 数值</param>
        /// <param name="color">color 颜色</param>
        public Pie(float value,Color color)
        {
            Color = color;
            Value = value;
            StartAngle = 0;
            EndAngle = 0;
            generateVisualContent += GenerateVisualContent;
        }

        /// <summary>
        /// set angle 设置角度
        /// </summary>
        /// <param name="startAngle">start angle 开始角度</param>
        /// <param name="endAngle">end angle 结束角度</param>
        public void SetAngle(float startAngle,float endAngle)
        {
            StartAngle = startAngle;
            EndAngle = endAngle;
        }

        /// <summary>
        /// Draw 绘制
        /// </summary>
        /// <param name="context"></param>
        private void GenerateVisualContent(MeshGenerationContext context)
        {
            var width = contentRect.width;
            var height = contentRect.height;

            var painter = context.painter2D;
            painter.lineWidth = LineWidth;
            painter.lineCap = LineCap.Butt;

            // Draw the track
            painter.strokeColor = Color;
            painter.BeginPath();
            painter.Arc(new Vector2(width * 0.5f, height * 0.5f), width * 0.5f, StartAngle, EndAngle);
            painter.Stroke();
        }
    }
}