using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; } = new List<string>();

        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public static Selector Parse(string query)
        {
            var parts = query.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            Selector root = null;
            Selector current = null;

            foreach (var part in parts)
            {
                var sel = new Selector();
                var segments = System.Text.RegularExpressions.Regex.Split(part, @"(?=[#\.])");

                foreach (var seg in segments)
                {
                    if (string.IsNullOrWhiteSpace(seg)) continue;
                    if (seg.StartsWith("#")) sel.Id = seg.Substring(1);
                    else if (seg.StartsWith(".")) sel.Classes.Add(seg.Substring(1));
                    else sel.TagName = seg;
                }

                if (root == null) root = sel;
                if (current != null) current.Child = sel;
                sel.Parent = current;
                current = sel;
            }

            return root;
        }
    }
}

