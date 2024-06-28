using Daily_Diary_App;
using System;
using System.IO;
using Xunit;

namespace Daily_Diary_AppTest
{
    public class DailyDiaryTests : IDisposable
    {
        private readonly string _testFilePath;

        public DailyDiaryTests()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.txt");
        }

        public void Dispose()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Fact]
        public void ReadDiaryFile_ShouldReadContentSuccessfully()
        {
            // Arrange
            File.WriteAllLines(_testFilePath, new[] { "2024-01-01", "Entry 1", "2024-01-02", "Entry 2" });
            var diaryManager = new DailyDiary(_testFilePath);

            // Act
            var entries = diaryManager.LoadEntriesFromFile();

            // Assert
            Assert.Equal(2, entries.Count);
            Assert.Contains(entries, e => e.Date == new DateTime(2024, 1, 1) && e.Content == "Entry 1");
            Assert.Contains(entries, e => e.Date == new DateTime(2024, 1, 2) && e.Content == "Entry 2");
        }

        [Fact]
        public void AddEntry_ShouldIncreaseEntryCount()
        {
            // Arrange
            var diaryManager = new DailyDiary(_testFilePath);
            var initialCount = diaryManager.CountEntries();
            var newEntry = new Entry(DateTime.Now, "New Entry");

            // Act
            diaryManager.AddEntry(newEntry);
            var finalCount = diaryManager.CountEntries();

            // Assert
            Assert.Equal(initialCount + 1, finalCount);
        }
    }
}
