using System.Diagnostics;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Device = SharpDX.Direct3D11.Device;
//using Factory = SharpDX.DXGI.Factory;
using System;
using System.Collections.Generic;
using static Tank.Core.Direct2D;
using System.Windows.Forms;
using static Tank.Core.Exterd;
using SharpDX.DirectInput;

namespace Tank.Core
{
    public class Direct2D : IDisposable
    {
        public Device D2Ddevice { get; set; }

        public DrawFrom Mainform { get; set; }

        public SwapChain SwapChain { get; set; }

        public SharpDX.DXGI.Factory DXGIFactory { get; set; }

        public SharpDX.Direct2D1.Factory D2dFactory { get; set; }

        public SharpDX.DirectWrite.Factory FactoryDWrite { get; set; }


        /************************************************/


        public Texture2D BackBuffer { get; set; }

        public RenderTarget D2dRenderTarget { get; set; }

        public RenderTargetView BackBufferView { get; set; }

        public List<Resource_2D> Resources { get; set; }

        public List<RenderQuene_2D> RenderQuene { get; set; }

        public SolidColorBrush DefaultBrush { get; set; }
        //public Dictionary<Color, SolidColorBrush> SolidColorBrush_ { get; set; }


        /************************************************/


        private readonly DemoTime Clock = new DemoTime();

        public float FramePerSecond { get; private set; }

        public float FrameDelta { get; set; }



        private float _frameAccumulator { get; set; }

        private int _frameCount { get; set; }


        /************************************************/

        private Keyboard _keybord { get; set; }



        /// <summary>
        /// 类初始化函数
        /// </summary>
        /// <param name="formName"></param>
        public Direct2D(string formName = "Default")
        {
            _keybord = new Keyboard(new DirectInput());
            _keybord.Acquire();

            RenderQuene = new List<RenderQuene_2D>();
            Resources = new List<Resource_2D>();

            Mainform = new DrawFrom(formName);       //创建一个渲染窗口
            Mainform.AllowUserResizing = false;      //设置不可调整大小
            Mainform.KeyCallBackSet(KeyBordCallBack);//设置键盘回调
            Mainform.MouseCallBackSet(MouseCallBack);//设置鼠标回调

            
            CreateSwapChain(CreateDesc(Mainform));       //创建设备和交换链
            D2dFactory = new SharpDX.Direct2D1.Factory();//Direct2D1.Factory 是D2D渲染工厂
            FactoryDWrite = new SharpDX.DirectWrite.Factory();//RW工厂
            DXGIFactory = SwapChain.GetParent<SharpDX.DXGI.Factory>();                           //DXGI.Factory 是基础渲染工厂
            DXGIFactory.MakeWindowAssociation(Mainform.Handle, WindowAssociationFlags.IgnoreAll);//关联窗口 并忽略所有事件

            // 从backbuffer新建renderTargetView
            BackBuffer = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0);
            BackBufferView = new RenderTargetView(D2Ddevice, BackBuffer);
            using (Surface surface = BackBuffer.QueryInterface<Surface>())
            {
                D2dRenderTarget = new RenderTarget(D2dFactory, surface, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
            }

            DefaultBrush = new SolidColorBrush(D2dRenderTarget, Color.White);


        }

        /// <summary>
        /// using释放函数
        /// </summary>
        public void Dispose()
        {
            ExitAndRelease();
        }

        /// <summary>
        /// 类销毁 析构函数
        /// </summary>
        ~Direct2D()
        {
            ExitAndRelease();
        }

        public void ExitAndRelease()
        {
            try
            {
                D2Ddevice.ImmediateContext.ClearState();//设备即时上下文清理状态
                D2Ddevice.ImmediateContext.Flush();     //设备即时上下文刷新
                D2Ddevice.Dispose();   //设备销毁
                D2Ddevice.Dispose();   //设备销毁
                SwapChain.Dispose();//基础交换链销毁
                DXGIFactory.Dispose();  //基础工厂销毁


                BackBufferView.Dispose();  //后备缓冲视图销毁
                BackBuffer.Dispose();      //后备缓冲销毁
            }
            catch (Exception)
            {
                return;
            }
        }



        public long ResourceAdd<T>(T obj)
        {
            var resource = new Resource_2D(obj);
            Resources.Add(resource);
            return resource.index;
        }

        public void ResourceDelete(long index)
        {
            var resource = Resources.Find(row => row.index == index);
            Resources.Remove(resource);
        }

        public T ResourceGet<T>(long index)
        {
            var resource = Resources.Find(row => row.index == index);
            return (T)resource.obj;
        }

        public void ResourceUpDate(Resource_2D row)
        {
            var resource = Resources.Find(o => o.index == row.index);
            resource.obj = row.obj;
        }






        public long RenderQueneAdd(Callback fun)
        {
            lock (RenderQuene)
            {
                var row = new RenderQuene_2D(fun);
                RenderQuene.Add(row);
                return row.index;
            }
        }

        public void RenderQueneDelete(long index)
        {
            lock (RenderQuene)
            {
                var item = RenderQuene.Find(row => row.index == index);
                RenderQuene.Remove(item);
            }
        }

        private void Render()
        {
            lock (RenderQuene)
            {
                foreach (var item in RenderQuene)
                {
                    item.fun();
                }
            }
        }


        public virtual void Draw()
        {

        }

        public virtual void Update(DemoTime time, KeyboardState key)
        {

        }


        private void OnUpDate()
        {
            //if (keyStatus.PressedKeys.Count > 0)
            //{
            //    Debug.WriteLine(keyStatus.PressedKeys[0].ToString());
            //}

            //keybord.Properties.BufferSize = 128;
            //keybord.Poll();
            //var datas = keybord.GetBufferedData();
            //foreach (var state in datas)
            //    Debug.WriteLine(state);


            var keyStatus = _keybord.GetCurrentState();
            FrameDelta = (float)Clock.Update();
            Update(Clock, keyStatus);

            _frameAccumulator += FrameDelta;
            ++_frameCount;
            if (_frameAccumulator >= 1.0f)
            {
                FramePerSecond = _frameCount / _frameAccumulator;

                Debug.WriteLine($"FPS:[{FramePerSecond}]");

                _frameAccumulator = 0.0f;
                _frameCount = 0;
            }


        }


        public virtual void MouseCallBack(MouseEventArgs e)
        {

        }

        public virtual void KeyBordCallBack(KeyEventArgs e)
        {

        }



        public void Run()
        {
            Clock.Start();
            RenderLoop.Run(Mainform, () =>
            {
                D2dRenderTarget.BeginDraw();
                D2dRenderTarget.Clear(Color.Gray);

                OnUpDate();
                Render();
                Draw();


                D2dRenderTarget.EndDraw();
                SwapChain.Present(0, PresentFlags.None);
            });
        }


        private SwapChainDescription CreateDesc(RenderForm form)
        {
            return new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription =
                       new ModeDescription(form.ClientSize.Width, form.ClientSize.Height,
                                           new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
        }

        private void CreateSwapChain(SwapChainDescription desc)
        {
            Device device_;
            SwapChain swapChain_;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, new SharpDX.Direct3D.FeatureLevel[] { SharpDX.Direct3D.FeatureLevel.Level_11_0 }, desc, out device_, out swapChain_);

            D2Ddevice = device_;
            SwapChain = swapChain_;
        }
    }



    public class Resource_2D
    {
        public long index { get; set; }

        public object obj { get; set; }

        public Resource_2D(object o, long i = 0)
        {
            obj = o;
            if (i == 0) i = DateTime.UtcNow.Ticks;
            index = i;
        }
    }

    public class RenderQuene_2D
    {
        public long index { get; set; }

        public Callback fun { get; set; }

        public RenderQuene_2D(Callback o, long i = 0)
        {
            fun = o;
            if (i == 0) i = DateTime.UtcNow.Ticks;
            index = i;
        }
    }





}
