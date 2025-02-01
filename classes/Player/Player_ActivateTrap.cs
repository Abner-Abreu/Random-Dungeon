namespace Player_Utils;
using Game_Board;

public partial class Player
{
    public void ActivateTrap(GameBoard board, List<Player> players)
    {
        var dice = new Random();
        if (board[position] == CellType.Trap_Hiden || board[position] == CellType.Trap_Visible)
        {
            Action activedTrap = dice.Next(5) switch
            {
                0 => EnergyDrainTrap,
                1 => AntiManaTrap,
                2 => PastChange,
                3 => ()=> TrapsRespawnTrap(board),
                4 => ()=> TeleportTrap(board)
            };
            activedTrap.Invoke();
            board.UpdatePlayersPosition(players);
            _numberOfActivatedTraps++;
            Console.WriteLine();
            Console.WriteLine("[PRESIONE UNA TECLA PARA CONTINUAR]");
            Console.ReadKey(false);
        }
    }

    public void EnergyDrainTrap()
    {
        Console.Clear();
        Console.WriteLine("Sientes como tu energía se escapa de tu ti...");
        _energy -= 3;
    }

    public void AntiManaTrap()
    {
        Console.Clear();
        Console.WriteLine("Sientes tu maná escapar de tu cuerpo...");
        _mana -= 3;
    }

    public void PastChange()
    {
        var dice = new Random();
        Console.Clear();
        Console.WriteLine("Visiones de una vida pasada pasan por tu mente, algo ha cambiado dentro de ti...");
        List<playerType> posibleTypes = new List<playerType>();
        foreach (playerType type in Enum.GetValues(typeof(playerType)))
        {
            if (type != _playerType)
            {
                posibleTypes.Add(type);
            }
        }
        var newRol = posibleTypes[dice.Next(posibleTypes.Count)];
        _playerType = newRol;
        _playerHability = ClassHability[newRol];
    }

    public void TrapsRespawnTrap(GameBoard board)
    {
        Console.Clear();
        Console.WriteLine("Sientes un gran temblor y sientes que ocurrió un gran cambio, nuevos peligros acechan...");
        board.numberOfTraps = 0;
        for (int y = 0; y <= board._size.y; y++)
        {
            for (int x = 0; x <= board._size.x; x++)
            {
                if (board[(y, x)] == CellType.Trap_Hiden || board[(y, x)] == CellType.Trap_Visible)
                {
                    board[(y, x)] = CellType.Road;
                }
            }
        }
        board.SetTraps();
    }

    public void TeleportTrap(GameBoard board)
    {
        Console.Clear();
        Console.WriteLine("Se activa un círculo mágico a tus pies y te empiezas a sentir mareado, cuando te recuperas estás en un lugar completamente distinto...");
        var dice = new Random();
        if (position.x > board._size.x / 5 && position.y > board._size.y / 5 && position.x < board._size.x - (board._size.x / 5) && position.y < board._size.x - (board._size.y / 5)) /// Interior
        {
            do
            {
                position = (dice.Next(board._size.x / 5, board._size.x - (board._size.x / 5)), dice.Next(board._size.y / 5, board._size.y - (board._size.y / 5)));
            } while (board.RaceWinned(this) || board[position] == CellType.Wall);
        }
        else if (position.x > board._size.x / 3 && position.y > board._size.y / 3 &&
            position.x < board._size.x - (board._size.x / 3) && position.y < board._size.y - (board._size.y / 3)) /// Middle
        {
            do
            {
                position = (dice.Next(board._size.x / 3, board._size.x - (board._size.x / 3)), dice.Next(board._size.y / 3, board._size.y - (board._size.y / 3)));
            } while (board.RaceWinned(this) || board[position] == CellType.Wall);
        }
        else /// Exterior
        {
            do
            {
                position = (dice.Next(1, board._size.x / 3), dice.Next(1, board._size.y / 3));
            } while (board.RaceWinned(this) || board[position] == CellType.Wall);
        }
    }
}