using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public class PositionMoveDialog : MirImageControl
    {
        public MirButton RememberButton, PositionMoveButton, PositionDeleteButton, CloseButton, UpButton, DownButton, PositionBar;
        private MoveCell[] Movers;
        public static List<PlayerTeleportInfo> MoveList;
        public static int SelectIndex = 0;
        private int ScrollIndex = 0, ShowCount = 1, PageRows = 24, MapIndex = -1, ScrollBarBaseX = 717;


        string PositionName = string.Empty;

        public PositionMoveDialog()
        {
            Index = 934;
            Library = Libraries.Title;
            Movable = true;

            BeforeDraw += (o, e) => OnBeforeDraw();

            Sort = true;
            Location = Center;
            RememberButton = new MirButton
            {
                Index = 944,
                HoverIndex = 945,
                PressedIndex = 946,
                Location = new Point(10, 270),
                Library = Libraries.Title,
                Parent = this,
                Hint = "输入定位坐标",
                Sound = SoundList.ButtonC
            };
            RememberButton.Click += (o, e) =>
            {
                new RememberMoveDialog(PositionName);
            };

            PositionMoveButton = new MirButton
            {
                Index = 947,
                HoverIndex = 948,
                PressedIndex = 949,
                Location = new Point(70, 270),
                Library = Libraries.Title,
                Parent = this,
                Hint = "移动到定位点",
                Sound = SoundList.ButtonC
            };
            PositionMoveButton.Click += (o, e) =>
            {
                MirMessageBox mirMessageBox = new MirMessageBox("是否移动到选定地图位置?\n传送将消耗3000金币.", MirMessageBoxButtons.YesNo);
                mirMessageBox.Show();
                mirMessageBox.YesButton.Click += (o, e) =>
                {
                    Network.Enqueue(new PositionMove { SelectIndex = PositionMoveDialog.SelectIndex });
                    Hide();
                };
            };

            PositionDeleteButton = new MirButton
            {
                Index = 950,
                HoverIndex = 951,
                PressedIndex = 952,
                Location = new Point(130, 270),
                Library = Libraries.Title,
                Parent = this,
                Hint = "删除定位点",
                Sound = SoundList.ButtonC
            };
            PositionDeleteButton.Click += (o, e) =>
            {
                MirMessageBox mirMessageBox = new MirMessageBox("您确定要删除所选位置吗?\n删除后无法还原.", MirMessageBoxButtons.YesNo);
                mirMessageBox.Show();
                mirMessageBox.YesButton.Click += (o, e) =>
                {
                    Network.Enqueue(new Chat
                    {
                        Message = "@위치기억삭제 " + PositionMoveDialog.SelectIndex.ToString()
                    });
                    UpdateMoveCells();
                };
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(174, 5),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA
            };
            CloseButton.Click += (o, e) =>
            {
                Hide();
            };

            UpButton = new MirButton
            {
                HoverIndex = 198,
                Index = 197,
                Visible = true,
                Library = Libraries.Prguse2,
                Location = new Point(ScrollBarBaseX - 540, 34),
                Size = new Size(16, 14),
                Parent = this,
                PressedIndex = 199,
                Sound = SoundList.ButtonA
            };
            UpButton.Click += (o, e) =>
            {
                if (ScrollIndex != 0)
                {
                    ScrollIndex--;
                    UpdateMoveCells();
                    UpdateScrollPosition();
                }
            };

            DownButton = new MirButton
            {
                HoverIndex = 208,
                Index = 207,
                Visible = true,
                Library = Libraries.Prguse2,
                Location = new Point(ScrollBarBaseX - 540, 250),
                Size = new Size(16, 14),
                Parent = this,
                PressedIndex = 209,
                Sound = SoundList.ButtonA
            };
            DownButton.Click += (o, e) =>
            {
                if (ScrollIndex != ShowCount - PageRows)
                {
                    ScrollIndex++;
                    UpdateMoveCells();
                    UpdateScrollPosition();
                }
            };

            PositionBar = new MirButton
            {
                Index = 206,
                Library = Libraries.Prguse2,
                Location = new Point(ScrollBarBaseX - 540, 46),
                Parent = this,
                Movable = true,
                Sound = SoundList.None
            };
            PositionBar.OnMoving += PositionBar_OnMoving;
        }

        private void OnBeforeDraw()
        {
            MapControl map = GameScene.Scene.MapControl;

            if (map == null || !Visible) return;

            PositionName = string.Format(map.Title + "   " + GameScene.User.CurrentLocation.X.ToString() + " : " + GameScene.User.CurrentLocation.Y.ToString());

        }
        public void PositionBar_OnMoving(object sender, MouseEventArgs e)
        {
            int scrollBarBaseX = ScrollBarBaseX;

            int y = PositionBar.Location.Y;

            if (PositionBar.Location.Y >= DownButton.Location.Y - 15) y = DownButton.Location.Y - 15;   
            if (y < UpButton.Location.Y + 15) y = UpButton.Location.Y + 15;
            
            int location = y - 62;
            int interval = 366 / (ShowCount - PageRows);

            double yPoint = (location / interval);

            ScrollIndex = (int)Convert.ToInt16(Math.Floor(yPoint));

            if (ScrollIndex > ShowCount - PageRows) ScrollIndex = ShowCount - PageRows;        
            if (ScrollIndex <= 0)ScrollIndex = 0;

            UpdateMoveCells();
            PositionBar.Location = new Point(scrollBarBaseX - 540, y);
        }

        private void KeyPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int count = e.Delta / SystemInformation.MouseWheelScrollDelta;
            if (ScrollIndex == 0 && count >= 0) return;
            if (ScrollIndex == ShowCount - PageRows && count <= 0) return;


            ScrollIndex -= ((count > 0) ? 1 : -1);

            UpdateMoveCells();
            UpdateScrollPosition();
               
            
        }

        private void UpdateScrollPosition()
        {
            int interval = 366 / (ShowCount - PageRows);
            int scrollBarBaseX = ScrollBarBaseX;
            int y = 62 + ScrollIndex * interval;
            if (y >= DownButton.Location.Y - 15) y = DownButton.Location.Y - 15;           
            if (y < UpButton.Location.Y + 15) y = UpButton.Location.Y + 15;         
            PositionBar.Location = new Point(scrollBarBaseX - 540, y);
        }

        public void UpdateMoveCells()
        {
            if (ShowCount < PageRows)
            {
                ScrollIndex = 0;
            }
            for (int i = 0; i < Movers.Length; i++)
            {
                Movers[i].Location = new Point(15, 50 + i * 16 - ScrollIndex * 16 - 10);
                if (ScrollIndex <= i && ScrollIndex + PageRows > i)
                {
                    Movers[i].Show();
                }
                else
                {
                    Movers[i].Hide();
                }
            }
        }
        private void ClearMoveList()
        {
            bool flag = Movers == null;
            if (!flag)
            {
                for (int i = 0; i < Movers.Length; i++)
                {
                    Movers[i].Dispose();
                }
            }
        }

        public void ReloadMoveList()
        {
            ClearMoveList();
            if (PositionMoveDialog.MoveList != null)
            {
                int count = PositionMoveDialog.MoveList.Count;
                Movers = new MoveCell[count];
                ShowCount = this.Movers.Length;
                for (int i = 0; i < count; i++)
                {
                    Movers[i] = new MoveCell
                    {
                        Parent = this,
                        Location = new Point(15, 50 + i * 16 - 10),
                        Size = new Size(115, 16),
                        MoveLocation = PositionMoveDialog.MoveList[i].Location,
                        Name = PositionMoveDialog.MoveList[i].Name,
                        ColorIndex = PositionMoveDialog.MoveList[i].ColorIndex,
                        Index = i,
                    };
                    Movers[i].MouseWheel += this.KeyPanel_MouseWheel;
                }
                UpdateMoveCells();
            }
        }
        public void Show()
        {
            if (Visible) return;
            Visible = true;
        }

        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
        public void Toggle()
        {
            if (!Visible)
                Show();
            else
                Hide();
        }

    }

    public sealed class MoveCell : MirControl
    {
        public int Index;
        public Point MoveLocation;
        private MirLabel NameLabel;
        public MirImageControl SelectedImage;
        public int ColorIndex;
        public string Name
        {
            get
            {
                return (NameLabel != null) ? NameLabel.Text : string.Empty;
            }
            set
            {
                bool flag;
                string text = GetText(value, out flag) + (flag ? ".." : "");
                if (text != NameLabel.Text)
                {
                    NameLabel.Text = text;
                }
            }
        }

        public MoveCell()
        {
            SelectedImage = new MirImageControl
            {
                Index = 1346,
                Library = Libraries.Prguse2,
                Location = new Point(0, -4),
                Parent = this,
                Visible = false
            };
            Border = false;
            base.BorderColour = Color.Transparent;
            base.BeforeDraw += Border_BeforeDraw;
            base.Click += delegate (object o, EventArgs e)
            {
                PositionMoveDialog.SelectIndex = Index;
            };
            NameLabel = new MirLabel
            {
                Text = "text",
                Location = new Point(0, 0),
                Parent = this,
                AutoSize = true,
                DrawFormat = TextFormatFlags.Default,
                Font = new Font(Settings.FontName, 8f),
                ForeColour = Color.White,
                NotControl = true
            };
        }

        private string GetText(string text, out bool isOver)
        {
            isOver = false;
            for (int i = 0; i < text.Length; i++)
            {
                string text2 = text.Remove(i);
                if (Encoding.Default.GetBytes(text2).Length >= 30)
                {
                    isOver = true;
                    return text2;
                }
            }
            return text;
        }

        private void Border_BeforeDraw(object sender, EventArgs e)
        {
            if (MirControl.MouseControl != null)
            {
                Border = (IsMouseOver(MirControl.MouseControl.DisplayLocation) || PositionMoveDialog.SelectIndex == Index);
                if (GameScene.User != null)
                {
                    NameLabel.ForeColour = GetColor();
                }
                if (MirControl.MouseControl == null)
                {
                }
            }
        }

        private Color GetColor()
        {
            if (GameScene.User == null)
            {
                return Color.White;
            }
            else
            {
                if (PositionMoveDialog.SelectIndex == Index)
                {
                    return Color.Red;
                }
                else
                {
                    //return Color.White;

                    switch (ColorIndex)
                    {
                        case 0:
                            return Color.White;
                        case 1:
                            return Color.Gray;
                        case 2:
                            return Color.Orange;
                        case 3:
                            return Color.Red;
                        case 4:
                            return Color.Pink;
                        case 5:
                            return Color.Yellow;
                        case 6:
                            return Color.LightPink;
                        case 7:
                            return Color.Green;
                        case 8:
                            return Color.Blue;
                        case 9:
                            return Color.LightSkyBlue;
                        default:
                            return Color.White;

                    }
                }
            }
        }

        public void Show()
        {
            if (!Visible) return;
            Visible = true;
            Network.Enqueue(new Chat
            {
               Message = "@위치이동리스트"
            });
            
        }

        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }

        public void Toggle()
        {
            if (!Visible)
            {
                Visible = !Visible;
                Network.Enqueue(new Chat
                {
                    Message = "@위치이동리스트"
                });
                Show();
            }
            else
            {
                Visible = !Visible;
                Hide();
            }
            Redraw();
        }

    }

    public sealed class RememberMoveDialog : MirImageControl
    {
        public readonly MirButton OKButton, CancelButton;
        public readonly MirTextBox InputTextBox;
        public readonly MirLabel CaptionLabel;
        public MirControl[] ColorIcon;
        public static int SelectIndex = 0;

        public RememberMoveDialog(string name)
        {
            Index = 935;
            Library = Libraries.Title;
            Parent = GameScene.Scene;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);
            Modal = true;
            Visible = true;

            CaptionLabel = new MirLabel
            {
                DrawFormat = TextFormatFlags.WordBreak,
                Location = new Point(12, 35),
                Size = new Size(235, 40),
                Parent = this,
                Text = "请指定定位地点的名称",
            };

            InputTextBox = new MirTextBox
            {
                Parent = this,
                Location = new Point(17, 57),
                Size = new Size(215, 5),
                MaxLength = 50,
                Text = name,
                Font = new Font(Settings.FontName, 8F),
            };
            InputTextBox.SetFocus();
            InputTextBox.TextBox.KeyPress += MirInputBox_KeyPress;

            OKButton = new MirButton
            {
                HoverIndex = 201,
                Index = 200,
                Library = Libraries.Title,
                Location = new Point(60, 123),
                Parent = this,
                PressedIndex = 202,
            };
            OKButton.Click += (o, e) =>
            {
                if (PositionMoveDialog.MoveList.Count >= Globals.MaxPositionMove)
                {
                    MirMessageBox messagea = new MirMessageBox("无法存储更多的位置");
                    messagea.Show();
                    return;
                }
                Network.Enqueue(new MemoryLocation { Name = InputTextBox.Text, ColorIndex = RememberMoveDialog.SelectIndex });
                Dispose();
                GameScene.Scene.PositionMoveDialog.UpdateMoveCells();
         
            };

            CancelButton = new MirButton
            {
                HoverIndex = 204,
                Index = 203,
                Library = Libraries.Title,
                Location = new Point(160, 123),
                Parent = this,
                PressedIndex = 205,
            };
            CancelButton.Click += DisposeDialog;

            ColorIcon = new MirControl[12];

            for (int i = 0; i < ColorIcon.Length; i++)
            {
                ColorIcon[i] = new ColorIcon
                {
                    Parent = this,
                    Location = new Point(24 + (i * 17), 85),
                    Size = new Size(15, 15),
                    Index = i,
                };
            }
        }
        public void MirInputBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (OKButton != null && !OKButton.IsDisposed)
                    OKButton.InvokeMouseClick(EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                if (CancelButton != null && !CancelButton.IsDisposed)
                    CancelButton.InvokeMouseClick(EventArgs.Empty);
                e.Handled = true;
            }
        }
        public void DisposeDialog(object sender, EventArgs e)
        {
            Dispose();
        }
        public void Show()
        {
            if (!Visible)
            {
                Visible = true;
            }
        }
        public void Hide()
        {
            if (Visible)
            {
                Visible = false;
            }
        }
    }

    public sealed class ColorIcon : MirControl
    {
        public int Index;
        public ColorIcon()
        {
            Border = true;
            BorderColour = Color.Gold;
            BeforeDraw += Border_BeforeDraw;
            base.Click += delegate (object o, EventArgs e)
            {
                //记录界面颜色选择序号
                RememberMoveDialog.SelectIndex = Index;
            };
        }

        private void Border_BeforeDraw(object sender, EventArgs e)
        {
            if (MirControl.MouseControl != null)
            {
                Border = (IsMouseOver(MirControl.MouseControl.DisplayLocation) || RememberMoveDialog.SelectIndex == Index);
            }
        }
        public void Show()
        {
            if (Visible) return;
            Visible = true;
        }
        public void Hide()
        {
            if (!Visible) return;
            Visible = false;
        }
    }

}
