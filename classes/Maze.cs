namespace MazeGenerator;
using Pair_Type;

public enum CellType
    {
        Wall,
        Road,
        Trap,
        Player
    }
public class GameBoard
{
    private CellType[,] _maze;
    private Random _random = new Random();

    public CellType this[Pair index]
    {
        get => _maze[index.first,index.second];
        set => _maze[index.first,index.second] = value;
    }

    public GameBoard(int width, int height)
    {
        _maze = new CellType[height+2, width+2];
        // Generate the maze starting from (1, 1)"
        GenerateMaze(1, 1);
    }

    private void GenerateMaze(int xPosition, int yPosition)
    {
        _maze[yPosition, xPosition] = CellType.Road; // Marck cell as road
        // Possible directions: right, down, left, up
        int[] xPossibleDirections = { 2, 0, -2, 0 };
        int[] yPossibleDirections = { 0, 2, 0, -2 };

        // Create a list of directions and shuffle it
        var directions = new (int x, int y)[4];
        for (int i = 0; i < 4; i++)
        {
            directions[i] = (yPossibleDirections[i], xPossibleDirections[i]);
        }

        // Shuffle directions 
        for (int i = directions.Length - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            var temp = directions[i];
            directions[i] = directions[j];
            directions[j] = temp;
        }

        foreach (var (xDirection, yDirection) in directions)
        {
            int xNewPosition = xPosition + xDirection;
            int yNewPosition = yPosition + yDirection;

            if (xNewPosition >= 0 && xNewPosition < _maze.GetLength(0) && 
                yNewPosition >= 0 && yNewPosition < _maze.GetLength(1) &&
                _maze[yNewPosition, xNewPosition] == CellType.Wall)
            {
                // Remove the wall between the current cell and the new cell
                _maze[yPosition + yDirection / 2, xPosition + xDirection / 2] = CellType.Road;
                GenerateMaze(xNewPosition, yNewPosition);
            }
        }
        CreateCycles(yPosition, xPosition);    
    }

    private void CreateCycles(int yPosition, int xPosition)
    {
        int[] xPossibleDirections = { 2, 0, -2, 0 };
        int[] yPossibleDirections = { 0, 2, 0, -2 };

        for (int i = 0; i < 4; i++)
        {
            int xNewPosition = xPosition + xPossibleDirections[i];
            int yNewPosition = yPosition + yPossibleDirections[i];

            // Check if the adjacent cell is visited
            if (xNewPosition >= 0 && xNewPosition < _maze.GetLength(0) 
                && yNewPosition >= 0 && yNewPosition < _maze.GetLength(1) 
                && _maze[yNewPosition, xNewPosition] == CellType.Road)
            {
                // Randomly decide to create a cycle
                if (_random.Next(26) == 0) // 10% chance to create a cycle
                {
                    // Remove the wall between the current cell and the adjacent visited cell
                    _maze[yPosition + yPossibleDirections[i] / 2, xPosition + xPossibleDirections[i] / 2] = CellType.Road;
                }
            }
        }
    }

    public void PrintMaze()
    {
        for (int x = 0; x < _maze.GetLength(0); x++)
        {
            string row = "";
            for (int y = 0; y < _maze.GetLength(1); y++)
            {
                switch (_maze[x,y])
                {
                    case CellType.Road:
                    {
                        row += "  ";
                        break;
                    }
                    case CellType.Wall:
                    {
                        row += "██";
                        break;
                    }
                    case CellType.Player:
                    {
                        row += "🙂";
                        break;
                    }
                }
            }
            Console.WriteLine(row);
        }
        Console.WriteLine();
    }
}