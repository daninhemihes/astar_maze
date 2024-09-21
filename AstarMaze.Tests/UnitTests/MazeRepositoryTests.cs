using AstarMaze.App.Infrastructure.Repositories;
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
        public void LoadMaze_ShouldReturnCorrectMatrix_WhenFileIsValid()
        {
            var repository = new MazeRepository();
            string testFilePath = "valid_maze.txt";

            File.WriteAllText(testFilePath, "##\n# ");

            char[,] result = repository.LoadMaze(testFilePath);

            Assert.NotNull(result);
            Assert.Equal(2, result.GetLength(0));
            Assert.Equal(2, result.GetLength(1));

            Assert.Equal('#', result[0, 0]);
            Assert.Equal('#', result[0, 1]);
            Assert.Equal('#', result[1, 0]);
            Assert.Equal(' ', result[1, 1]);

            File.Delete(testFilePath);
        }

    }
}