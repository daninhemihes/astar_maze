using AstarMaze.App.Domain.ValueObjects;
using AstarMaze.App.Domain.Enums;


namespace AstarMaze.App.Infrastructure.Repositories
{
    public class MazeRepository
    {
        public Maze LoadMaze(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Arquivo não encontrado: {filePath}");
            }

            string[] lines = File.ReadAllLines(filePath);
            Array.Reverse(lines);

            Maze maze = CreateMaze(lines);
            return maze;        
        }

        public Maze CreateMaze(string[] lines)
        {
            if (lines == null || lines.Length == 0)
            {
                throw new ArgumentException("Invalid input lines for maze.");
            }

            int height = lines.Length;
            int width = lines.Max(line => line.Length);

            Position[,] positions = new Position[width, height];

            Position? entryPosition = null;
            Position? humanPosition = null;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    char currentChar = lines[i][j];

                    if (currentChar != 'H' && currentChar != 'E' && currentChar != '*' && currentChar != ' ')
                    {
                        throw new ArgumentException($"Invalid character '{currentChar}' in maze.");
                    }

                    PositionType positionType = (PositionType)(int)currentChar;

                    if (positionType == PositionType.Entry)
                    {
                        entryPosition = new Position(j, i, positionType);
                    }
                    else if (positionType == PositionType.Human)
                    {
                        humanPosition = new Position(j, i, positionType);
                    }

                    positions[j,i] = new Position(j, i, positionType);
                }
            }

            if (entryPosition == null) throw new ArgumentException("No entry position found in maze.");
            if (humanPosition == null) throw new ArgumentException("No human position found in maze.");

            return new Maze(positions, entryPosition, humanPosition);
        }

    }
}
