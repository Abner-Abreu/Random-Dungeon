using Spectre.Console;

namespace Tutorials;

public class Tutorial
{
    public static void ControlTutorial()
    {
        Console.WriteLine(@"Controles Básicos:
▲▼ Para navegar por los menús
Enter Para seleccionar una opción

Movimiento de los Personajes:
▲ - Arriba
► - Derecha
◄ - Izquierda
▼ - Abajo
BackSpace - Dejar de moverse
");
        Console.ReadKey(false);
        Console.Clear();
    }

    public static void HowToPlayTutorial()
    {
        Console.WriteLine(@"Inicio de la Partida:
-Seleccionar el número de jugadores (2 o 4)
-Selecciona el tamaño del Mapa:
    -Pequeño (15 x 15): Para partidas rápidas [recomendado para 2 jugadores]
    -Mediano (25 x 25): Una experiencia equilibrada, ideal para cualquier partida
    -Grande  (35 x 35): Para partidas más prolongadas [recomendado para 4 jugadores]

Creación de personajes:
-Cada Jugador crea el personaje con que jugará la partida:
    -Nombre: Entre 3 y 10 caracteres, no se permiten nombres repetidos
    -Clase: Escoger una de las clases, cada una con su propia habilidad
Partida:
-Al principio de cada turno regenera 3 de energía y 1 de maná
-El Jugador Activo se visualiza en verde en el mapa
-Cada Jugador puede:
    -Moverse: Se traslada por el mapa siempre y cuando le quede energía
    -Estado: Permite comprobar el estado del personaje y algunas estadísticas
    -Usar Habilidad: El personaje activa su habilidad siempre y cuando cuente con maná suficiente
    -Terminar Turno: Pasa al turno del siguiente jugador
Condición de Victoria:
-Gana el primer jugador en terminar su turno en la habitación del centro del mapa
Nota: El mapa está plagado de trampas que son invisibles... hasta que caes en ellas ;)
");
        Console.ReadKey(false);
        Console.Clear();
    }

    public static void ShowTutorial()
    {
        var tutorialMenu = new SelectionPrompt<string>()
            .Title("Opciones:")
            .AddChoices("Controles", "Cómo Jugar", "Atrás");
        bool back = false;
        while (back == false)
        {
            Action tutorialToShow = AnsiConsole.Prompt(tutorialMenu) switch
            {
                "Controles" => ControlTutorial,
                "Cómo Jugar" => HowToPlayTutorial,
                "Atrás" => () => { back = true; },
                _ => ()=>{}
            };
            tutorialToShow.Invoke();
        }
    }

    public static void ShowClasses()
    {
        var classes = new Table();
        classes.AddColumns("Clase", "Habilidad", "Descripción");
        classes.AddRow("Guerrero", "Destructor de Muros", "Te inunda la ira y golpeas una pared, destruyendola");
        classes.AddEmptyRow();
        classes.AddRow("Mago", "Intercambio", "Utilizas tu magia espacial para cambiar tu lugar con alguien elegido por el Vacío");
        classes.AddEmptyRow();
        classes.AddRow("Explorador", "Instinto", "Usas tus sentidos superiores para encontrar mecanismos ocultos");
        classes.AddEmptyRow();
        classes.AddRow("Invocador", "Invocar Goblin", "Invocas a un goblin para que desactive las trampas a tu alrededor");
        classes.AddEmptyRow();
        classes.AddRow("Viajero", "Brisa Refrescante", "Una brisa recorre tu cuerpo, dándote energía para continuar tu camino (+2 de Energía)");

        AnsiConsole.Write(classes);
    }
}
