using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.MVC
{
    public static class HtmlHelperExtensions
    {
        public static ScriptContext BeginScriptContext(this HtmlHelper htmlHelper)
        {
            var scriptContext = new ScriptContext(htmlHelper.ViewContext.HttpContext);
            htmlHelper.ViewContext.HttpContext.Items[ScriptContext.ScriptContextItem] = scriptContext;
            return scriptContext;
        }

        public static void EndScriptContext(this HtmlHelper htmlHelper)
        {
            var items = htmlHelper.ViewContext.HttpContext.Items;
            var scriptContext = items[ScriptContext.ScriptContextItem] as ScriptContext;

            scriptContext?.Dispose();
        }

        public static void AddScriptBlock(this HtmlHelper htmlHelper, string script)
        {
            var scriptGroup = htmlHelper.ViewContext.HttpContext.Items[ScriptContext.ScriptContextItem] as ScriptContext;

            if (scriptGroup == null)
                throw new InvalidOperationException("cannot add a script block without a script context. Call Html.BeginScriptContext() beforehand.");

            scriptGroup.ScriptBlocks.Add(script);
        }

        public static void AddScriptFile(this HtmlHelper htmlHelper, string path)
        {
            var scriptGroup = htmlHelper.ViewContext.HttpContext.Items[ScriptContext.ScriptContextItem] as ScriptContext;

            if (scriptGroup == null)
                throw new InvalidOperationException("cannot add a script file without a script context. Call Html.BeginScriptContext() beforehand.");

            scriptGroup.ScriptFiles.Add(path);
        }

        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            var scriptContexts = htmlHelper.ViewContext.HttpContext.Items[ScriptContext.ScriptContextItems] as Stack<ScriptContext>;

            if (scriptContexts != null)
            {
                var count = scriptContexts.Count;
                var builder = new StringBuilder();
                var script = new List<string>();
                var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);

                for (int i = 0; i < count; i++)
                {
                    var scriptContext = scriptContexts.Pop();

                    foreach (var scriptFile in scriptContext.ScriptFiles)
                    {
                        builder.AppendLine("<script type='text/javascript' src='" + urlHelper.Content(scriptFile) + "'></script>");
                    }

                    script.AddRange(scriptContext.ScriptBlocks);

                    // render out all the scripts in one block on the last loop iteration
                    if (i == count - 1)
                    {
                        builder.AppendLine("<script type='text/javascript'>");
                        foreach (var s in script)
                        {
                            builder.AppendLine(s);
                        }
                        builder.AppendLine("</script>");
                    }
                }

                return new MvcHtmlString(builder.ToString());
            }

            return MvcHtmlString.Empty;
        }
    }

    public class ScriptContext : IDisposable
    {
        internal const string ScriptContextItems = "ScriptContexts";
        internal const string ScriptContextItem = "ScriptContext";

        private readonly HttpContextBase _httpContext;

        public ScriptContext(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            _httpContext = httpContext;
        }

        public IList<string> ScriptBlocks { get; } = new List<string>();

        public HashSet<string> ScriptFiles { get; } = new HashSet<string>();

        public void Dispose()
        {
            var items = _httpContext.Items;
            var scriptContexts = items[ScriptContextItems] as Stack<ScriptContext> ?? new Stack<ScriptContext>();

            // remove any script files already the same as the ones we're about to add
            foreach (var scriptContext in scriptContexts)
            {
                scriptContext.ScriptFiles.ExceptWith(ScriptFiles);
            }

            scriptContexts.Push(this);

            items[ScriptContextItems] = scriptContexts;
        }
    }
}