namespace PlayerUtils;
using Others;
using MazeGenerator;
public class Player 
{
    public enum moveDirections
    {
        Up,
        Down,
        Rigth,
        Left
    }
    Dictionary<moveDirections,Pair> DIRECTIONS_TO_MOVE = new Dictionary<moveDirections, Pair>()
    {
        ///REVISAR DIRECCIONES EN EL LABERINTO
        ///Están cambiadas allá
        ///Mejorar la organización 
        ///YA SE MUEVEN!!! SIUUU!!!
        {moveDirections.Up, new Pair(0,1)},
        {moveDirections.Down, new Pair(0,-1)},
        {moveDirections.Rigth, new Pair(1,0)},
        {moveDirections.Left, new Pair(-1,0)}
    };
    public Pair position = new Pair(0,0);
    public string _name;
    public string _playerClass;

    #region Stats
        int strength;
        int agility;
        int intelligence;
        int vitality;
    #endregion

    public Player(string name, string playerClass)
    {
        if (playerClass == null) throw new Exception("playerClass can't be a null expretion");
        if (name == null) throw new Exception("name can't be a null expretion");

        _name = name;
        
        switch (playerClass)
        {
            case "Warrior":
            {
                strength = 5;
                agility = 3;
                intelligence = 2;
                vitality = 4;
                break;
            }
            case "Mage":
            {
                strength = 2;
                agility = 4;
                intelligence = 5;
                vitality = 3;
                break;
            }
            case "Explorer":
            {
                strength = 3;
                agility = 5;
                intelligence = 4;
                vitality = 2;
                break;
            }
        }
        _playerClass = playerClass;
    }

    public static bool IsValidName(string name)
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
        if(name.Length < 3)
        {
            Console.Clear();
            Console.WriteLine("El tamaño del nombre debe contener mínimo 3 caracteres");
            return false;
        }
        if(name.Length > 10)
        {
            Console.Clear();
            Console.WriteLine("El tamaño del nombre debe contener máximo 10 caracteres");
            return false;
        }
        return true;
    }
    public void Move(GameBoard board, moveDirections direction)
    {
        if(ValidMove(board, direction))
        {
            board[position] = GameBoard.CellType.Road;
            position += DIRECTIONS_TO_MOVE[direction];
            board[position] = GameBoard.CellType.Player;
        }
    }
    public bool ValidMove(GameBoard board, moveDirections direction)
    {
        Pair posiblePosition = position + DIRECTIONS_TO_MOVE[direction];
        return board[posiblePosition] != GameBoard.CellType.Wall;
    }
}
