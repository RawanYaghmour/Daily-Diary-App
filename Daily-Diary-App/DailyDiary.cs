using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Daily_Diary_App
{
    public class DailyDiary
    {
        private readonly string mainfilePath;

        public DailyDiary(string filePath)
        {
            mainfilePath = filePath;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Daily Diary Manager Application!");
            Console.WriteLine("This application allows you to manage your daily diary entries with ease.");
            Console.WriteLine("You can read, add, delete, count, and search your diary entries.");
            Thread.Sleep(1000);


            bool exit = false;

            while (!exit)
            {
                DisplayMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    HandleUserChoice(choice, ref exit);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("\n\nYou should choose an option from 1-7 :");
            Console.WriteLine("1- Read Diary ");
            Console.WriteLine("2- Add Entry ");
            Console.WriteLine("3- Delete Entry ");
            Console.WriteLine("4- Count Entries ");
            Console.WriteLine("5- Search Entries by Date ");
            Console.WriteLine("6- Search Entries by Keyword ");
            Console.WriteLine("7- Exit ");
            Console.Write("Your option is : ");
        }

        private void HandleUserChoice(int choice, ref bool exit)
        {
            try
            {
                switch (choice)
                {
                    case 1:
                        ReadDiary();
                        Thread.Sleep(1000);

                        break;
                    case 2:
                        AddEntry();
                        Thread.Sleep(1000);

                        break;
                    case 3:
                        DeleteEntry();
                        Thread.Sleep(1000);

                        break;
                    case 4:
                        TotalNumperOfEntries();
                        Thread.Sleep(1000);

                        break;
                    case 5:
                        SearchEntriesByDate();
                        Thread.Sleep(1000);

                        break;
                    case 6:
                        SearchEntriesByKeyword();
                        Thread.Sleep(1000);

                        break;
                    case 7:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        Thread.Sleep(1000);

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ReadDiary()
        {
            try
            {
                var entries = LoadEntriesFromFile();
                foreach (var entry in entries)
                {
                    Console.WriteLine(entry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading: {ex.Message}");
            }
        }

        public void AddEntry()
        {
            try
            {
                Console.Write("Enter the date (YYYY-MM-DD) : ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.Write("Enter the content: ");
                    var content = Console.ReadLine();
                    SaveEntry(new Entry(date, content));
                    Console.WriteLine("Added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding: {ex.Message}");
            }
        }

        public void DeleteEntry()
        {
            try
            {
                Console.Write("Enter the date (YYYY-MM-DD) of the entry to delete: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    RemoveEntry(date);
                    Console.WriteLine("Deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting: {ex.Message}");
            }
        }

        public void TotalNumperOfEntries()
        {
            try
            {
                Console.WriteLine($"Total entries: {CountEntries()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in counting: {ex.Message}");
            }
        }

        public void SearchEntriesByDate()
        {
            try
            {
                Console.Write("Enter the date (YYYY-MM-DD) to retrieve entries: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    var entries = FindEntriesByDate(date);
                    foreach (var entry in entries)
                    {
                        Console.WriteLine(entry);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching by date: {ex.Message}");
            }
        }

        public void SearchEntriesByKeyword()
        {
            try
            {
                Console.Write("Enter keyword to search: ");
                var keyword = Console.ReadLine();
                var entries = FindEntriesByKeyword(keyword);
                foreach (var entry in entries)
                {
                    Console.WriteLine(entry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching by keyword: {ex.Message}");
            }
        }

        public List<Entry> LoadEntriesFromFile()
        {
            var entries = new List<Entry>();

            if (File.Exists(mainfilePath))
            {
                var lines = File.ReadAllLines(mainfilePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (DateTime.TryParse(lines[i], out DateTime date) && i + 1 < lines.Length)
                    {
                        entries.Add(new Entry(date, lines[++i]));
                    }
                }
            }

            return entries;
        }


        private void SaveEntry(Entry entry)
        {
            using (var writer = new StreamWriter(mainfilePath, true))
            {
                writer.WriteLine(entry.Date.ToString("yyyy-MM-dd"));
                writer.WriteLine(entry.Content);
            }
        }

        private void RemoveEntry(DateTime date)
        {
            var entries = LoadEntriesFromFile();
            var updatedEntries = entries.Where(e => e.Date != date).ToList();

            using (var writer = new StreamWriter(mainfilePath))
            {
                foreach (var entry in updatedEntries)
                {
                    writer.WriteLine(entry.Date.ToString("yyyy-MM-dd"));
                    writer.WriteLine(entry.Content);
                }
            }
        }

        private int CountEntries()
        {
            return LoadEntriesFromFile().Count;
        }

        private List<Entry> FindEntriesByDate(DateTime date)
        {
            return LoadEntriesFromFile().Where(e => e.Date == date).ToList();
        }

        private List<Entry> FindEntriesByKeyword(string keyword)
        {
            return LoadEntriesFromFile().Where(e => e.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
