namespace Player_Utils;
using Game_Board;
using Spectre.Console;

public partial class Player
{

    public void UseHability(GameBoard board, List<Player> playersGroup)
    {
        if (_mana >= 3)
        {
            Action useHability = _playerHability switch
            {
                playerHability.WallDestroyer => () => WallDestroyer(board),
                playerHability.Swap => () => Swap(playersGroup),
                playerHability.Instinct => () => Instinct(board),
                playerHability.GoblinSummon => () => GoblinSummon(board),
                playerHability.RefreshingBreeze => RefreshingBreeze,
                _ => ()=>{}
            };
            useHability.Invoke();
            _mana -= 3;
        }
        else
        {
            Console.WriteLine($"Necesitas {3 - _mana} puntos de maná más para activar tu habilidad");
            Console.WriteLine();
            Console.WriteLine("[PRESIONE UNA TECLA PARA CONTINUAR]");
            Console.ReadKey(true);
        }
    }
    private (int x, int y)[] posibleMoves = new (int x, int y)[4];

    private void SetPosibleMoves()
    {
        posibleMoves[0] = (position.x + Directions[moveDirection.Up].x, position.y + Directions[moveDirection.Up].y);
        posibleMoves[1] = (position.x + Directions[moveDirection.Down].x, position.y + Directions[moveDirection.Down].y);
        posibleMoves[2] = (position.x + Directions[moveDirection.Rigth].x, position.y + Directions[moveDirection.Rigth].y);
        posibleMoves[3] = (position.x + Directions[moveDirection.Left].x, position.y + Directions[moveDirection.Left].y);
    }
    public void WallDestroyer(GameBoard maze)
    {
        SetPosibleMoves();
        List<(int, int)> destroyableWallPosition = new List<(int, int)>();
        int numberOfDestroyableWalls = 0;
        for (int i = 0; i < 4; i++)
        {
            if (maze[posibleMoves[i]] == CellType.Wall)
            {
                destroyableWallPosition.Add(posibleMoves[i]);
                numberOfDestroyableWalls++;
            }
        }
        Random dice = new Random();
        (int x, int y) wallToDestroy = destroyableWallPosition[dice.Next(numberOfDestroyableWalls)];
        if (IsBorderWall(maze._size, wallToDestroy))
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Intentas derribar una pared exterior de la mazmorra, no logras ni arañarla");
            Console.ResetColor();
        }
        else
        {
            maze[wallToDestroy] = CellType.Road;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Derribas la pared de un golpe, ahora puedes pasar");
            Console.ResetColor();
        }
    }
    private static bool IsBorderWall((int x, int y) mazeSize, (int x, int y) position)
    {
        if (position.x == 0 || position.x > mazeSize.x) return true; //Left and Right Border
        if (position.y == 0 || position.y > mazeSize.y) return true; //Top and Button Border
        return false;
    }

    public void Instinct(GameBoard board)
    {
        SetPosibleMoves();
        for (int i = 0; i < posibleMoves.Length; i++)
        {
            if (board[posibleMoves[i]] == CellType.Trap_Hiden)
            {
                board[posibleMoves[i]] = CellType.Trap_Visible;
            }
        }
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Trampas mostradas");
    }

    public void Swap(List<Player> playersGroup)
    {
        Random dice = new Random();
        int playerToSwap = dice.Next(playersGroup.Count);
        if (playersGroup[playerToSwap].position == position)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("El hechizo falla miserablemente... te quedas en tu sitio");
            Console.ResetColor();
        }
        else
        {
            (int x, int y) temp = playersGroup[playerToSwap].position;
            playersGroup[playerToSwap].position = position;
            position = temp;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("El hechizo funciona y eres transportado a un lugar mejor... tal vez");
            Console.ResetColor();
        }
    }

    public void RefreshingBreeze()
    {
        _energy += 2;
        Console.WriteLine("Te envuelve una brisa refrescante");
    }

    public void GoblinSummon(GameBoard board)
    {
        SetPosibleMoves();
        int disabledTraps = 0;
        foreach ((int x, int y) direction in posibleMoves)
        {
            if (direction.x <= board._size.x && direction.y <= board._size.y)
            {
                if (board[direction] == CellType.Trap_Hiden || board[direction] == CellType.Trap_Visible)
                {
                    board[direction] = CellType.Road;
                    disabledTraps++;
                }
            }
        }
        Console.Clear();
        Console.WriteLine($"Se desarmaron {disabledTraps} trampas");
    }
    public static Dictionary<moveDirection, (int x, int y)> Directions = new Dictionary<moveDirection, (int x, int y)>()
    {
        {moveDirection.Up, (0,-1)},
        {moveDirection.Down, (0,1)},
        {moveDirection.Rigth, (1,0)},
        {moveDirection.Left, (-1,0)}
    };
}
