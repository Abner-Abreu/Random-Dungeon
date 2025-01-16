namespace Player_Utils;
using Utils;
using Pair_Type;
using Game_Board;



public partial class Player 
{
    public string? _name;
    public playerType _playerType;
    public playerHability _playerHability;

    public int numberOfMoves = 3;
    public Pair position = new Pair(0,0);

    public Player(string? name, playerType type)
    {
        _name = name;
        _playerType = type;
        _playerHability = Utils.ClassHability[_playerType];
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
    public void Move(GameBoard board, moveDirection direction)
    {
        if(ValidMove(board, direction))
        {
            board[position] = CellType.Road;
            position += Utils.Directions[direction];
            board[position] = CellType.Player;
        }
    }
    public bool ValidMove(GameBoard board, moveDirection direction)
    {
        Pair posiblePosition = position + Utils.Directions[direction];
        return board[posiblePosition] != CellType.Wall;
    }

    public bool IsInTheCenter(GameBoard board)
    {
        int centerIndex = (board._size+1)/2;
        if(position.first >= centerIndex - 1 && position.first <= centerIndex + 1
        && position.second >= centerIndex - 1 && position.second <= centerIndex +1)
        {
            return true;
        }
        return false;
    }
}
