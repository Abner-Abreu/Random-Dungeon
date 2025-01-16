namespace Player_Utils;
using Game_Board;
using Utils;
public partial class Player
{
    public void MoveMenu(GameBoard board)
    {
        bool wantToMove = true;
        while (numberOfMoves > 0 && wantToMove)
        {
            Console.Clear();
            Console.WriteLine("Dirección para moverse: ");
            Console.WriteLine("1- Arriba");
            Console.WriteLine("2- Derecha");
            Console.WriteLine("3- Abajo");
            Console.WriteLine("4- Izquierda");
            Console.WriteLine("0- Cancelar");
            ConsoleKeyInfo optionPlayerAction = Console.ReadKey(true);
            switch (optionPlayerAction.KeyChar)
            {
                case '1':
                    {
                        if (ValidMove(board, moveDirection.Up))
                        {
                            Move(board, moveDirection.Up);
                            numberOfMoves--;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No puedes moverte en esa dirección");
                            Console.ReadKey(true);
                            Console.ResetColor();
                        }
                        break;
                    }
                case '2':
                    {
                        if (ValidMove(board, moveDirection.Rigth))
                        {
                            Move(board, moveDirection.Rigth);
                            numberOfMoves--;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No puedes moverte en esa dirección");
                            Console.ReadKey(true);
                            Console.ResetColor();
                        }
                        break;
                    }
                case '3':
                    {
                        if (ValidMove(board, moveDirection.Down))
                        {
                            Move(board, moveDirection.Down);
                            numberOfMoves--;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No puedes moverte en esa dirección");
                            Console.ReadKey(true);
                            Console.ResetColor();
                        }
                        break;
                    }
                case '4':
                    {
                        if (ValidMove(board, moveDirection.Left))
                        {
                            Move(board, moveDirection.Left);
                            numberOfMoves--;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No puedes moverte en esa dirección");
                            Console.ReadKey(true);
                            Console.ResetColor();
                        }
                        break;
                    }
                case '0':
                    {
                        wantToMove = false;
                        break;
                    }
                default:
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Seleccione una opción válida");
                        Console.ReadKey(true);
                        Console.ResetColor();
                        break;
                    }
            }
        }
    }
}
