using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Song.Extend.UXMLExtend.Chart;
using Song.Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Song.Extend.Setting
{
    /// <summary>
    /// state page
    /// </summary>
    public partial class SongSetting : EditorWindow
    {
        private Texture2D _pieImg;
        private Texture2D _memoryImg;
        private Texture2D _cpuImg;
        private Texture2D _ramImg;
        private Texture2D _bluetoothImg;
        private Tuple<float, float, float, float, float, float> _memoryData;

        /// <summary>
        /// initialize state page 初始化状态
        /// </summary>
        private void InitState()
        {
            LoadStateData();
            MemoryCard();       //内存卡片
            DiskCard(Card("Card",0, _memoryImg));
            CPUCard(Card("Card",1, _cpuImg));
            RamCard(Card("Card",2,_ramImg));
            BluetoothCard();
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        private void  LoadStateData()
        {
            _pieImg       = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Song/Extend/Setting/Art/pie.png");
            _memoryImg    = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Song/Extend/Setting/Art/memory.png");
            _cpuImg       = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Song/Extend/Setting/Art/cpu.png");
            _ramImg       = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Song/Extend/Setting/Art/ram.png");
            _bluetoothImg = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Song/Extend/Setting/Art/bluetooth.png");
            
            //获取数据
            _memoryData = GetData();
        }

        /// <summary>
        /// 内存卡片
        /// </summary>
        private void MemoryCard()
        {
            //将数据存入
            var data = new List<Tuple<string,string,float, Color>>
            {
                new Tuple<string, string, float, Color>
                    ("图片","img",_memoryData.Item1,SColor.HexToColor("FF5F7E")),
                new Tuple<string, string, float, Color>
                    ("音频","audio",_memoryData.Item2,SColor.HexToColor("FFD766")),
                new Tuple<string, string, float, Color>
                    ("视频","video",_memoryData.Item3,SColor.HexToColor("7C94FF")),
                new Tuple<string, string, float, Color>
                    ("模型","model",_memoryData.Item4,SColor.HexToColor("FF7DF3")),
                new Tuple<string, string, float, Color>
                    ("其他","others",_memoryData.Item5,SColor.HexToColor("06F7C7"))
            };
            
            //创建存储卡片
            var memory = new VisualElement
            {
                name = "memory",
                style =
                {
                    width = 300,
                    height = 200,
                    marginLeft = 30,
                    marginTop = 30,
                    borderBottomLeftRadius = 6,
                    borderBottomRightRadius = 6,
                    borderTopLeftRadius = 6,
                    borderTopRightRadius = 6,
                    backgroundColor = SColor.HexToColor("313131"),
                }
            };

            //内存卡片图标
            var memoryIcon = new VisualElement()
            {
                name = "memoryIcon",
                style =
                {
                    position = Position.Absolute,
                    width = 20,
                    height = 20,
                    marginLeft = 5,
                    marginTop = 5,
                    backgroundImage = _pieImg
                }
            };
            
            //圆环
            var disc = new VisualElement
            {
                name = "disc",
                style =
                {
                    width = 150,
                    height = 150,
                    marginLeft = 20,
                    marginTop = 25,
                    borderBottomLeftRadius = Length.Percent(50),
                    borderBottomRightRadius = Length.Percent(50),
                    borderTopLeftRadius = Length.Percent(50),
                    borderTopRightRadius = Length.Percent(50),
                    backgroundColor = SColor.HexToColor("454545"),
                    alignItems = Align.Center,
                    justifyContent = Justify.Center
                }
            };
            
            //圆环中心字
            var discNowMemoryText = new Label
            {
                name = "discMemoryText",
                text = UnitConversion(_memoryData.Item6),
                style =
                {
                    color = SColor.KleinBlue(),
                    fontSize = 20
                }
            };
            
            //全部空间
            var discAllMemoryText = new Label
            {
                name = "discAllMemoryText",
                text = "占用空间",
                style =
                {
                    color = SColor.White(),
                    fontSize = 10,
                    unityTextAlign = TextAnchor.MiddleCenter
                }
            };
            
            //标签
            VisualElement CreateTag(string tagNameText,float tagValue,Color color)
            {
                var tag = new VisualElement
                {
                    name=tagNameText,
                    style =
                    {
                        height = 30,
                        flexDirection = FlexDirection.Row,
                        marginBottom = 7
                    }
                };

                //标签图标
                var tagIcon = new VisualElement
                {
                    style =
                    {
                        width = 14,
                        height = 14,
                        backgroundColor = color,
                        borderBottomLeftRadius = Length.Percent(50),
                        borderBottomRightRadius = Length.Percent(50),
                        borderTopLeftRadius = Length.Percent(50),
                        borderTopRightRadius = Length.Percent(50),
                        marginTop = 2,
                        marginRight = 6
                    }
                };

                //标签名
                var tagName = new Label
                {
                    text = tagNameText
                };

                var tagMemory = new Label()
                {
                    text = UnitConversion(tagValue),
                    style =
                    {
                        marginTop = 16,
                        marginLeft = -30,
                        fontSize = 11,
                        color = Color.white
                    }
                };
                
                tag.Add(tagIcon);
                tag.Add(tagName);
                tag.Add(tagMemory);
                
                return tag;
            }

            //标签区域
            var tagArea = new VisualElement
            {
                style =
                {
                    position = Position.Absolute,
                    height = Length.Percent(100),
                    width = 100,
                    marginLeft = 200,
                    marginTop = 10
                }
            };
            
            //派状表
            var pieChart = new PieChart
            {
                style =
                {
                    position = Position.Absolute,
                    width = 150,
                    height = 150,
                    marginLeft = 20,
                    marginTop = 25,
                }
            };
            
            //添加数据
            foreach (var t in data)
            {
                //初始化
                var pie = new Pie(t.Item2, t.Item3, t.Item4);
                var tag = CreateTag(t.Item1,t.Item3,t.Item4);
                
                pieChart.AddData(pie);
                pie.RegisterCallback<MouseEnterEvent>(Enter);
                pie.RegisterCallback<MouseOutEvent>(Out);
                
                
                tagArea.Add(tag);
                tag.RegisterCallback<MouseEnterEvent>(Enter);
                tag.RegisterCallback<MouseOutEvent>(Out);
                
                void Enter(MouseEnterEvent evt)
                {
                    pie.LineWidth = 22;
                    pie.MarkDirtyRepaint();
                    //为其他标签设置遮挡
                    foreach (var t in tagArea.Children())
                    {
                        if(t==tag) continue;
                        var overlay = new VisualElement()
                        {
                            name = "overlay",
                            style =
                            {
                                position = Position.Absolute,
                                width = Length.Percent(100),
                                height = Length.Percent(100),
                                backgroundColor = SColor.HexToColorAlpha("313131",0.6f)
                            }
                        };
                        t.Add(overlay);
                    }
                    
                    //添加所占比例
                    var progress = new Label()
                    {
                        name = "progress",
                        text = $"{((pie.EndAngle-pie.StartAngle)/3.6f):F2}%",
                        style =
                        {
                            color = pie.Color
                        }
                    };
                    disc.Add(progress);
                    progress.SendToBack();
                }
                
                void Out(MouseOutEvent evt)
                {
                    pie.LineWidth = 16;
                    pie.MarkDirtyRepaint();
                    
                    //删除遮挡
                    foreach (var t in tagArea.Children())
                    {
                        var o = t.Q<VisualElement>("overlay");
                        if(o!=null)
                            t.Remove(o);
                    }
                    
                    //删除比例
                    var progress= disc.Q<Label>("progress");
                    if(progress!=null)
                        disc.Remove(progress);
                }
            }
            
            disc.Add(discNowMemoryText);
            disc.Add(discAllMemoryText);
            memory.Add(memoryIcon);
            memory.Add(disc);
            memory.Add(tagArea);
            memory.Add(pieChart);
            _area.Add(memory);

            CardShadow(memory);
        }

        /// <summary>
        /// get data 获取数据
        /// </summary>
        private Tuple<float, float, float, float, float, float> GetData()
        {
            var imageType = new List<string>() {"jpg","jpeg","png","svg","bmp","gif","webp"};
            var audioType = new List<string>() {"mp3","wav","flac","ogg","aac","wma","aac"};
            var videoType = new List<string>() {"mp4","mkv","avi","wmv","flv","mov","3gp","mpg"};
            var modelType = new List<string>() {"obj","fbx","dae","3ds","gltf","glb"};

            float totalSize = 0;
            float imageSize = 0;
            float voiceSize = 0;
            float videoSize = 0;
            float modelSize = 0;
            float otherSize = 0;
            
            var filePaths = Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories);
            foreach (var filePath in filePaths)
            {
                if (!filePath.Contains(".")) continue;
                var lastIndexOf = filePath.LastIndexOf(".", StringComparison.Ordinal)+1;
                var type = filePath.Substring(lastIndexOf,filePath.Length-lastIndexOf);
                type = type.ToLower();
                
                var fileSize = new FileInfo(filePath).Length;
                if (imageType.Contains(type))
                {
                    imageSize += fileSize;
                }
                else if (audioType.Contains(type))
                {
                    voiceSize += fileSize;
                }
                else if (videoType.Contains(type))
                {
                    videoSize += fileSize;
                }
                else if (modelType.Contains(type))
                {
                    modelSize += fileSize;
                }
                else
                {
                    otherSize += fileSize;
                }
                totalSize += fileSize;
            }

            return new Tuple<float, float, float, float, float, float>(
                imageSize,
                voiceSize,
                videoSize,
                modelSize,
                otherSize,
                totalSize
            );
        }

        /// <summary>
        /// Unit Conversion 单位转换
        /// </summary>
        /// <param name="byte">byte 字节</param>
        /// <returns>字符串</returns>
        private string UnitConversion(float @byte)
        {
            var i = 0;
            while (@byte>=1024)
            {
                @byte /= 1024;
                i++;
            }

            return i switch
            {
                0 => $"{@byte:F2}B",
                1 => $"{@byte:F2}KB",
                2 => $"{@byte:F2}MB",
                3 => $"{@byte:F2}GB",
                4 => $"{@byte:F2}TB",
                _ => $"{@byte:F2}PB",
            };
        }

        /// <summary>
        /// 卡片管理
        /// </summary>
        private VisualElement Card(string cardName,int i,Texture2D iconImg)
        {
            var card = new VisualElement()
            {
                name = "cardName",
                style =
                {
                    position = Position.Absolute,
                    width = 200,
                    height = 60,
                    marginLeft = 350,
                    marginTop = 30+i*70,
                    borderBottomLeftRadius = 6,
                    borderBottomRightRadius = 6,
                    borderTopLeftRadius = 6,
                    borderTopRightRadius = 6,
                    backgroundColor = SColor.HexToColor("313131"),
                }
            };
            
            var icon = new VisualElement()
            {
                name = "memoryIcon",
                style =
                {
                    position = Position.Absolute,
                    width = 20,
                    height = 20,
                    marginLeft = 5,
                    marginTop = 5,
                    backgroundImage = iconImg
                }
            };

            var cardNameLabel = new Label()
            {
                name = "cardNameLabel",
                style =
                {
                    position = Position.Absolute,
                    marginLeft = 30,
                    marginTop = 7,
                    color = SColor.HexToColor("818181")
                }
            };
            
            card.Add(cardNameLabel);
            card.Add(icon);
            _area.Add(card);
            CardShadow(card);
            
            return card;
        }

        /// <summary>
        /// 进度组件
        /// </summary>
        VisualElement Progress(float use,float all,Color useColor,Color freeColor,bool tip = true)
        {
            var useData = UnitConversion(use);
            var freeData = UnitConversion(all - use);
            
            var free = (all - use)/all;
            use = use / all;

            var area = new VisualElement()
            {
                name = "ProgressArea",
                style =
                {
                    position = Position.Absolute,
                    width = 160,
                    height = 16,
                    marginLeft = 16,
                    marginTop = 32,
                    flexDirection = FlexDirection.Row,
                }
            };
            
            var useArea = new VisualElement()
            {
                name = "Use",
                style =
                {
                    width = 160*use,
                    height = 16,
                    backgroundColor = useColor,
                    borderTopLeftRadius = 6,
                    borderBottomLeftRadius = 6,
                    alignItems = Align.Center,
                    justifyContent = Justify.Center
                }
            };
            
            var freeArea = new VisualElement()
            {
                name = "Free",
                style =
                {
                    width = 160*free,
                    height = 16,
                    backgroundColor = freeColor,
                    borderTopRightRadius = 6,
                    borderBottomRightRadius = 6,
                    alignItems = Align.Center,
                    justifyContent = Justify.Center
                }
            };

            if (tip)
            {
                useArea.tooltip = $"已使用{useData}";
                freeArea.tooltip = $"未使用{freeData}";
            }
            
            useArea.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent evt)
            {
                useArea.style.marginTop = -2;
                useArea.style.height = 20;
                var label = new Label()
                {
                    name = "Tip",
                    text = $"{use*100:F2}%",
                    style =
                    {
                        color = SColor.White(),
                    }
                };
                useArea.Add(label);
            });
            useArea.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent evt)
            {
                useArea.style.marginTop = 0;
                useArea.style.height = 16;
                var label = useArea.Q<Label>("Tip");
                if(label!=null)
                    useArea.Remove(label);
            });
            freeArea.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent evt)
            {
                freeArea.style.marginTop = -2;
                freeArea.style.height = 20;
                var label = new Label()
                {
                    name = "Tip",
                    text = $"{free*100:F2}%",
                    style =
                    {
                        color = SColor.White(),
                    }
                };
                freeArea.Add(label);
            });
            freeArea.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent evt)
            {
                freeArea.style.marginTop = 0;
                freeArea.style.height = 16;
                var label = freeArea.Q<Label>("Tip");
                if(label!=null)
                    freeArea.Remove(label);
            });
            
            area.Add(useArea);
            area.Add(freeArea);
            return area;
        }

        /// <summary>
        /// 磁盘管理
        /// </summary>
        /// <param name="card"></param>
        void DiskCard(VisualElement card)
        {
#if UNITY_STANDALONE_OSX
            var currentDrive = "Macintosh HD";
            var currentDirectory = Directory.GetCurrentDirectory();
            foreach (var directory in Directory.GetDirectories("/Volumes"))
            {
                var path = directory+currentDirectory;
                if (Directory.Exists(path))
                {
                    var lastIndexOf = directory.LastIndexOf("/", StringComparison.Ordinal)+1;
                    currentDrive = directory.Substring(lastIndexOf, directory.Length - lastIndexOf);
                }
            }
#else
                var currentDrive = Environment.GetLogicalDrives()[0];
#endif
            var driveInfo = new DriveInfo("/");
            var diskSize = UnitConversion(driveInfo.TotalSize);
            var diskFree = UnitConversion(driveInfo.TotalFreeSpace);
            card.Q<Label>("cardNameLabel").text = $"{currentDrive} {diskSize}";
            card.Add(Progress(
                    driveInfo.TotalSize-driveInfo.TotalFreeSpace,
                    driveInfo.TotalSize,
                    SColor.KleinBlue(),
                    SColor.HexToColor("4D4C4C")));
        }

        /// <summary>
        /// cpu 显示
        /// </summary>
        /// <param name="card">卡片</param>
        void CPUCard(VisualElement card)
        {
            var processorType = SystemInfo.processorType;
            var label = card.Q<Label>("cardNameLabel");
            if (processorType.Length >= 20)
                processorType = processorType[..20] + "...";
            label.text = processorType;
            
            card.Add(Progress(
                1024*1024*2,
                1024*1024*8,
                SColor.KleinBlue(),
                SColor.HexToColor("4D4C4C"),false));
        }

        /// <summary>
        /// 内存显示
        /// </summary>
        /// <param name="card">卡片</param>
        void RamCard(VisualElement card)
        {
            var systemMemorySize = (SystemInfo.systemMemorySize/1024)+"GB";
            
            // 获取已使用内存
            var memoryUsed = GC.GetTotalMemory(false); 

            // 获取最大可用内存 
            var memoryMax = GC.GetTotalMemory(true);
            card.Q<Label>("cardNameLabel").text = systemMemorySize;
            
            card.Add(Progress(
                memoryUsed*128,
                (memoryMax+memoryUsed)*128,
                SColor.KleinBlue(),
                SColor.HexToColor("4D4C4C")));
        }

        /// <summary>
        /// 蓝牙模块组件
        /// </summary>
        VisualElement BluetoothCardItem(string itemName)
        {
            var item = new VisualElement()
            {
                style =
                {
                    width = 120,
                    height = 30,
                    marginLeft = 20,
                    marginTop = 5,
                    borderBottomLeftRadius = 6,
                    borderBottomRightRadius = 6,
                    borderTopLeftRadius = 6,
                    borderTopRightRadius = 6,
                    backgroundColor = SColor.HexToColor("4D4C4C"),
                    alignItems = Align.Center,
                    justifyContent = Justify.Center
                }
            };
            
            var label = new Label()
            {
                text = itemName,
                style =
                {
                    color = SColor.White(),
                }
            };
            
            item.Add(label);

            return item;
        }

        /// <summary>
        /// 蓝牙模块卡片
        /// </summary>
        void BluetoothCard()
        {
            var card = new VisualElement()
            {
                name = "Bluetooth",
                style =
                {
                    width = 160,
                    height = 200,
                    marginLeft = 30,
                    marginTop = 20,
                    borderBottomLeftRadius = 6,
                    borderBottomRightRadius = 6,
                    borderTopLeftRadius = 6,
                    borderTopRightRadius = 6,
                    backgroundColor = SColor.HexToColor("313131"),
                }
            };
            
            var icon = new VisualElement()
            {
                name = "memoryIcon",
                style =
                {
                    position = Position.Absolute,
                    width = 20,
                    height = 20,
                    marginLeft = 5,
                    marginTop = 5,
                    backgroundImage = _bluetoothImg
                }
            };
            
            card.Add(new VisualElement(){style = { height = 30}});
            
            card.Add(BluetoothCardItem("MX Master 3"));
            card.Add(BluetoothCardItem("Air Pos Pro"));
            
            card.Add(icon);
            _area.Add(card);

            CardShadow(card);
        }

        void CardShadow(VisualElement card)
        {
            //高光
            card.style.borderTopWidth = 2;
            card.style.borderLeftWidth = 2;
            card.style.borderTopColor = SColor.HexToColorAlpha("4D4C4C",0.4f);
            card.style.borderLeftColor = SColor.HexToColorAlpha("4D4C4C",0.4f);
            
            //阴影
            card.style.borderBottomWidth = 2;
            card.style.borderRightWidth = 2;
            card.style.borderBottomColor = SColor.HexToColorAlpha("242323",0.4f);
            card.style.borderRightColor = SColor.HexToColorAlpha("242323",0.4f);
        }
    }
}