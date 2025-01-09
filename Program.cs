using MazeGenerator;
using PlayerUtils;
using Others;
class Program
{
    enum playerClass {
        Warrior,
        Mage,
        Explorer
    }
    static void Main()
    {
        #region Number of Players
        int numberOfPlayers = 0;
        Console.Clear();
            while (numberOfPlayers != 2 && numberOfPlayers != 4)
            {
                Console.WriteLine("Escoja el número de Jugadores: ");
                Console.WriteLine("1- Dos jugadores");
                Console.WriteLine("2- Cuatro jugadores");

                string? optionNumberOfPlayers = Console.ReadLine();
                switch (optionNumberOfPlayers)
                {
                    case "1":
                    {
                        numberOfPlayers = 2;
                        break;
                    }
                    case "2": 
                    {
                        numberOfPlayers = 4;
                        break;
                    }
                    default:
                    {
                        Console.Clear();
                        Console.WriteLine("Por favor seleccione una opción válida");
                        break;
                    }
                }
            }
        #endregion

        #region Set Players
            Player[] players = new Player[numberOfPlayers];

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Clear();
                string? name = "PlaceHolder";
                do
                {
                    Console.WriteLine($"Jugador {i+1}: ");
                    Console.WriteLine($"Inserte Nombre del Jugador {i+1}: ");
                    name = Console.ReadLine();
                }while(Player.IsValidName(name) == false); 
                
                string? optionPlayerClass = null;
                string rol = "NoSeted";
                while(optionPlayerClass == null)
                {
                    Console.WriteLine($"{name} selecciona una Clase: ");
                    Console.WriteLine("1- Guerrero");
                    Console.WriteLine("2- Mago");
                    Console.WriteLine("3- Explorador");
                    optionPlayerClass = Console.ReadLine();
                    switch (optionPlayerClass)
                    {
                        case "1":
                        {
                            rol = "Warrior";
                            break;
                        }
                        case "2":
                        {
                            rol = "Mage";
                            break;
                        }
                        case "3":
                        {
                            rol = "Explorer";
                            break;
                        }
                        default:
                        {
                            Console.Clear();
                            Console.WriteLine("Por favor seleccione una opción válida");
                            optionPlayerClass = null;
                            break;
                        }
                    }
                }
                players[i] = new Player(name,rol);
            }
        #endregion

        #region Generación del laberinto y preeliminares
        Console.Clear();
        Console.WriteLine("Generando laberinto...");
        GameBoard maze;
        if(numberOfPlayers == 2)
        {
            maze = new GameBoard(15,15);
        }
        else
        {
            maze = new GameBoard(31,31);
        }

        Console.Clear();
        Console.WriteLine("Colocando jugadores...");
        if(numberOfPlayers == 2)
        {
            players[0].position = new Pair(1,1);
            players[1].position = new Pair(15,15);
        }
        else
        {
            players[0].position = new Pair(1,1);
            players[1].position = new Pair(1,31);
            players[2].position = new Pair(31,1);
            players[3].position = new Pair(31,31);
        }
        foreach(Player player in players)
        {
            maze[player.position] = GameBoard.CellType.Player;
        }

        //maze.PrintMaze();
        #endregion

        #region Turnos
        
        do
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {   
                Console.Clear();
                Console.WriteLine($"Turno de {players[i]._name}");
                string? optionPlayerAction = null;
                while(optionPlayerAction == null)
                {
                    Console.WriteLine("Acciones:");
                    Console.WriteLine("1- Moverse");
                    Console.WriteLine("2- Habilidades");
                    Console.WriteLine("3- Estado");
                    optionPlayerAction = Console.ReadLine();
                    switch(optionPlayerAction)
                    {
                        case "1":
                        {
                            do
                            {
                                maze.PrintMaze();
                                Console.WriteLine("Dirección para moverse: ");
                                Console.WriteLine("1- Arriba");
                                Console.WriteLine("2- Derecha");
                                Console.WriteLine("3- Abajo");
                                Console.WriteLine("4- Izquierda");
                                Console.WriteLine("0- Cancelar");
                                optionPlayerAction = Console.ReadLine();
                                switch (optionPlayerAction)
                                {
                                    case "1":
                                    {
                                        if(players[i].ValidMove(maze,Player.moveDirections.Up))
                                        {
                                            players[i].Move(maze,Player.moveDirections.Up);
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("No puedes moverte en esa dirección");
                                            optionPlayerAction = null;
                                        }
                                        break;
                                    }
                                    case "2":
                                    {
                                        if(players[i].ValidMove(maze,Player.moveDirections.Rigth))
                                        {
                                            players[i].Move(maze,Player.moveDirections.Rigth);
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("No puedes moverte en esa dirección");
                                            optionPlayerAction = null;
                                        }
                                        break;
                                    }
                                    case "3":
                                    {
                                        if(players[i].ValidMove(maze,Player.moveDirections.Down))
                                        {
                                            players[i].Move(maze,Player.moveDirections.Down);
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("No puedes moverte en esa dirección");
                                            optionPlayerAction = null;
                                        }
                                        break;
                                    }
                                    case "4":
                                    {
                                        if(players[i].ValidMove(maze,Player.moveDirections.Left))
                                        {
                                            players[i].Move(maze,Player.moveDirections.Left);
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("No puedes moverte en esa dirección");
                                            optionPlayerAction = null;
                                        }
                                        break;
                                    }
                                    case "0":
                                    {
                                        optionPlayerAction = null;
                                        break;
                                    }
                                    default:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Por favor escoja una opción válida");
                                        optionPlayerAction = "NoMoved";
                                        break;
                                    }
                                }
                            }while(optionPlayerAction == "NoMoved");
                            break;
                        }
                        default:
                        {
                            Console.Clear();
                            Console.WriteLine("Por favor escoja una opción válida");
                            optionPlayerAction = null;
                            break;
                        }
                    }
                }
            }
        }while(true);
        #endregion

        #region End Game
            
        #endregion

    }
}