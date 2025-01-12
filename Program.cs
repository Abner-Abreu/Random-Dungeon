using Game_Board;
using Player_Utils;
using Pair_Type;
using Utils;
using System.Threading;
class Program
{
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
            Player[] playersGroup = new Player[numberOfPlayers];

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Clear();
                string? name;
                do
                {
                    Console.WriteLine($"Jugador {i+1}: ");
                    Console.WriteLine($"Inserte Nombre del Jugador {i+1}: ");
                    name = Console.ReadLine();
                }while(Player.IsValidName(name) == false); 
                
                string? optionPlayerClass = null;
                playerType type = playerType.None;
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
                            type = playerType.Warrior;
                            break;
                        }
                        case "2":
                        {
                            type = playerType.Mage;
                            break;
                        }
                        case "3":
                        {
                            type = playerType.Explorer;
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
                playersGroup[i] = new Player(name,type);
                Console.WriteLine($"{playersGroup[i]._name} => {playersGroup[i]._playerType}");
            }
        #endregion

        #region Preeliminares
        Console.Clear();
        Console.WriteLine("Generando laberinto...");
        GameBoard maze;
        if(numberOfPlayers == 2)
        {
            maze = new GameBoard(15);    
        }
        else
        {
            maze = new GameBoard(31);
        }
        
        Console.Clear();
        Console.WriteLine("Colocando jugadores...");
        if(numberOfPlayers == 2)
        {
            playersGroup[0].position = new Pair(1,1);
            playersGroup[1].position = new Pair(maze._size,maze._size);
        }
        else
        {
            playersGroup[0].position = new Pair(1,1);
            playersGroup[1].position = new Pair(1,maze._size);
            playersGroup[2].position = new Pair(maze._size,1);
            playersGroup[3].position = new Pair(maze._size,maze._size);
        }
        maze.UpdatePlayersPosition(playersGroup);

        //maze.PrintMaze();
        #endregion

        #region Turnos
        
        do
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {   
                Console.Clear();
                Console.WriteLine($"Turno de {playersGroup[i]._name}");
                string? optionPlayerAction = null;
                while(optionPlayerAction == null)
                {
                    Console.WriteLine("Acciones:");
                    Console.WriteLine("1- Moverse");
                    Console.WriteLine("2- Estado");
                    Console.WriteLine("3- Habilidad");
                    optionPlayerAction = Console.ReadLine();
                    switch(optionPlayerAction)
                    {
                        case "1":
                        {
                            int numberOfMoves = 3;
                            do
                            {
                                maze.PrintMaze();
                                Console.WriteLine("Dirección para moverse: ");
                                Console.WriteLine("1- Arriba");
                                Console.WriteLine("2- Derecha");
                                Console.WriteLine("3- Abajo");
                                Console.WriteLine("4- Izquierda");
                                optionPlayerAction = Console.ReadLine();
                                switch (optionPlayerAction)
                                {
                                    case "1":
                                    {
                                        if(playersGroup[i].ValidMove(maze,moveDirection.Up))
                                        {
                                            playersGroup[i].Move(maze,moveDirection.Up);
                                            numberOfMoves--;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("No puedes moverte en esa dirección");
                                        }
                                        break;
                                    }
                                    case "2":
                                    {
                                        if(playersGroup[i].ValidMove(maze,moveDirection.Rigth))
                                        {
                                            playersGroup[i].Move(maze,moveDirection.Rigth);
                                            numberOfMoves--;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("No puedes moverte en esa dirección");
                                        }
                                        break;
                                    }
                                    case "3":
                                    {
                                        if(playersGroup[i].ValidMove(maze,moveDirection.Down))
                                        {
                                            playersGroup[i].Move(maze,moveDirection.Down);
                                            numberOfMoves--;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("No puedes moverte en esa dirección");
                                        }
                                        break;
                                    }
                                    case "4":
                                    {
                                        if(playersGroup[i].ValidMove(maze,moveDirection.Left))
                                        {
                                            playersGroup[i].Move(maze,moveDirection.Left);
                                            numberOfMoves--;
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("No puedes moverte en esa dirección");
                                        }
                                        break;
                                    }
                                    default:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Por favor escoja una opción válida");
                                        break;
                                    }
                                }
                            }while(numberOfMoves > 0);
                            break;
                        }
                        case "2":
                        {
                            break;
                        }
                        case "3":
                        {
                            Console.Clear();
                            string description = Utils.Utils.HabilityDescription[playersGroup[i]._playerHability];
                            Console.WriteLine($"{description}");
                            switch (playersGroup[i]._playerHability)
                            {
                                case playerHability.WallDestroyer:
                                {
                                    PlayerHabilitys.WallDestroyer(maze,playersGroup[i]);
                                    break;
                                }
                                case playerHability.Instinct:
                                {
                                    PlayerHabilitys.Instinct(maze,playersGroup[i]);
                                    break;
                                }
                                case playerHability.Swap:
                                {
                                    PlayerHabilitys.Swap(playersGroup,playersGroup[i]);
                                    break;
                                }
                            }
                            Thread.Sleep(5000);
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