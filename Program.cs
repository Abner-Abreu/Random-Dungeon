using Game_Board;
using Player_Utils;
using Spectre.Console;
class Program
{
    public static void WaitAnimation(string message)
    {
        for (int i = 0; i < 3; i++)
        {
            Console.Clear();
            Console.Write(message);
            Thread.Sleep(500);
            for (int j = 0; j < 3; j++)
            {
                Console.Write('.');
                Thread.Sleep(250);
            }
        }
    }

    
    static void Main()
    {
        Console.Clear();
        var font = FigletFont.Load(@"fonts\delta_corps_priest.flf");
        AnsiConsole.Write(
            new FigletText(font, "Random Dungeon")
            .Centered()
            .Color(Color.White)
        );
        Console.ReadKey();

        #region Game Mode Selection
        var mapSizeMenu = new SelectionPrompt<string>()
            .Title("Seleccione el tamaño del mapa")
            .AddChoices("Pequeño", "Medio", "Grande");
        MapSize mapSize = AnsiConsole.Prompt(mapSizeMenu) switch
        {
            "Pequeño" => MapSize.Small,
            "Medio" => MapSize.Medium,
            "Grande" => MapSize.Large
        };

        var startMenu = new SelectionPrompt<string>()
            .Title("[green]Seleccione la cantidad de jugadores: [/]")
            .AddChoices("Dos Jugadores", "Cuatro Jugadores");
        int numberOfPlayers = AnsiConsole.Prompt(startMenu) switch
        {
            "Dos Jugadores" => 2,
            "Cuatro Jugadores" => 4
        };
        #endregion

        #region Character Creation
        List<Player> playersGroup = new List<Player>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            Console.Clear();
            var insertName = new TextPrompt<string>($"Introduce el numbre del [yellow]Jugador {i + 1}[/]: ")
                .Validate(name =>
                {
                    if (name == null)
                    {
                        return ValidationResult.Error("[red]Necesitas un nombre...[/]");
                    }
                    if (name.Contains(' '))
                    {
                        return ValidationResult.Error("[red]El nombre no puede contener espacios[/]");
                    }
                    if (name.Length < 3 || name.Length > 10)
                    {
                        return ValidationResult.Error("[red]El nombre debe contener entre 3 y 10 caracteres[/]");
                    }
                    if (playersGroup.Count > 0)
                    {
                        foreach (Player player in playersGroup)
                        {
                            if (name.ToUpper() == player._name.ToUpper())
                            {
                                return ValidationResult.Error("[red]No pueden haber nombres repetidos[/]");
                            }
                        }
                    }
                    return ValidationResult.Success();
                });
            string name = AnsiConsole.Prompt(insertName);

            var classMenu = new SelectionPrompt<string>()
                .Title($"[yellow]{name}[/] selecciona una Clase: ")
                .AddChoices(["Guerrero", "Mago", "Explorador", "Invocador", "Viajero"]);

            playerType type = AnsiConsole.Prompt(classMenu) switch
            {
                "Guerrero" => playerType.Warrior,
                "Mago" => playerType.Mage,
                "Explorador" => playerType.Explorer,
                "Invocador" => playerType.Summoner,
                "Viajero" => playerType.Traveler,
            };
            playersGroup.Add(new Player(name, type));
        }
        #endregion

        #region Map Generation
        WaitAnimation("Generando Laberinto");

        GameBoard maze = mapSize switch
        {
            MapSize.Small => new GameBoard(21),
            MapSize.Medium => new GameBoard(31),
            MapSize.Large => new GameBoard(41)
        };


        WaitAnimation("Colocando Jugadores");
        if (numberOfPlayers == 2)
        {
            playersGroup[0].position = (1, 1);
            playersGroup[1].position = (maze._size.x, maze._size.y);
        }
        else
        {
            playersGroup[0].position = (1, 1);
            playersGroup[1].position = (1, maze._size.y);
            playersGroup[2].position = (maze._size.x, 1);
            playersGroup[3].position = (maze._size.x, maze._size.y);
        }
        maze.UpdatePlayersPosition(playersGroup);
        #endregion

        #region Turnos
        int numberOfTurns = 0;
        bool isVictoryAchieved = false;
        do
        {
            numberOfTurns++;
            foreach (Player activePlayer in playersGroup)
            {
                Console.Clear();
                Console.WriteLine();
                activePlayer._energy += 3;
                if (activePlayer._mana < 3)activePlayer._mana++;

                var turnMenu = new SelectionPrompt<string>()
                    .Title($"Turno de {activePlayer._name}")
                    .AddChoices(["Moverse", "Estado", "Usar Habilidad", "Terminar Turno"]);

                bool turnEnded = false;
                while (activePlayer._energy > 0 && turnEnded == false && isVictoryAchieved == false)
                {
                    Action action = AnsiConsole.Prompt(turnMenu) switch
                    {
                        "Moverse" => () => activePlayer.ShowMovement(maze, playersGroup),
                        "Estado" => activePlayer.Status,
                        "Usar Habilidad" => () => activePlayer.UseHability(maze, playersGroup),
                        "Terminar Turno" => () => turnEnded = true,
                    };
                    action.Invoke();
                    if(maze.RaceWinned(activePlayer))
                    {
                        isVictoryAchieved = true;
                        Console.Clear();
                        AnsiConsole.WriteLine($"[red]{activePlayer._name} HA GANADO LA PARTIDA[/]");
                        Console.ReadKey(false);
                        activePlayer.Status();
                        Console.ReadKey(false);
                    }
                }
            }
        } while (isVictoryAchieved == false);
        #endregion

        #region End Game
        Console.Clear();
        int totalOfWalkedCells = 0;
        int totalOfActivatedTraps = 0;
        foreach (Player player in playersGroup) 
        {
            totalOfWalkedCells += player._numberOfMoves;
            totalOfActivatedTraps += player._numberOfActivatedTraps;
        }
        
        var finalStats = new Table();
        finalStats.AddColumn("[blue]Resultados Finales[/]");
        finalStats.AddRow($"Turnos Jugados: [green]{numberOfTurns}[/]");
        finalStats.AddRow($"Casillas Recorridas (por todos los jugadores): [green]{totalOfWalkedCells}[/]");
        finalStats.AddRow($"Trampas Activadas (por todos los jugadores): [green]{totalOfActivatedTraps}[/]");
        finalStats.AddRow($"Trampas Sin Activar: [green]{maze.numberOfTraps - totalOfActivatedTraps}[/]");
        finalStats.Border = TableBorder.Rounded;
        finalStats.BorderColor(Color.Blue);

        AnsiConsole.Write(finalStats);
        #endregion
    }
}
