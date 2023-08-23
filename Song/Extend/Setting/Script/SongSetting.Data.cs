using System.Collections;
using System.Collections.Generic;
using Song.Extend.UXMLExtend.Chart;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Song.Tools;

namespace Song.Extend.Setting
{
    public partial class SongSetting : EditorWindow
    {
        /// <summary>
        /// 左侧界面
        /// </summary>
        private VisualElement _leftMenu;
        /// <summary>
        /// 状态
        /// </summary>
        private VisualElement _state;
        /// <summary>
        /// 资源
        /// </summary>
        private VisualElement _assets;
        /// <summary>
        /// 插件
        /// </summary>
        private VisualElement _plugins;
        /// <summary>
        /// 关于
        /// </summary>
        private VisualElement _information;
        /// <summary>
        /// 右侧显示区域
        /// </summary>
        private VisualElement _area;
        
        //临时变量
        /// <summary>
        /// 当前选中的左侧按钮
        /// </summary>
        private VisualElement _nowChooseLeftItem;
        private VisualElement _nowChooseLeftItemIcon;
        private VisualElement _nowChooseLeftItemBg;
        private VisualElement _nowChooseLeftItemText;
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            _leftMenu    = rootVisualElement.Q<VisualElement>("LeftMenu");
            _state       = _leftMenu.Q<VisualElement>("State");
            _assets      = _leftMenu.Q<VisualElement>("Assets");
            _plugins     = _leftMenu.Q<VisualElement>("Plugins");
            _information = _leftMenu.Q<VisualElement>("Information");
            _area        = rootVisualElement.Q<VisualElement>("Area");
            
            //color init
            var iconBgColor = SColor.HexToColor("#2D2D2D");
            var iconBgColorChoose = SColor.KleinBlue();
            var iconAndTextColor = SColor.HexToColor("#9E9E9E");
            var textColorChoose = Color.white;
            var iconColorChoose = SColor.HexToColor("#E0FFFF");
            
            var items = new VisualElement[]{_state,_assets,_plugins,_information};
            foreach (var item in items)
            {
                item.RegisterCallback<MouseDownEvent>(delegate(MouseDownEvent evt)
                {
                    var iconBg = item.Q<VisualElement>("IconBg");
                    var icon = iconBg.Q<VisualElement>("Icon");
                    var text = item.Q<Label>("Name");
                    if (_nowChooseLeftItem != null)
                    {
                        _nowChooseLeftItemBg.style.unityBackgroundImageTintColor = iconBgColor;
                        _nowChooseLeftItemIcon.style.unityBackgroundImageTintColor = iconAndTextColor;
                        _nowChooseLeftItemText.style.color = iconAndTextColor;
                    }

                    iconBg.style.unityBackgroundImageTintColor = iconBgColorChoose;
                    icon.style.unityBackgroundImageTintColor = iconColorChoose;
                    text.style.color = textColorChoose;
                    
                    _nowChooseLeftItem = item;
                    _nowChooseLeftItemBg = iconBg;
                    _nowChooseLeftItemIcon = icon;
                    _nowChooseLeftItemText = text;
                    
                    _area.Clear();
                    switch (item.name)
                    {
                        case "State":
                            InitState();
                            break;
                        case "Assets":
                            break;
                    }
                });
            }
        }
        
    }
}