

using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using ConsoleApp1;




async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
//string allHtml =await Load("https://gemini.google.com/app/dd03f210296a52a9");
//var cleanHtml = Regex.Replace(allHtml, @"\s+", " "); 
//var lineHtml = new Regex("<.*?>").Split(allHtml).Where(s => s.Length > 0); ;
//var htmlelement = "<div id=\"myId\" class=\"myClass1 myClass2\" width=\"100\"></div>";
//var attributes = new Regex("^[ ]*?=\".*?\"").Matches(htmlelement);


Console.WriteLine("🔍 Loading HTML...");

string html = await Load("https://netfree.link/shortcuts/");

Console.WriteLine("📦 Parsing HTML...");
var parser = new HtmlParser();
HtmlElement root = parser.Parse(html);

Console.WriteLine("🔎 Querying...");


var selector = Selector.Parse(".infont");
var results = root.Query(selector);

Console.WriteLine($"Found {results.Count()} results:");
foreach (var el in results)
    Console.WriteLine(el);

