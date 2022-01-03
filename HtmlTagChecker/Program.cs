using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlTagChecker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var html = @"<B><CENTER>This should be centered boldface, but there is a missing tag</CENTER>";

            var gg = Check(html);
            Console.WriteLine(gg);
            Console.ReadKey();
        }

        public static string Check(string html)
        {
            var tags = SeperateTags(html);
            var stack = new Stack<string>();

            for (int i = 0; i < tags.Count; i++)
            {
                if (!tags[i].Contains("</"))
                    stack.Push(tags[i]);

                if (tags[i].Contains("</"))
                {
                    if (stack.Count == 0)
                    {
                        return "Expected # found " + tags[i];
                    }

                    else
                    {
                        var previous = stack.Pop();
                        if (!IsBothTagSamePain(previous, tags[i]))
                            return "Expected " + previous.Replace("<", "</") + " found " + tags[i];
                    }
                }
            }
            if (stack.Count == 0)
                return "Correctly tagged paragraph";
            else
            {
                return "Expected " + stack.Pop().Replace("<", "</") + " found #";
            }
        }

        static Boolean IsBothTagSamePain(string tag1, string tag2)
        {
            if (tag1 == tag2.Replace("/", ""))
                return true;
            else
                return false;
        }

        public static List<string> SeperateTags(string html)
        {
            List<string> tagList = new List<string>();
            bool tagOpen = false;
            var tags = String.Empty;
            for (int i = 0; i < html.Length; i++)
            {

                if (tagOpen)
                {
                    if (html[i] == '<')
                    {
                        tags = "";
                        tagOpen = false;
                        continue;
                    }
                }

                if (html[i] == '<')
                {
                    tagOpen = true;
                }

                if (tagOpen)
                {
                    if (html[i] == '\\')
                    {
                        tags = "";
                        tagOpen = false;
                        continue;
                    }
                    tags += html[i];
                }

                if (tagOpen && html[i] == '>')
                {
                    tagOpen = false;
                    tagList.Add(tags);
                    tags = "";
                }
            }
            return tagList;
        }
    }

}
