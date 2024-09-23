using AstarMaze.App.Domain.ValueObjects;
using AstarMaze.App.Domain.Enums;


namespace AstarMaze.App.Infrastructure.Repositories
{
    public class MazeRepository : IMazeRepository
    {
        public Maze LoadMaze(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Simulator", fileName);
            Console.WriteLine($"Tentando carregar o arquivo: {filePath}");

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

                    positions[j, i] = new Position(j, i, positionType);
                }
            }

            if (entryPosition == null) throw new ArgumentException("No entry position found in maze.");
            if (humanPosition == null) throw new ArgumentException("No human position found in maze.");
            PrintMaze(positions);

            return new Maze(positions, entryPosition, humanPosition);
        }
        public void PrintMaze(Position[,] positions)
        {
            int height = positions.GetLength(1);
            int width = positions.GetLength(0);
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    char symbol = ' ';
                    switch (positions[j, i].Type)
                    {
                        case PositionType.Entry:
                            symbol = 'E';
                            break;
                        case PositionType.Human:
                            symbol = 'H';
                            break;
                        case PositionType.Wall:
                            symbol = '*';
                            break;
                        case PositionType.Empty:
                            symbol = ' ';
                            break;
                    }
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }


    }
}
