using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectInput;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank.Core;

namespace Tank.App
{
    public class TankUI : Direct2D
    {

        public int WinX, WinY;

        public float GlobalSpeed;

        public List<TargetTypes.BLock> Tanks;

        public TargetTypes.RECT FormBoxRange;



        public TankUI(string formName = "Default") : base(formName)
        {
            Tanks = new List<TargetTypes.BLock>();
            CreateResource();
        }

        ~TankUI()
        {

        }


        public override void Update(DemoTime demoTime, KeyboardState key)
        {
            var player_01 = Tanks.Where(row => row.Name == "P_01").FirstOrDefault();

            if (key.IsPressed(Key.Left))
            {
                player_01.Move(-GlobalSpeed * player_01.Speed, 0, FormBoxRange);
            }

            if (key.IsPressed(Key.Up))
            {
                player_01.Move(0, -GlobalSpeed * player_01.Speed, FormBoxRange);
            }

            if (key.IsPressed(Key.Right))
            {
                player_01.Move(GlobalSpeed * player_01.Speed, 0, FormBoxRange);
            }

            if (key.IsPressed(Key.Down))
            {

                player_01.Move(0, GlobalSpeed * player_01.Speed, FormBoxRange);
            }


        }

        public override void Draw()
        {
            base.Draw();

            using (var formBox = new RectangleGeometry(D2dFactory, (RawRectangleF)FormBoxRange))
            {
                DefaultBrush.Color = Color.Black;
                D2dRenderTarget.FillGeometry(formBox, DefaultBrush, null);
            }

            //D2dRenderTarget.DrawRectangle(new RawRectangleF(FormBoxRange.Left, FormBoxRange.Top, FormBoxRange.Right, FormBoxRange.Bottom), DefaultBrush);

            //D2dRenderTarget.FillGeometry(RenderTank, DefaultBrush, null);

            foreach (var item in Tanks)
            {
                using (var defTank = new RectangleGeometry(D2dFactory, (RawRectangleF)item.Rect))
                {
                    DefaultBrush.Color = (Color)item.Color;
                    D2dRenderTarget.FillGeometry(defTank, DefaultBrush, null);
                }
            }




            //D2dRenderTarget.DrawGeometry(RenderTank, DefaultBrush);

        }


        public override void KeyBordCallBack(KeyEventArgs e)
        {
            //var player_01 = Tanks.Where(row => row.Name == "P_01").FirstOrDefault();

            //switch (e.KeyCode)
            //{
            //    case Keys.Left:
            //        {
            //            player_01.Move(-GlobalSpeed * player_01.Speed, 0);
            //        }
            //        break;
            //    case Keys.Up:
            //        {
            //            player_01.Move(0, -GlobalSpeed * player_01.Speed);
            //        }
            //        break;
            //    case Keys.Right:
            //        {
            //            player_01.Move(GlobalSpeed * player_01.Speed, 0);
            //        }
            //        break;
            //    case Keys.Down:
            //        {
            //            player_01.Move(0, GlobalSpeed * player_01.Speed);
            //        }
            //        break;
            //    default:
            //        break;
            //}
            base.KeyBordCallBack(e);
        }

        public override void MouseCallBack(MouseEventArgs e)
        {
            base.MouseCallBack(e);
        }

        public void CreateResource()
        {
            GlobalSpeed = 0.01f;
            WinX = Mainform.ClientSize.Width;
            WinY = Mainform.ClientSize.Height;
            FormBoxRange = new TargetTypes.RECT(20, 20, WinX - 20, WinY - 20);

            Tanks.Add(new TargetTypes.BLock("P_01", new TargetTypes.RECT(WinX / 2 - 80, WinY - 60, WinX / 2 - 40, WinY - 20), 1, (long)Color.Yellow));

            Tanks.Add(new TargetTypes.BLock("P_02", new TargetTypes.RECT(WinX / 2 - 20, WinY - 60, WinX / 2 + 20, WinY - 20), 1, (long)Color.Green));


            //TextFormat textFormat = new TextFormat(FactoryDWrite, "宋体", 128) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };

            //TextLayout textLayout = new TextLayout(FactoryDWrite, " 1", textFormat, Mainform.Width, base.Mainform.Height);


            //var rectangleGeometry = new RoundedRectangleGeometry(D2dFactory, new RoundedRectangle() { RadiusX = 32, RadiusY = 32, Rect = new RectangleF(128, 128, Mainform.ClientSize.Width - 128 * 2, Mainform.ClientSize.Height - 128 * 2) });

            //D2dRenderTarget.DrawTextLayout(new Vector2(0, 0), textLayout, SolidColorBrush_[Color.Red], DrawTextOptions.None);
            //solidColorBrush.Color = new Color4(1, 1, 1, (float)Math.Abs(Math.Cos(stopwatch.ElapsedMilliseconds * .001)));
            //D2dRenderTarget.FillGeometry(rectangleGeometry, DefaultBrush, null);

            //var taskId = RenderQueneAdd(() =>
            //{
            //    D2dRenderTarget.DrawRectangle(new RawRectangleF(20, 20, Mainform.ClientSize.Width - 20, Mainform.ClientSize.Height - 20), DefaultBrush);

            //});



        }








    }
}
