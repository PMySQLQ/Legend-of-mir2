using Client.MirControls;
using Client.MirGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MirObjects
{
    public class Damage
    {
        public string Text;
        public Color Colour;
        public int Distance;
        public long ExpireTime;
        public double Factor;
        public int Offset;

        public MirLabel DamageLabel;

        public Damage(string text, int duration, Color colour, int distance = 50)
        {
            ExpireTime = (long)(CMain.Time + duration);
            Text = text;
            Distance = distance;
            Factor = duration / this.Distance;
            Colour = colour;
        }

        public void Draw(Point displayLocation)
        {
            long timeRemaining = ExpireTime - CMain.Time;

            if (DamageLabel == null)
            {
                DamageLabel = new MirLabel
                {
                    AutoSize = true,
                    BackColour = Color.Transparent,
                    ForeColour = Colour,
                    OutLine = true,
                    OutLineColour = Color.Black,
                    Text = Text,
                    Font = new Font(Settings.FontName, 8F, FontStyle.Bold)
                };
            }

            displayLocation.Offset((int)(15 - (Text.Length * 3)), (int)(((int)((double)timeRemaining / Factor)) - Distance) - 75 - Offset);

            DamageLabel.Location = displayLocation;
            DamageLabel.Draw();
        }
    }
    /// <summary>
    /// 伤害显示类(素材) 完成!!
    /// </summary>
    public class Flap
    {
        private List<int> valArr = new List<int>();
        private int MarkIndex, StartIndex;
        private int OffsetX;


        public long ExpireTime;

        public Flap(FlapType type, int duration = 0)
        {
            string str = Math.Abs(duration).ToString();

            for (int i = 0; i < str.Length; i++)

                valArr.Add(int.Parse(str.Substring(i, 1)));

            switch (type)
            {
                case FlapType.Yellow:
                    StartIndex = 90;
                    MarkIndex = duration > 0 ? 160 : 160;
                    break;
                case FlapType.Green:
                    StartIndex = 126;
                    MarkIndex = duration > 0 ? 137 : 136;
                    OffsetX = -20;
                    break;
                case FlapType.Red:
                    StartIndex = 102;
                    MarkIndex = duration > 0 ? 113 : 112;
                    break;
                case FlapType.Blue:
                    StartIndex = 114;
                    MarkIndex = duration > 0 ? 125 : 124;
                    OffsetX = 20;
                    break;
                case FlapType.Miss:
                    StartIndex = 138;
                    break;


                case FlapType.Critical:
                    StartIndex = 150;
                    MarkIndex = duration > 0 ? 101 : 100;
                    break;
                case FlapType.Immunity:
                    StartIndex = 371;
                    break;
                case FlapType.DodgeAbility:
                    StartIndex = 372;
                    break;
                case FlapType.IgnoreArmor:
                    StartIndex = 373;
                    break;
            }

            ExpireTime = (long)(CMain.Time + 950);
        }

        public void Draw(Point displayLocation)
        {
            long timeRemaining = ExpireTime - CMain.Time;

            displayLocation.Offset(OffsetX, (int)(((int)((double)timeRemaining / 20)) - 50) - 75 - OffsetX);

            //单图片
            if (StartIndex >= 138)
            {
                Libraries.Prguse3.Draw(StartIndex, displayLocation.X, displayLocation.Y);
                return;
            }

            Libraries.Prguse3.Draw(MarkIndex, displayLocation.X - 5, displayLocation.Y + 6);//正负符号

            for (int i = 0; i < valArr.Count; i++)

                Libraries.Prguse3.Draw(StartIndex + valArr[i], displayLocation.X + i * 9 + 12, displayLocation.Y);


        }
    }

    /// <summary>
    /// 伤害飘雪类型
    /// </summary>
    public enum FlapType : byte
    {
        Yellow,//其他对象
        Green,//自己
        Red,//其他玩家
        Blue,//自己魔法
        Miss,//躲闪
        Critical,
        Immunity,//免疫
        DodgeAbility,//闪避
        IgnoreArmor//无视
    }
}
