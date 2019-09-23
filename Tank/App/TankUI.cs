using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
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

        public RectangleGeometry RenderTank;

        public List<TargetTypes.Tank> Tanks;


        public TankUI(string formName = "Default") : base(formName)
        {
            Tanks = new List<TargetTypes.Tank>();
            CreateResource();
        }

        ~TankUI()
        {

        }


        public override void Update(DemoTime demoTime)
        {

        }

        public override void Draw()
        {
            base.Draw();

            D2dRenderTarget.DrawRectangle(new RawRectangleF(20, 20, Mainform.ClientSize.Width - 20, Mainform.ClientSize.Height - 20), DefaultBrush);

            //D2dRenderTarget.FillGeometry(RenderTank, DefaultBrush, null);

            foreach (var item in Tanks)
            {
                using (var defTank = new RectangleGeometry(D2dFactory, new RawRectangleF(item.Rect.Left, item.Rect.Top, item.Rect.Right, item.Rect.Bottom)))
                {
                    D2dRenderTarget.FillGeometry(defTank, DefaultBrush, null);
                }
            }






            //D2dRenderTarget.DrawGeometry(RenderTank, DefaultBrush);

        }

        public override void KeyBordCallBack(KeyEventArgs e)
        {
            base.KeyBordCallBack(e);
        }

        public override void MouseCallBack(MouseEventArgs e)
        {
            base.MouseCallBack(e);
        }

        public void CreateResource()
        {
            WinX = Mainform.ClientSize.Width;
            WinY = Mainform.ClientSize.Height;
            RenderTank = new RectangleGeometry(D2dFactory, new RawRectangleF(WinX / 2 - 20, WinY - 60, WinX / 2 + 20, WinY - 20));

            Tanks.Add(new TargetTypes.Tank("P_01", new TargetTypes.RECT(WinX / 2 - 20, WinY - 60, WinX / 2 + 20, WinY - 20), 1, (long)Color.Yellow));





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
