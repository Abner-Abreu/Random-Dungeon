namespace MazeGenerator;

public class GameBoard
{
    enum CellType
    {
        Wall,
        Road,
        Trap
    }
    private int _width;
    private int _height;
    private CellType[,] _maze;
    private Random _random = new Random();
    public GameBoard(int width, int height)
    {
        //Width and Height most be odd or the algorithm BREAK
        if(width % 2 == 0) width++;
        if(height % 2 == 0) height++;

        _width = width + 2;
        _height = height + 2;
        _maze = new CellType[_height, _width];

        

        // Generate the maze starting from (1, 1)"
        GenerateMaze(1, 1);
    }

    private void GenerateMaze(int xPosition, int yPosition)
    {
        //PrintMaze();
        _maze[xPosition, yPosition] = CellType.Road; // Marck cell as road

        // Possible directions: right, down, left, up
        int[] xPossibleDirections = { 2, 0, -2, 0 };
        int[] yPossibleDirections = { 0, 2, 0, -2 };

        // Create a list of directions and shuffle it
        var directions = new (int x, int y)[4];
        for (int i = 0; i < 4; i++)
        {
            directions[i] = (xPossibleDirections[i], yPossibleDirections[i]);
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

            if (xNewPosition >= 0 && xNewPosition < _height && 
                yNewPosition >= 0 && yNewPosition < _width &&
                _maze[xNewPosition, yNewPosition] == 0)
            {
                // Remove the wall between the current cell and the new cell
                _maze[xPosition + xDirection / 2, yPosition + yDirection / 2] = CellType.Road;
                GenerateMaze(xNewPosition, yNewPosition);
            }
        }
        CreateCycles(xPosition, yPosition);    
    }

    private void CreateCycles(int xPosition, int yPosition)
    {
        int[] xPossibleDirections = { 2, 0, -2, 0 };
        int[] yPossibleDirections = { 0, 2, 0, -2 };

        for (int i = 0; i < 4; i++)
        {
            int xNewPosition = xPosition + xPossibleDirections[i];
            int yNewPosition = yPosition + yPossibleDirections[i];

            // Check if the adjacent cell is visited
            if (xNewPosition >= 0 && xNewPosition < _height 
                && yNewPosition >= 0 && yNewPosition < _width 
                && _maze[xNewPosition, yNewPosition] == CellType.Road)
            {
                // Randomly decide to create a cycle
                if (_random.Next(26) == 0) // 10% chance to create a cycle
                {
                    // Remove the wall between the current cell and the adjacent visited cell
                    _maze[xPosition + xPossibleDirections[i] / 2, yPosition + yPossibleDirections[i] / 2] = CellType.Road;
                }
            }
        }
    }

    public void PrintMaze()
    {
        for (int x = 0; x < _height; x++)
        {
            string row = "";
            for (int y = 0; y < _width; y++)
            {
                row += _maze[x,y] == CellType.Road ? "  " : "██";
            }
            Console.WriteLine(row);
        }
        Console.WriteLine();
    }
}