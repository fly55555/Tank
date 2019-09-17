using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System;
using System.Diagnostics;
using System.Threading;
using Tank.App;
using Tank.Core;

namespace Tank
{
    internal static class Program
    {
        //static long taskId = 0;

        //static Direct2D d2d;

        [STAThread]
        private static void Main()
        {

            using (var Tank = new TankUI("钧天道"))
            {
                Tank.Run();
            }



            //Tank.Run();





            //d2d = new Direct2D("万剑归宗");

            //var whiteBrush = new SolidColorBrush(d2d.d2dRenderTarget, Color.White);
            //var blackBrush = new SolidColorBrush(d2d.d2dRenderTarget, Color.Black);
            //var redBrush = new SolidColorBrush(d2d.d2dRenderTarget, Color.Red);

            //var bush_White = d2d.Add(whiteBrush);
            //var bush_Black = d2d.Add(blackBrush);
            //var bush_Red = d2d.Add(redBrush);


            //var solidColorBrush = new SolidColorBrush(d2d.D2dRenderTarget, Color.White);
            //var rectangleGeometry = new RoundedRectangleGeometry(d2d.D2dFactory, new RoundedRectangle() { RadiusX = 32, RadiusY = 32, Rect = new RectangleF(128, 128, d2d.Mainform.ClientSize.Width - 128 * 2, d2d.Mainform.ClientSize.Height - 128 * 2) });


            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();




            //TextFormat TextFormat = new TextFormat(d2d.FactoryDWrite, "Calibri", 128) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };

            //TextLayout TextLayout = new TextLayout(d2d.FactoryDWrite, " 1", TextFormat, d2d.Mainform.Width, d2d.Mainform.Height);


            //taskId = d2d.RenderQueneAdd(()=>
            //{

            //    d2d.D2dRenderTarget.DrawTextLayout(new Vector2(0, 0), TextLayout, solidColorBrush, DrawTextOptions.None);
            //    //solidColorBrush.Color = new Color4(1, 1, 1, (float)Math.Abs(Math.Cos(stopwatch.ElapsedMilliseconds * .001)));
            //    //d2d.D2dRenderTarget.FillGeometry(rectangleGeometry, solidColorBrush, null);
            //});

            //dd.Test();

            //d2d.Run();

            return;
        }



    }
}

