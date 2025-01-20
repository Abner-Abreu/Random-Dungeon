namespace Player_Utils;
using Game_Board;
using Colorful;
using System.Drawing;

public partial class Player
{
    public void ActivateTrap(GameBoard board)
    {
        var dice = new Random();
        if (board[position] == CellType.Trap_Hiden || board[position] == CellType.Trap_Visible)
        {
            Action activedTrap = dice.Next(2) switch
            {
                0 => EnergyDrainTrap,
                1 => AntiManaTrap,
            };
            activedTrap.Invoke();
            Thread.Sleep(5000);
        }
    }

    public void EnergyDrainTrap()
    {
        Console.Clear();
        Console.WriteLine("Sientes como tu energía se escapa de tu ti...", Color.Azure);
        numberOfMoves -= 3;
    }

    public void AntiManaTrap()
    {
        Console.Clear();
        Console.WriteLine("Sientes tu maná escapar de tu cuerpo...", Color.Azure);
        habilityColdDown += 3;
    }

}