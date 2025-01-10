namespace Player_Type;
using Utils;
using Pair_Type;
using GameBoard;



public class Player 
{
    public string? _name;
    public playerType _playerType;
    public int strength;
    public int agility;
    public int intelligence;
    public Pair position = new Pair(0,0);

    public Player(string? name, playerType type)
    {
        _name = name;
        _playerType = type;
        strength = Utils.ClassStats[type][playerStat.Strength];
        agility = Utils.ClassStats[type][playerStat.Agility];
        intelligence = Utils.ClassStats[type][playerStat.Itelligence];
    }

    public static bool IsValidName(string? name)
    {
        if(name == null) 
        {
            Console.Clear();
            Console.WriteLine("Necesitas un nombre...");
            return false;
        }
        if(name.Contains(' ')) 
        {
            Console.Clear();
            Console.WriteLine("No puede contener espacios vacíos");
            return false;
        }
        if(name.Length < 3 || name.Length > 10)
        {
            Console.Clear();
            Console.WriteLine("El tamaño del nombre debe contener entre 3 y 10 caracteres");
            return false;
        }
        
        return true;
    }
    public void Move(Maze board, moveDirection direction)
    {
        if(ValidMove(board, direction))
        {
            board[position] = CellType.Road;
            position += Utils.Directions[direction];
            board[position] = CellType.Player;
        }
    }
    public bool ValidMove(Maze board, moveDirection direction)
    {
        Pair posiblePosition = position + Utils.Directions[direction];
        return board[posiblePosition] != CellType.Wall;
    }
}
