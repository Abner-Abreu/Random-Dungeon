using MazeGenerator;
class Program
{
    static void Main()
    {
        #region Players Number
        int numberOfPlayers = 0;
        Console.Clear();
            while (numberOfPlayers != 2 && numberOfPlayers != 4)
            {
                Console.WriteLine("Escoja el número de Jugadores: ");
                Console.WriteLine("1- Dos jugadores");
                Console.WriteLine("2- Cuatro jugadores");

                string? option = Console.ReadLine();
                switch (option)
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

        #region Generación del laberinto y preeliminares
            
        #endregion

        #region Turnos
            
        #endregion

        #region End Game
            
        #endregion

    }
}