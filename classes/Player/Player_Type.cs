namespace Player_Utils;
using Game_Board;
public partial class Player
{
    public string? _name;
    public playerType _playerType;
    public playerHability _playerHability;

    public int numberOfMoves = 0;
    public int habilityColdDown = 0;
    public (int x, int y) position;

    public Player(string? name, playerType type)
    {
        _name = name;
        _playerType = type;
        _playerHability = ClassHability[_playerType];
    }

    public void Move(GameBoard board, moveDirection direction)
    {
        (int x, int y) directionToMove = Directions[direction];
        if (ValidMove(board, directionToMove))
        {
            board[position] = CellType.Road;
            position = (position.x + directionToMove.x, position.y + directionToMove.y); ;
            ActivateTrap(board);
            board[position] = CellType.Player;
            numberOfMoves--;
        }
    }
    public bool ValidMove(GameBoard board, (int x, int y) directionToMove)
    {
        (int x, int y) posiblePosition = (position.x + directionToMove.x, position.y + directionToMove.y);
        return board[posiblePosition] != CellType.Wall;
    }
}
