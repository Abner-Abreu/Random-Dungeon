namespace Game_Board;
using Player_Utils;
using Spectre.Console;

public enum CellType
    {
        Wall,
        Road,
        Trap_Hiden,
        Trap_Visible,
        Player
    }

public enum MapSize
    {
        Small,
        Medium,
        Large,
        Massive
    }
public partial class GameBoard
{
    private CellType[,] _maze;

    public int numberOfTraps = 0;
    public (int x, int y) _size { get; private set; }
    private Random _random = new Random();
    
    public CellType this[(int x, int y) index]
    {
        get => _maze[index.y,index.x];
        set => _maze[index.y,index.x] = value;
    }
    public GameBoard(int size)
    {
        _size = (size,size);
        _maze = new CellType[size+2, size+2]; ///y,x
        // Generate the maze starting from (1, 1)
        GenerateMaze((1,1));
        SetTraps();
        SetCenterRoom();
    }
    (int x, int y)[] possibleDirections = {(0,-2),(0,2),(2,0),(-2,0)};
    private void GenerateMaze((int x, int y) position)
    {
        _maze[position.y, position.x] = CellType.Road; // Marck cell as road

        // Create a list of directions and shuffle it
        var directions = new (int x, int y)[4];
        for (int i = 0; i < 4; i++)
        {
            directions[i] = (possibleDirections[i].x, possibleDirections[i].y);
        }

        // Shuffle directions 
        for (int i = directions.Length - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            var temp = directions[i];
            directions[i] = directions[j];
            directions[j] = temp;
        }

        foreach ((int x, int y) direction in directions)
        {
            (int x, int y) newPosition = (position.x + direction.x, position.y + direction.y);

            if (newPosition.y >= 0 && newPosition.y < _maze.GetLength(0) - 1
            && newPosition.x >= 0 && newPosition.x < _maze.GetLength(1) - 1
            && _maze[newPosition.y, newPosition.x] == CellType.Wall)
            {
                // Remove the wall between the current cell and the new cell
                _maze[position.y + direction.y / 2, position.x + direction.x / 2] = CellType.Road;
                GenerateMaze(newPosition);
            }
        }
        CreateCycles(position);    
    }
    private void CreateCycles((int x, int y) position)
    {
        for (int i = 0; i < 4; i++)
        {
            int xNewPosition = position.x + possibleDirections[i].x;
            int yNewPosition = position.y + possibleDirections[i].y;

            // Check if the adjacent cell is visited
            if (xNewPosition > 0 && xNewPosition < _maze.GetLength(0) - 2 
                && yNewPosition > 0 && yNewPosition < _maze.GetLength(1) - 2
                && _maze[yNewPosition, xNewPosition] == CellType.Road)
            {
                // Randomly decide to create a cycle
                if (_random.Next(11) == 0) // 10% chance to create a cycle
                {
                    // Remove the wall between the current cell and the adjacent visited cell
                    _maze[position.y + possibleDirections[i].y / 2, position.x + possibleDirections[i].x / 2] = CellType.Road;
                }
            }
        }
    }

    public void SetCenterRoom()
    {
        int centerIndex = (_maze.GetLength(0) - 1)/2;
        for (int x = centerIndex - 1; x <= centerIndex + 1; x++)
        {
            for (int y = centerIndex - 1; y <= centerIndex + 1; y++)
            {
                _maze[x, y] = CellType.Road;
            }
        }
    }
    
    private void SetTraps()
    {
        for (int y = 1; y < _maze.GetLength(0) -1; y++)
        {
            for (int x = 1; x < _maze.GetLength(1) - 1; x++)
            {
                if(_maze[y,x] == CellType.Road && FreeSpaceForTrap(x, y))
                {
                    if (x > _size.x / 5 && y > _size.y / 5&&  x < _size.x - (_size.x / 5) && y < _size.x - (_size.y / 5))
                    {
                        if (_random.Next(100) <= 25)
                        { 
                            _maze[y, x] = CellType.Trap_Hiden; /// 25% chance Hard
                            numberOfTraps++;
                        } 
                    }
                    else if (x > _size.x / 3 && y > _size.y / 3 &&
                        x < _size.x - (_size.x / 3) && y < _size.y - (_size.y / 3))
                    {
                        if (_random.Next(100) <= 15)
                        { 
                            _maze[y, x] = CellType.Trap_Hiden; /// 15% chance Medium
                            numberOfTraps++;
                        } 
                    }
                    else
                    {
                        if (_random.Next(100) <= 10 ) 
                        { 
                            _maze[y, x] = CellType.Trap_Hiden; /// 10% chance Easy
                            numberOfTraps++;
                        } 
                    }      
                }
            }
        }
    }
    private bool FreeSpaceForTrap(int x, int y)
    {
        int checker = 0;
        if (_maze[x, y - 1] == CellType.Trap_Hiden) checker++; //Up
        if (_maze[x, y + 1] == CellType.Trap_Hiden) checker++; //Down
        if (_maze[x + 1, y] == CellType.Trap_Hiden) checker++; //Right
        if (_maze[x - 1, y] == CellType.Trap_Hiden) checker++; //Left
        return checker == 0;
    }

    public void UpdatePlayersPosition(List<Player> playersGroup)
    {
        foreach (Player player in playersGroup)
        {
            _maze[player.position.y, player.position.x] = CellType.Player;
        }
    }
    public void PrintMaze((int, int) activePlayerPosition)
    {
        for (int y = 0; y < _maze.GetLength(0); y++)
        {
            string row = "";
            for (int x = 0; x < _maze.GetLength(1); x++)
            {
                if(activePlayerPosition == (x,y))
                {
                    row += "[green]██[/]";
                    continue;
                }
                switch (_maze[y,x])
                {
                    case CellType.Road:
                    {
                        row += "▓▓";
                        break;
                    }
                    case CellType.Wall:
                    {
                        row += "[grey]██[/]";
                        break;
                    }
                    case CellType.Player:
                    {
                        row += "[yellow]██[/]";
                        break;
                    }
                    case CellType.Trap_Visible:
                    {
                        row += "[red]██[/]";
                        break;
                    }
                    case CellType.Trap_Hiden:
                    {
                        row += "▓▓";
                        break;
                    }
                }
            }
            AnsiConsole.MarkupLine(row);
        }
        Console.WriteLine();
    }
    
}