using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tank.App.TargetTypes
{

    public class Tank
    {
        public string Name { get; set; }

        public RECT Rect { get; set; }

        public long Color { get; set; }

        public List<int> Gun { get; set; }

        public double Speed { get; set; }

        public int Type { get; set; }

        /// <summary>
        ///  方向 1 = 上; 2 = 左 ; 3 = 下; 4 = 右;
        /// </summary>
        public int Dir { get; set; }

        public Tank (string name, RECT rc, int type, long color)
        {
            Dir = 1;
            Rect = rc;
            Type = type;
            Name = name;
            Speed = 10.0;
            Color = color;
            Gun = new List<int>();
        }

    }


    public struct RECT
    {
        public float Bottom;
        public float Left;
        public float Right;
        public float Top;

        public RECT(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}
