using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Core
{
    public class Exterd
    {
        public delegate void Callback();//定义回调

        public delegate void Callback<T>(T obj);//定义回调

    }


}
