using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Text;

namespace LifeSpot
{
    public static class EndpointMapper
    {
        /// <summary>
        /// Мэппинг СSS-файлов
        /// </summary>
        /// <param name="bilder"></param>
        public static void MapCss(this IEndpointRouteBuilder bilder)
        {
            var cssFile = new[] { "index.css" };

            foreach (var fileName in cssFile) 
            {
                bilder.MapGet($"/ Static / CSS /{fileName}", async context =>
                {
                    var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "CSS", fileName);

                    var css = await File.ReadAllTextAsync(cssPath);
                    await context.Response.WriteAsync(css);
                });
            }
        }
        /// <summary>
        /// Мэппинг JS
        /// </summary>
        /// <param name="bilder"></param>
        public static void MapJs(this IEndpointRouteBuilder bilder)
        {
            var jsFile = new[] { "index.js", "testing.js", "about.js" };

            foreach(var fileName in jsFile)
            {
                bilder.MapGet($"/Static/JS/{fileName}", async context =>
                {
                    var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", fileName);

                    var js = await File.ReadAllTextAsync(jsPath);
                    await context.Response.WriteAsync(js);
                });
            } 
        }
        /// <summary>
        ///  Маппинг Html-страниц
        /// </summary>
        public static void MapHtml(this IEndpointRouteBuilder builder)
        {
            string footerHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "footer.html"));
            string sideBarHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "sidebar.html"));
            string sliderHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "slider.html"));

            builder.MapGet("/about", async context =>
            {
                var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "about.html");

                var html = new StringBuilder(await File.ReadAllTextAsync(viewPath))
                    .Replace("<!--SIDEBAR-->", sideBarHtml)
                    .Replace("<!--FOOTER-->", footerHtml)
                    // Добавим для загрузки слайдера
                    .Replace("<!--SLIDER-->", sliderHtml);

                await context.Response.WriteAsync(html.ToString());
            });

            builder.MapGet("/testing", async context =>
            {
                var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "testing.html");

                // Загружаем шаблон страницы, вставляя в него элементы
                var html = new StringBuilder(await File.ReadAllTextAsync(viewPath))
                    .Replace("<!--SIDEBAR-->", sideBarHtml)
                    .Replace("<!--FOOTER-->", footerHtml);

                await context.Response.WriteAsync(html.ToString());
            });

            builder.MapGet("/about", async context =>
            {
                var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "about.html");

                // Загружаем шаблон страницы, вставляя в него элементы
                var html = new StringBuilder(await File.ReadAllTextAsync(viewPath))
                    .Replace("<!--SIDEBAR-->", sideBarHtml)
                    .Replace("<!--FOOTER-->", footerHtml);

                await context.Response.WriteAsync(html.ToString());
            });
        }
    }
}
