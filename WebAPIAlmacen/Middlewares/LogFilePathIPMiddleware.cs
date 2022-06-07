namespace WebAPIAlmacen.Middlewares
{
    public class LogFilePathIPMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;

        public LogFilePathIPMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }
        // Invoke o InvokeAsync. Obligatorio en un middleware
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var IP = httpContext.Connection.RemoteIpAddress.ToString();
            //if (IP == "::1") // Bloquearía las peticiones de una IP. En este caso representa localhost.
            //{
            //    httpContext.Response.StatusCode = 400;
            //    return;
            //}
            var ruta = httpContext.Request.Path.ToString();

            var path = $@"{env.ContentRootPath}\wwwroot\log.txt";
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine($@"{IP} - {ruta}");
            }

            await next(httpContext);
        }
    }
}
