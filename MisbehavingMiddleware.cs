using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _502StartupError
{
    public class MisbehavingMiddleware
    {
        private readonly RequestDelegate _next;

        public MisbehavingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            /// Test 1: Instead of calling the next middleware, just do nothing
            /// Do not call _next().  This produces 502.3.12030 error
            await _next(context);
            /// Test 2: Kill the request.  This also results in a 502.3.12030 error

            //var feature = context.Features
            //    .Where(x=> x.Key == typeof(IHttpRequestLifetimeFeature))
            //    .Select(x => x.Value)
            //    .First() as IHttpRequestLifetimeFeature;

            //feature.Abort();

            

        }
    }
    public static class MisbehavingMiddlewareExtensions
    {
        public static IApplicationBuilder UseMisbehavingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MisbehavingMiddleware>();
        }
    }
}
