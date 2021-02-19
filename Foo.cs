using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _502StartupError
{
    public interface IFoo { }
    public class Foo : IFoo
    {
        public Foo()
        {
            //simulate a very long initialization
            //Thread.Sleep(5 * 60 * 1000);
        }
    }
}
