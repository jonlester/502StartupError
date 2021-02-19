using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _502StartupError.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IFoo _foo;
        private readonly IHostApplicationLifetime _appLifetime;
        public HomeController( IFoo foo, IHostApplicationLifetime appLifetime)
        {           
            /// Test 1: Ifoo singleton is injected (see Foo constructor).  The first time this controller is instantiated
            /// the front end will return a 502.3.12002 status because it will time out.  Subsequent
            /// requests work fine (as expected)
            _foo = foo;
            _appLifetime = appLifetime;

            /// Test 2: Instantiate our own Foo instance.  This always results in a 502.3.12002

            //var foo2 = new Foo();
        }

        [HttpGet]
        public async Task<string> Get()
        {
            /// Test 3: simulate a really slow, external api call or other process
            /// this also causes a 502.3.12002 from the front end
            /// Test 3b: Restart the web app while this request is still pending
            /// results in a 502.3.12030 error

            //await Task.Delay(5 * 60 * 1000);

            /// Test 4: use up a bunch of TCP ports.Not disposing the client to
            ///  keep it around a little longer.Open a few browser tabs to get a few
            ///  requests going.They will stop responding fairly quickly. 
            ///  Although this did cause outbound SNAT port exhausting fairly quickly,
            ///  It did not seem to affect inbound connections.Rather, it pegged the
            ///  CPU % and resulted in the same timeout 502.3.12002

            //Task.Run(() =>
            //{
            //    for (int i = 0; i < 10000; i++)
            //    {
            //        try
            //        {
            //            var client = new HttpClient();
            //            var _ = client.GetAsync("https://management.azure.com/");
            //        }
            //        catch { }
            //    }
            //});

            /// Test 4: just abort the connection.  This will result in code 502.3.12030
            /// in the real world this can happen under a heavy load or load test

            //HttpContext.Abort();


            return DateTime.UtcNow.ToString("u");
        }
    }
}

