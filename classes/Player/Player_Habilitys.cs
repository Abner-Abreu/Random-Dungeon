﻿namespace Player_Utils;

using System.Net.NetworkInformation;
using Game_Board;
using Pair_Type;
using Utils;

public partial class Player
{
    public static Pair[] posibleDirections = {
        Utils.Directions[moveDirection.Up],
        Utils.Directions[moveDirection.Down],
        Utils.Directions[moveDirection.Rigth],
        Utils.Directions[moveDirection.Left]
    };
    public static void WallDestroyer(GameBoard maze,Player player)
    {
        List<Pair> destroyableWallPosition = new List<Pair>();
        int numberOfDestroyableWalls = 0;
        for (int i = 0; i < 4; i++)
        {
            Pair possibleWall = player.position + posibleDirections[i];
            if (maze[possibleWall] == CellType.Wall)
            {
                destroyableWallPosition.Add(possibleWall);
                numberOfDestroyableWalls++;
            }
        }
        Random dice = new Random();
        Pair wallToDestroy = destroyableWallPosition[dice.Next(numberOfDestroyableWalls)];
        if (IsBorderWall(maze._size, wallToDestroy))
        {
            Console.WriteLine("Intentas derribar una pared exterior de la mazmorra, no logras ni arañarla");
        }
        else
        {
            maze[wallToDestroy] = CellType.Road;
        }
    }
    private static bool IsBorderWall(int mazeSize, Pair position) 
    {
        if (position.first == 0) return true; //Left Border
        if (position.second == 0) return true; //Top Border
        if (position.first > mazeSize) return true; //Right Border
        if (position.second > mazeSize) return true; //Button Border
        return false;
    }

    public static void Instinct (GameBoard maze, Player player)
    {
        if (maze[player.position + Utils.Directions[moveDirection.Up]] == CellType.Trap)
        {
            Console.WriteLine("Hay una trampa al norte");
        }
        if (maze[player.position + Utils.Directions[moveDirection.Down]] == CellType.Trap)
        {
            Console.WriteLine("Hay una trampa al sur");
        }
        if (maze[player.position + Utils.Directions[moveDirection.Rigth]] == CellType.Trap)
        {
            Console.WriteLine("Hay una trampa al este");
        }
        if (maze[player.position + Utils.Directions[moveDirection.Left]] == CellType.Trap)
        {
            Console.WriteLine("Hay una trampa al oeste");
        }
    }

    public static void Swap (Player[] playersGroup, Player player)
    {
        Random dice = new Random();
        int playerToSwap = dice.Next(playersGroup.Length);
        if (playersGroup[playerToSwap].position == player.position)
        {
            Console.WriteLine("El hechizo falla miserablemente... te quedas en tu sitio");
        }
        else
        {
            Pair temp = playersGroup[playerToSwap].position;
            playersGroup[playerToSwap].position = player.position;
            player.position = temp;
            Console.WriteLine("El hechizo funciona y eres transportado a un lugar mejor... tal vez");
        }
    }   

    public static void Ilumination(GameBoard board, Player player)
    {

    }

    public static void GoblinSummon(GameBoard board, Player player)
    {
        foreach(Pair direction in posibleDirections)
        {
            Pair nearCell = player.position + direction;
            board[nearCell] = CellType.Road;
        }
    }
}
