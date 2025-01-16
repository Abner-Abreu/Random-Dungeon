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

                ConsoleKeyInfo optionNumberOfPlayers = Console.ReadKey(true);
                switch (optionNumberOfPlayers.KeyChar)
                {
                    case '1':
                    {
                        numberOfPlayers = 2;
                        break;
                    }
                    case '2': 
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
                
                playerType type = playerType.None;
                while(type == playerType.None)
                {
                    Console.WriteLine($"{name} selecciona una Clase: ");
                    Console.WriteLine("1- Guerrero");
                    Console.WriteLine("2- Mago");
                    Console.WriteLine("3- Explorador");
                    Console.WriteLine("4- Invocador");
                    ConsoleKeyInfo	optionPlayerClass = Console.ReadKey(true);
                    switch (optionPlayerClass.KeyChar)
                    {
                        case '1':
                        {
                            type = playerType.Warrior;
                            break;
                        }
                        case '2':
                        {
                            type = playerType.Mage;
                            break;
                        }
                        case '3':
                        {
                            type = playerType.Explorer;
                            break;
                        }
                        case '4':
                        {
                            type = playerType.Summoner;
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
        int numberOfTurns = 0;
        bool isVictoryAchieved = false;
        do
        {   
            numberOfTurns++;
            for (int i = 0; i < numberOfPlayers; i++)
            {   
                Console.Clear();
                Console.WriteLine($"Turno de {playersGroup[i]._name}");

                playersGroup[i].numberOfMoves = 3;
                while(playersGroup[i].numberOfMoves > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Acciones:");
                    Console.WriteLine("1- Moverse");
                    Console.WriteLine("2- Estado");
                    Console.WriteLine("3- Habilidad");
                    ConsoleKeyInfo	optionSelected = Console.ReadKey(true);
                    switch(optionSelected.KeyChar)
                    {
                        case '1':
                        {
                            playersGroup[i].MoveMenu(maze);
                            break;
                        }
                        case '2':
                        {
                            break;
                        }
                        case '3':
                        {
                            Console.Clear();
                            string description = Utils.Utils.HabilityDescription[playersGroup[i]._playerHability];
                            Console.WriteLine($"{description}");
                            switch (playersGroup[i]._playerHability)
                            {
                                case playerHability.WallDestroyer:
                                {
                                    Player.WallDestroyer(maze,playersGroup[i]);
                                    break;
                                }
                                case playerHability.Instinct:
                                {
                                    Player.Instinct(maze,playersGroup[i]);
                                    break;
                                }
                                case playerHability.Swap:
                                {
                                    Player.Swap(playersGroup,playersGroup[i]);
                                    break;
                                }
                                case playerHability.GoblinSummon:
                                {
                                    Player.GoblinSummon(maze,playersGroup[i]);
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
                            break;
                        }
                    }
                }
                if(isVictoryAchieved)
                {
                    Console.Clear();
                    Console.WriteLine($"{playersGroup[i]._name} ha llegado al centro del laberinto");
                    Console.WriteLine($"Logró la victoria tras {numberOfTurns} turnos");
                    Console.WriteLine("FELICIDADES!!!");
                }
            }
        }while(isVictoryAchieved == false);
        #endregion

        #region End Game
            
        #endregion

    }
}