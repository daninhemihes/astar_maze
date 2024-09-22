using AstarMaze.App.Infrastructure.Repositories;
using AstarMaze.App.Domain.ValueObjects;
using AstarMaze.App.Domain.Enums;

namespace AstarMaze.Tests
{
    public class MazeRepositoryTests
    {
        [Fact]
        public void mustReturnExceptionWhenFileDoesbNotExist()
        {
            var repository = new MazeRepository();
            string nonExistentFile = "nonexistent_file.txt";

            Assert.Throws<FileNotFoundException>(() => repository.LoadMaze(nonExistentFile));

        }

        [Fact]
        public void LoadMaze_ShouldReturnCorrectMaze_WhenFileIsValid()
        {
            var repository = new MazeRepository();
            string testFilePath = "valid_maze.txt";

            File.WriteAllText(testFilePath, "*E*\n  *\nH**");

            Maze result = repository.LoadMaze(testFilePath);

            Assert.NotNull(result);
            Assert.Equal(3, result.Positions.GetLength(0));
            Assert.Equal(3, result.Positions.GetLength(1));

            Assert.Equal(PositionType.Human, result.Positions[0,0].Type); 
            Assert.Equal(PositionType.Wall, result.Positions[1,0].Type); 
            Assert.Equal(PositionType.Wall, result.Positions[2,0].Type); 
            Assert.Equal(PositionType.Empty, result.Positions[0,1].Type); 
            Assert.Equal(PositionType.Empty, result.Positions[1,1].Type); 
            Assert.Equal(PositionType.Wall, result.Positions[2,1].Type); 
            Assert.Equal(PositionType.Wall, result.Positions[0,2].Type); 
            Assert.Equal(PositionType.Entry, result.Positions[1,2].Type);
            Assert.Equal(PositionType.Wall, result.Positions[2,2].Type);

            File.Delete(testFilePath);
        }

        [Fact]
        public void LoadMaze_ShouldThrowException_WhenMazeDoesNotContainEntryOrHuman()
        {
            var repository = new MazeRepository();
            string testFilePath = "invalid_maze.txt";

            File.WriteAllText(testFilePath, 
                "* *\n" +
                "* *\n" +
                "***"); 

            var exception = Assert.Throws<ArgumentException>(() => repository.LoadMaze(testFilePath));

            Assert.Equal("No entry position found in maze.", exception.Message);

            File.Delete(testFilePath);
        }

        [Fact]
        public void LoadMaze_ShouldThrowException_WhenMazeContainsInvalidCharacter()
        {
            var repository = new MazeRepository();
            string testFilePath = "invalid_characters_maze.txt";

            File.WriteAllText(testFilePath, 
                "*E*\n" +
                "A *\n" + 
                "*H*");

            var exception = Assert.Throws<ArgumentException>(() => repository.LoadMaze(testFilePath));
            Assert.Contains("Invalid character", exception.Message); 

            File.Delete(testFilePath);
        }
    }
}