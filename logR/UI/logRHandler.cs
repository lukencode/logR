using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;
using System.IO;

namespace logR.UI
{
    public class logRHandler : IRouteHandler, IHttpHandler
    {
        internal static void RegisterRoutes()
        {
            var urls = new[] 
            {  
                "logr-viewer",
                "logr-style.css",
                "jquery.js",
                "signalR.js",
                "logr-conn.js",
                "tmpl.js"
            };

            var routes = RouteTable.Routes;
            var handler = new logRHandler();
            //var prefix = (MiniProfiler.Settings.RouteBasePath ?? "").Replace("~/", "").EnsureTrailingSlash();

            using (routes.GetWriteLock())
            {
                foreach (var url in urls)
                {
                    //var route = new Route(prefix + url, handler)
                    var route = new Route(url, handler)
                    {
                        // we have to specify these, so no MVC route helpers will match, e.g. @Html.ActionLink("Home", "Index", "Home")
                        Defaults = new RouteValueDictionary(new { controller = "logRHandler", action = "ProcessRequest" })
                    };

                    // put our routes at the beginning, like a boss
                    routes.Insert(0, route);
                }
            }
        }

        /// <summary>
        /// Returns this <see cref="MiniProfilerHandler"/> to handle <paramref name="requestContext"/>.
        /// </summary>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this; // elegant? I THINK SO.
        }

        /// <summary>
        /// Try to keep everything static so we can easily be reused.
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Returns either includes' css/javascript or results' html.
        /// </summary>
        public void ProcessRequest(HttpContext context)
        {
            string output;
            string path = context.Request.AppRelativeCurrentExecutionFilePath;

            switch (Path.GetFileNameWithoutExtension(path).ToLower())
            {
                case "logr-style":
                case "logr-conn":
                case "jquery":
                case "signalr":
                case "tmpl":
                    output = Includes(context, path);
                    break;

                case "logr-viewer":
                    output = FullPage(context);
                    break;

                default:
                    output = NotFound(context);
                    break;
            }

            context.Response.Write(output);
        }

        private static string FullPage(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            return GetResource("FullViewer.html");
        }

        /// <summary>
        /// Handles rendering static content files.
        /// </summary>
        private static string Includes(HttpContext context, string path)
        {
            var response = context.Response;

            switch (Path.GetExtension(path))
            {
                case ".js":
                    response.ContentType = "application/javascript";
                    break;
                case ".css":
                    response.ContentType = "text/css";
                    break;
                case ".tmpl":
                    response.ContentType = "text/x-jquery-tmpl";
                    break;
                default:
                    return NotFound(context);
            }

            var cache = response.Cache;
            cache.SetCacheability(System.Web.HttpCacheability.Public);
            cache.SetExpires(DateTime.Now.AddDays(7));
            cache.SetValidUntilExpires(true);

            var embeddedFile = Path.GetFileName(path).Replace("mini-profiler-", "");
            return GetResource(embeddedFile);
        }
        
        private static string GetResource(string filename)
        {
            string result;

            if (!_ResourceCache.TryGetValue(filename, out result))
            {
                using (var stream = typeof(logRHandler).Assembly.GetManifestResourceStream("logR.UI." + filename))
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }

                _ResourceCache[filename] = result;
            }

            return result;
        }

        /// <summary>
        /// Embedded resource contents keyed by filename.
        /// </summary>
        private static readonly Dictionary<string, string> _ResourceCache = new Dictionary<string, string>();

        /// <summary>
        /// Helper method that sets a proper 404 response code.
        /// </summary>
        private static string NotFound(HttpContext context, string contentType = "text/plain", string message = null)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = contentType;

            return message;
        }

    }
}
