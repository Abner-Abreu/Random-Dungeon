using Game_Board;
using Player_Utils;
using Spectre.Console;
using Tutorials;
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
        #region Game Start
        Console.Clear();
        Console.CursorVisible = false;
        string fontPosition = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "fonts", "delta_corps_priest.flf");
        var font = FigletFont.Load(fontPosition);
        Action ShowTitle = ()=> AnsiConsole.Write(
            new FigletText(font, "Random Dungeon")
            .LeftJustified()
            .Color(Color.Blue)
        );

        var startMenu = new SelectionPrompt<string>()
            .AddChoices("Jugar", "Tutorial", "Salir");
        
        bool newGame = false;
        while (newGame == false)
        {
            Console.Clear();
            ShowTitle.Invoke();
            Console.WriteLine();
            Action startMenuAction = AnsiConsole.Prompt(startMenu) switch
            {
                "Jugar" => () => newGame = true,
                "Tutorial" => Tutorial.ShowTutorial,
                "Salir" => ()=> {Environment.Exit(0);}
            };
            startMenuAction.Invoke();
        }
        #endregion

        #region Game Mode Selection
        var numberOfPlayersMenu = new SelectionPrompt<string>()
            .Title("[green]Seleccione la cantidad de jugadores: [/]")
            .AddChoices("Dos Jugadores", "Cuatro Jugadores");
        int numberOfPlayers = AnsiConsole.Prompt(numberOfPlayersMenu) switch
        {
            "Dos Jugadores" => 2,
            "Cuatro Jugadores" => 4
        };

        var mapSizeMenu = new SelectionPrompt<string>()
            .Title("Seleccione el tamaño del mapa")
            .AddChoices("Pequeño", "Medio", "Grande");
        MapSize mapSize = AnsiConsole.Prompt(mapSizeMenu) switch
        {
            "Pequeño" => MapSize.Small,
            "Medio" => MapSize.Medium,
            "Grande" => MapSize.Large
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
                    if (name is null)
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
            Tutorial.ShowClasses();
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
            MapSize.Small => new GameBoard(15),
            MapSize.Medium => new GameBoard(25),
            MapSize.Large => new GameBoard(35)
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
                maze.PrintMaze(activePlayer.position);
                if (activePlayer._energy <= 0) activePlayer._energy += 3;
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
                        AnsiConsole.Write(new FigletText(font, "VICTORIA")
                            .LeftJustified()
                            .Color(Color.Green));
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
        finalStats.BorderColor(Color.Blue);

        AnsiConsole.Write(finalStats);
        #endregion
    }
}
