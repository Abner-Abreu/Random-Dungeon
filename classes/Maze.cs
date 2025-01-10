﻿namespace MazeGenerator;
using Pair_Type;
using Utils;

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
    public int _size { get; private set; }
    private Random _random = new Random();

    public CellType this[Pair index]
    {
        get => _maze[index.second,index.first];
        set => _maze[index.second,index.first] = value;
    }
    public GameBoard(int size)
    {
        _size = size;
        _maze = new CellType[size+2, size+2]; ///y,x
        // Generate the maze starting from (1, 1)"
        GenerateMaze(1,1);
        SetCenterRoom();
    }

    Pair[] possibleDirections = { 
        Utils.Directions[moveDirection.Up] * 2,
        Utils.Directions[moveDirection.Down] * 2,
        Utils.Directions[moveDirection.Rigth] *2,
        Utils.Directions[moveDirection.Left] * 2
    };
    private void GenerateMaze(int xPosition, int yPosition)
    {
        _maze[yPosition, xPosition] = CellType.Road; // Marck cell as road

        // Create a list of directions and shuffle it
        var directions = new (int x, int y)[4];
        for (int i = 0; i < 4; i++)
        {
            directions[i] = (possibleDirections[i].second, possibleDirections[i].first);
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
        for (int i = 0; i < 4; i++)
        {
            int xNewPosition = xPosition + possibleDirections[i].first;
            int yNewPosition = yPosition + possibleDirections[i].second;

            // Check if the adjacent cell is visited
            if (xNewPosition >= 0 && xNewPosition < _maze.GetLength(0) 
                && yNewPosition >= 0 && yNewPosition < _maze.GetLength(1) 
                && _maze[yNewPosition, xNewPosition] == CellType.Road)
            {
                // Randomly decide to create a cycle
                if (_random.Next(11) == 0) // 10% chance to create a cycle
                {
                    // Remove the wall between the current cell and the adjacent visited cell
                    _maze[yPosition + possibleDirections[i].second / 2, xPosition + possibleDirections[i].first / 2] = CellType.Road;
                }
            }
        }
    }

    public void SetCenterRoom()
    {
        int centerIndex = (_maze.GetLength(0) - 1)/2;
        for (int i = centerIndex - 1; i <= centerIndex + 1; i++)
        {
            for (int j = centerIndex - 1; j <= centerIndex + 1; j++)
            {
                _maze[i, j] = CellType.Road;
            }
        }
    }
    private void SetTraps()
    {
        
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