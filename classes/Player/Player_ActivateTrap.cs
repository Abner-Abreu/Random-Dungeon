namespace Player_Utils;
using Game_Board;

public partial class Player
{
    public void ActivateTrap(GameBoard board)
    {
        var dice = new Random();
        if (board[position] == CellType.Trap_Hiden || board[position] == CellType.Trap_Visible)
        {
            Action activedTrap = dice.Next(3) switch
            {
                0 => EnergyDrainTrap,
                1 => AntiManaTrap,
                2 => PastChange,
            };
            activedTrap.Invoke();
            _numberOfActivatedTraps++;
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
        foreach(playerType type in Enum.GetValues(typeof(playerType)))
        {
            if(type != _playerType)
            {
                posibleTypes.Add(type);
            }
        }
        var newRol = posibleTypes[dice.Next(posibleTypes.Count)];
        _playerType = newRol;
        _playerHability = ClassHability[newRol];
    }
}