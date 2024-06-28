using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Diary_App
{
    public class Entry
    {

        public DateTime Date {get; set;}
        public string Content {get; set;}


        public Entry(DateTime date, string content)

        {

            Date = date;
            Content = content;
        }

        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd}" +
                   $"\n{Content} ";
        }
    }
}
