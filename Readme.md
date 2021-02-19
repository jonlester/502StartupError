Http 502 or "Bad Gateway" errors from an app deployed to Azure App Services are often incorrectly attributed to issues with the platform itself.  In reality, most of these errors occur because of issues in the code, transient startup problems, or not correctly handling platform events.

Below are some scenarios that will cause 502 errors from your code.  To test them, uncomment a scenario in one of the code files below and deploy as an Azure App Service.


WinHttp error code 12002 is a timeout, 12030 is a request that got interrupted/aborted.  The 502.3 status/substatus is returned by the http platform handler on the front end loadbalancer

Program.cs 
- kill host process after a time delay while requests are in flight.  Result 502.3.12030

Foo.cs
- Add initialization delay to constructor.  Deferred singleton initialization via IoC result is 502.3.12002 on first request.  Per-call initialization results in 502.3.12002 per call.

MisbehavingMiddleware.cs
- Simulate a swallowed exception that fails to call the next component in the pipeline results in 502.3.12030
- Abort the current request results in 502.3.12030

HomeController.cs
- Simulate a very long running process result is 502.3.12002
- Recycle workers while long running process is in-flight result is 502.3.12030
- Run an async task to exhaust ports/cpu result is 502.3.12002.  Note that Outbound TCP SNAT ports hit limit.  Inbound was unaffected.  CPU exhaustion resulted in timeouts.
- Abort current request result is 502.3.12030
