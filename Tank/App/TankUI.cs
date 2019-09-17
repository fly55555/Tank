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
        public TankUI(string formName = "Default") : base(formName)
        {
            CreateResource();
        }

        ~TankUI()
        {

        }


        public override void Draw()
        {
            base.Draw();









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
            TextFormat textFormat = new TextFormat(FactoryDWrite, "宋体", 128) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };

            TextLayout textLayout = new TextLayout(FactoryDWrite, " 1", textFormat, Mainform.Width, base.Mainform.Height);


            //var rectangleGeometry = new RoundedRectangleGeometry(D2dFactory, new RoundedRectangle() { RadiusX = 32, RadiusY = 32, Rect = new RectangleF(128, 128, Mainform.ClientSize.Width - 128 * 2, Mainform.ClientSize.Height - 128 * 2) });


            var rectangleGeometry  = new RectangleGeometry(D2dFactory, new RawRectangleF(128, 128, Mainform.ClientSize.Width - 128 * 2, Mainform.ClientSize.Height - 128 * 2));


            var taskId = RenderQueneAdd(() =>
            {

                //D2dRenderTarget.DrawTextLayout(new Vector2(0, 0), textLayout, SolidColorBrush_[Color.Red], DrawTextOptions.None);
                //solidColorBrush.Color = new Color4(1, 1, 1, (float)Math.Abs(Math.Cos(stopwatch.ElapsedMilliseconds * .001)));
                D2dRenderTarget.FillGeometry(rectangleGeometry, SolidColorBrush_[Color.Red], null);
            });



        }








    }
}
