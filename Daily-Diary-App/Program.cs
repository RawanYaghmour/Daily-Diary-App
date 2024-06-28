using System;
using System.IO;


namespace Daily_Diary_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            string filePath = Path.Combine(Environment.CurrentDirectory, "mydiary.txt");
            var diaryManager = new DailyDiary(filePath);

            diaryManager.Start();
        
        }
    }
}
