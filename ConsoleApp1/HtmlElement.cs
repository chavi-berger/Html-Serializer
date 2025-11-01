using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
   
 
        public class HtmlElement
        {

            public string Id { get; set; }
            public string Name { get; set; }
            public List<string> Classes { get; set; } = new List<string>();
            public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
            public string InnerHtml { get; set; } = "";

            public HtmlElement Parent { get; set; }
            public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();

            public override string ToString()
            {
                return $"<{Name}{(Id != null ? $" id='{Id}'" : "")}{(Classes.Count > 0 ? $" class='{string.Join(" ", Classes)}'" : "")}>";
            }

            // -----------------------------------------------------------------------------------
            // שלב 6: פונקציות הריצה על העץ והחיפוש לפי Selector
            // -----------------------------------------------------------------------------------

            /// <summary>
            /// מחזירה את כל הצאצאים (מכל הרמות) של האלמנט הנוכחי.
            /// </summary>
            public IEnumerable<HtmlElement> Descendants()
            {
                var queue = new Queue<HtmlElement>();
                queue.Enqueue(this);

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    yield return current;

                    foreach (var child in current.Children)
                        queue.Enqueue(child);
                }
            }

            /// <summary>
            /// מחזירה את כל האבות (מכל הדורות) של האלמנט הנוכחי.
            /// </summary>
            public IEnumerable<HtmlElement> Ancestors()
            {
                var current = this.Parent;
                while (current != null)
                {
                    yield return current;
                    current = current.Parent;
                }
            }

            /// <summary>
            /// פונקציית חיפוש לפי סלקטור (CSS Selector)
            /// </summary>
            internal IEnumerable<HtmlElement> Query(Selector selector)
            {
                var result = new HashSet<HtmlElement>();
                QueryRecursive(this, selector, result);
                return result;
            }

            /// <summary>
            /// פונקציה רקורסיבית פנימית המבצעת את החיפוש בעץ.
            /// </summary>
            private void QueryRecursive(HtmlElement root, Selector selector, HashSet<HtmlElement> result)
            {
                var matches = root.Descendants()
                    .Where(e =>
                        (selector.TagName == null || e.Name == selector.TagName) &&
                        (selector.Id == null || e.Id == selector.Id) &&
                        (!selector.Classes.Any() || selector.Classes.All(c => e.Classes.Contains(c)))
                    ).ToList();

                if (selector.Child == null)
                {
                    foreach (var match in matches)
                        result.Add(match);
                }
                else
                {
                    foreach (var match in matches)
                        QueryRecursive(match, selector.Child, result);
                }
            }
        }
    }




