using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using System;
using System.IO;
using System.Text.Json;
namespace ConsoleApp1
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper instence = new HtmlHelper();

        public static HtmlHelper instenceOut => instence;

        public string[] HtmlTags { get; private set; }

        public string[] HtmlVoidTags { get; private set; }



        private HtmlHelper()
        {
            HtmlTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("HtmlTags.json"));
            HtmlVoidTags = JsonSerializer.Deserialize<string[]>(File.ReadAllText("HtmlVoidTags.json"));
        }

        public bool IsTag(string tagName) => Array.Exists(HtmlTags, t => t.Equals(tagName, StringComparison.OrdinalIgnoreCase));
        public bool IsHtmlVoidTags(string tagName) => Array.Exists(HtmlVoidTags, t => t.Equals(tagName, StringComparison.OrdinalIgnoreCase));

    }
}
