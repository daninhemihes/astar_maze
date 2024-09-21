
namespace AstarMaze.App.Infrastructure.Repositories
{
    public class MazeRepository
    {
        public char[,] LoadMaze(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Arquivo não encontrado: {filePath}");
            }

            string[] lines = File.ReadAllLines(filePath);

            int maxColumns = GetMaxColumns(lines);

            char[,] maze = CreateMazeMatrix(lines, maxColumns);

            return maze;
        }

        private int GetMaxColumns(string[] lines)
        {
            int maxColumns = 0;
            foreach (var line in lines)
            {
                if (line.Length > maxColumns)
                {
                    maxColumns = line.Length;
                }
            }
            return maxColumns;
        }

        private char[,] CreateMazeMatrix(string[] lines, int maxColumns)
        {
            char[,] maze = new char[lines.Length, maxColumns];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    maze[i, j] = lines[i][j];
                }

                for (int j = lines[i].Length; j < maxColumns; j++)
                {
                    maze[i, j] = ' ';
                }
            }

            return maze;
        }
    }
}
