using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SharpDX.Mathematics.Interop;

namespace Tank.App.TargetTypes
{

    public interface RangeObject
    {




    }



    public class LimitTank
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  方向 1 = 上; 2 = 左 ; 3 = 下; 4 = 右;
        /// </summary>
        public int Dir { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public long Color { get; set; }

        /// <summary>
        /// 移动速度
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// 坦克类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 枪械特效
        /// </summary>
        public List<int> Gun { get; set; }

        public RawPoint Point { get; set; }

        /// <summary>
        /// 坦克主体
        /// </summary>
        public BLock MainTank { get; set; }

        /// <summary>
        /// 坦克炮管
        /// </summary>
        public BLock GunBarrel { get; set; }
        
        /// <summary>
        ///坦克子弹 
        /// </summary>
        public BLock GunBullet { get; set; }


        public LimitTank(string name, int type, long color, RawPoint raw)
        {
            Dir = 1;
            Name = name;
            Type = type;
            Point = raw;
            Speed = 5.0f;
            Color = color;
            Gun = new List<int>();


            MainTank = new BLock();
            GunBarrel = new BLock();
            GunBullet = new BLock();






        }


    }


    public class BLock
    {
        public RawPoint Point { get; set; }

        public string Name { get; set; }

        public RECT Rect { get; set; }

        public long Color { get; set; }

        public List<int> Gun { get; set; }

        public float Speed { get; set; }

        public int Type { get; set; }

        /// <summary>
        ///  方向 1 = 上; 2 = 左 ; 3 = 下; 4 = 右;
        /// </summary>
        public int Dir { get; set; }

        public BLock()
        {

        }


        public BLock (string name, RECT rc, int type, long color)
        {
            Dir = 1;
            Rect = rc;
            Type = type;
            Name = name;
            Speed = 5.0f;
            Color = color;
            Gun = new List<int>();
        }


        public void Move(float x, float y, RECT range)
        {
            var rx = Rect.Left + x;
            var ry = Rect.Top + y;
            var rw = Rect.Right + x;
            var rh = Rect.Bottom + y;

            if (rx < range.Left)
            {
                rx = range.Left;
                rw = rx + (Rect.Right - Rect.Left);
            }

            if (rw > range.Right)
            {
                rw = range.Right;
                rx = rw - (Rect.Right - Rect.Left);
            }
            if (ry < range.Top)
            {
                ry = range.Top;
                rh = ry + (Rect.Bottom - Rect.Top);
            }

            if (rh > range.Bottom)
            {
                rh = range.Bottom;
                ry = rh - (Rect.Bottom - Rect.Top);
            }
            //if (rh < range.Bottom) rh = range.Bottom;

            Rect = new RECT(rx, ry, rw, rh);
        }

    }


    public struct RECT
    {
        public float Bottom { get; set; }
        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }

        public RECT(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static explicit operator RawRectangleF(RECT v)
        {
            return new RawRectangleF(v.Left, v.Top, v.Right, v.Bottom);
        }
    }
}
