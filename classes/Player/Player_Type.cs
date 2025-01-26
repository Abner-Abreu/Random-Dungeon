namespace Player_Utils;
using Game_Board;
using Spectre.Console;

public partial class Player
{
    public int _numberOfMoves = 0;
    public int _numberOfActivatedTraps = 0;
    public string? _name;
    public playerType _playerType;
    public playerHability _playerHability;

    public int _energy = 0;
    public int _mana = 3;
    public (int x, int y) position;

    public Player(string? name, playerType type)
    {
        _name = name;
        _playerType = type;
        _playerHability = ClassHability[_playerType];
    }

    public void ShowMovement(GameBoard board, List<Player> playersGroup)
    {
        bool cancelMove = false;
        while (_energy > 0 && cancelMove == false)
        {
            Console.Clear();
            board.PrintMaze();
            Action move = Console.ReadKey(false).Key switch
            {
                ConsoleKey.UpArrow => () => Move(board, moveDirection.Up),
                ConsoleKey.DownArrow => () => Move(board, moveDirection.Down),
                ConsoleKey.RightArrow => () => Move(board, moveDirection.Rigth),
                ConsoleKey.LeftArrow => () => Move(board, moveDirection.Left),
                ConsoleKey.Backspace => () => cancelMove = true,
                _ => () => { }
            };
            move.Invoke();
            board.UpdatePlayersPosition(playersGroup);
        }
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
            _energy--;
            _numberOfMoves++;
        }
    }
    public bool ValidMove(GameBoard board, (int x, int y) directionToMove)
    {
        (int x, int y) posiblePosition = (position.x + directionToMove.x, position.y + directionToMove.y);
        return board[posiblePosition] != CellType.Wall;
    }

    Dictionary<playerType, string> Class_Spanish = new Dictionary<playerType, string>()
    {
        {playerType.Warrior, "Guerrero"},
        {playerType.Mage, "Mago"},
        {playerType.Explorer, "Explorador"},
        {playerType.Summoner, "Invocador"},
        {playerType.Traveler, "Viajero"}
    };

    Dictionary<playerHability, string> Hability_Spanish = new Dictionary<playerHability, string>()
    {
        {playerHability.GoblinSummon, "Invocar Duende"},
        {playerHability.Instinct, "Instinto"},
        {playerHability.RefreshingBreeze, "Brisa Refrescante"},
        {playerHability.Swap, "Intercambio"},
        {playerHability.WallDestroyer, "Destructor de Muros"}
    };
    public void Status()
    {
        var stateTable = new Table();
        stateTable.Title = new TableTitle("[bold yellow]Estado[/]");
        stateTable.AddColumn($"{_name}").Centered();
        stateTable.AddRow($"Rol: {Class_Spanish[_playerType]}").LeftAligned();
        stateTable.AddRow($"Habilidad: {Hability_Spanish[_playerHability]}").LeftAligned();
        stateTable.AddRow($"Maná: {_mana}").LeftAligned();
        stateTable.AddRow($"Energía: {_energy}");
        stateTable.AddRow($"Distancia recorrida: {_numberOfMoves} casillas").LeftAligned();
        stateTable.Border = TableBorder.Rounded;
        Console.Clear();
        AnsiConsole.Write(stateTable);
        Console.ReadKey(false);
    }
}
