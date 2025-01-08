﻿using MazeGenerator;
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
            Console.Clear();

            Player[] players = new Player[numberOfPlayers];

            for (int i = 0; i < numberOfPlayers; i++)
            {
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

        maze.PrintMaze();
        #endregion

        #region Turnos
            
        #endregion

        #region End Game
            
        #endregion

    }
}