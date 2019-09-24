using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Tank.Core.Direct2D;
using static Tank.Core.Exterd;

namespace Tank.Core
{
    public class DrawFrom : RenderForm
    {
        /// <summary>
        /// 是否允许调整大小
        /// </summary>
        public new bool AllowUserResizing
        {
            get { return base.AllowUserResizing; }
            set { base.AllowUserResizing = value; }
        }

        /// <summary>
        /// 是否全屏
        /// </summary>
        public new bool IsFullscreen
        {
            get { return base.IsFullscreen; }
            set { base.IsFullscreen = value; }
        }


        public DrawFrom() : base()
        {

        }

        public DrawFrom(string text) : base(text)
        {

        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DrawFrom
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "DrawFrom";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DrawFrom_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawFrom_MouseClick);
            this.ResumeLayout(false);
        }

        /********************************************************************/

        private Callback<KeyEventArgs> KeyCallBack { get; set; }

        private Callback<MouseEventArgs> MouseCallBack { get; set; }



        public void KeyCallBackSet(Callback<KeyEventArgs> fun)
        {
            KeyCallBack = fun;
        }

        public void MouseCallBackSet(Callback<MouseEventArgs> fun)
        {
            MouseCallBack = fun;
        }

        private void DrawFrom_MouseClick(object sender, MouseEventArgs e)
        {
            MouseCallBack(e);
        }

        private void DrawFrom_KeyDown(object sender, KeyEventArgs e)
        {
            KeyCallBack(e);
        }
    }
}
