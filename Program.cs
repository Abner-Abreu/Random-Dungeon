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
        #region Players Number
        int numberOfPlayers = 0;
        var selectionActions = new Dictionary<string, Action>()
        {
            {"Dos Jugadores", () => numberOfPlayers = 2 },
            {"Cuatro Jugadores", () => numberOfPlayers = 4}
        };
        var startMenu = new SelectionPrompt<string>()
            .Title("[green]Seleccione la cantidad de jugadores: [/]")
            .AddChoices(selectionActions.Keys);
        selectionActions[AnsiConsole.Prompt(startMenu)].Invoke();
        #endregion

        #region Set Players
        Player[] playersGroup = new Player[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            Console.Clear();
            string? name;
            var insertName = new TextPrompt<string>($"Introduce el numbre del [yellow]Jugador {i + 1}[/]: ")
                .Validate(name =>
                {
                    if (name == null)
                    {
                        return ValidationResult.Error("[red]El nombre no puede estar vacío[/]");
                    }
                    if (name.Contains(' '))
                    {
                        return ValidationResult.Error("[red]El nombre no puede contener espacios[/]");
                    }
                    if (name.Length < 3 || name.Length > 10)
                    {
                        return ValidationResult.Error("[red]El nombre debe contener entre 3 y 10 caracteres[/]");
                    }
                    return ValidationResult.Success();
                });
            name = AnsiConsole.Prompt(insertName);

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

            playersGroup[i] = new Player(name, type);
        }
        #endregion

        #region Preeliminares

        WaitAnimation("Generando Laberinto");

        GameBoard maze = numberOfPlayers switch
        {
            2 => new GameBoard(15),
            4 => new GameBoard(31)
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
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Clear();
                Console.WriteLine();
                playersGroup[i].numberOfMoves += 3;
                if (playersGroup[i].habilityColdDown > 0) playersGroup[i].habilityColdDown--;
                
                var turnMenu = new SelectionPrompt<string>()
                    .Title($"Turno de {playersGroup[i]._name}")
                    .Title("Acciones: ")
                    .AddChoices(["Moverse", "Estado", "Usar Habilidad", "Terminar Turno"]);

                bool turnEnded = false;
                while (playersGroup[i].numberOfMoves > 0 && turnEnded == false)
                {
                    Action action = AnsiConsole.Prompt(turnMenu) switch
                    {
                        "Moverse" => () =>
                        {
                            bool cancelMove = false;
                            while (playersGroup[i].numberOfMoves > 0 && cancelMove == false)
                            {
                                Console.Clear();
                                maze.PrintMaze();
                                Action move = Console.ReadKey(false).Key switch
                                {
                                    ConsoleKey.UpArrow => () => playersGroup[i].Move(maze, moveDirection.Up),
                                    ConsoleKey.DownArrow => () => playersGroup[i].Move(maze, moveDirection.Down),
                                    ConsoleKey.RightArrow => () => playersGroup[i].Move(maze, moveDirection.Rigth),
                                    ConsoleKey.LeftArrow => () => playersGroup[i].Move(maze, moveDirection.Left),
                                    ConsoleKey.Backspace => () => cancelMove = true,
                                    _ => () => {}
                                };
                                move.Invoke();
                            }
                        },
                        "Usar Habilidad" => () =>
                        {
                            if (playersGroup[i].habilityColdDown == 0)
                            {
                                Action useHability = playersGroup[i]._playerHability switch
                                {
                                    playerHability.WallDestroyer => () => playersGroup[i].WallDestroyer(maze),
                                    playerHability.Swap => () => playersGroup[i].Swap(playersGroup),
                                    playerHability.Instinct => () => playersGroup[i].Instinct(maze),
                                    playerHability.GoblinSummon => () => playersGroup[i].GoblinSummon(maze),
                                    playerHability.RefreshingBreeze => () => playersGroup[i].RefreshingBreeze(),
                                };
                                useHability.Invoke();
                                playersGroup[i].habilityColdDown = 3;
                            }
                            else
                            {
                                Console.WriteLine($"No puedes usar tu habilidad hasta dentro de {playersGroup[i].habilityColdDown} turnos");
                                Console.ReadKey(true);
                            }
                        },
                        "Terminar Turno" => () => turnEnded = true,
                    };
                    action.Invoke();
                }
                if (isVictoryAchieved)
                {
                    Console.Clear();
                    Console.WriteLine($"{playersGroup[i]._name} ha llegado al centro del laberinto");
                    Console.WriteLine($"Logró la victoria tras {numberOfTurns} turnos");
                    Console.WriteLine("FELICIDADES!!!");
                }
            }
        } while (isVictoryAchieved == false);
        #endregion
    }
}
