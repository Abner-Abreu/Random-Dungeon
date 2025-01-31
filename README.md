# Random Dungeon


## Principales Características
+ Mapa ***generado aleatoriamente***, especialmente para cada partida
+ Varios ***tamaños*** de mapas para elegir
+ Múltiples ***tipos de trampas***
+ Personalización de los personajes con ***múltiples clases y habilidades únicas***
+ Jugabilidad ***simple y fácil*** de entender

## ¿Cómo ejecutar el proyecto?
+ Descargar el contenido del repositorio
+ Abrir la ubicación del proyecto desde la terminal o algún editor de texto/IDE
+ Ejecutar el comando `dotnet restore` en la terminal para descargar archivos necesarios
+ Ejecutar el comando `dotnet run` desde la terminal o la acción equivalente en el editor
+ Jugar!  

> [!Important]
> Es necesario tener instalado `DotNET 8.x` para ejecutar el proyecto  
> El proyecto emplea `Spectre.Console` para su implementación, es necesario descargarlo antes de ejecutar el proyecto  
> En algunas consolas más antiguas pueden aparecer problemas de compatibilidad, se recomienda usar instalar una lo más actualizada posible (ej `PoweShell 10.0`)

## ¿Cómo jugar?
### 1. Inicio de la Partida: ###
  + Seleccionar el número de jugadores (2 o 4)
  + Selecciona el tamaño del Mapa:  
    - Pequeño (15 x 15): Para partidas rápidas [recomendado para 2 jugadores]  
    - Mediano (25 x 25): Una experiencia equilibrada, ideal para cualquier partida  
    - Grande  (35 x 35): Para partidas más prolongadas [recomendado para 4 jugadores]  

### 2. Creación de personajes: ###
+ Cada Jugador crea el personaje con que jugará la partida:  
  - Nombre: Entre 3 y 10 caracteres, no se permiten nombres repetidos  
  - Clase: Escoger una de las clases, cada una con su propia habilidad  
### 3. Partida: ### 
+ Al principio de cada turno regenera 3 de energía y 1 de maná  
+ El Jugador Activo se visualiza en verde en el mapa  
+ Cada Jugador puede:
  - Moverse: Se traslada por el mapa siempre y cuando le quede energía  
  - Estado: Permite comprobar el estado del personaje y algunas estadísticas  
  - Usar Habilidad: El personaje activa su habilidad siempre y cuando cuente con maná suficiente  
  - Terminar Turno: Pasa al turno del siguiente jugador  
### 4. Condición de Victoria: ###
+ Gana el primer jugador en terminar su turno en la habitación del centro del mapa
> [!Note]
> El mapa está plagado de trampas invisibles... hasta que caes en ellas ;)  

## Implementación ##  
### Mapa ###
Generado de manera que se logre la mayor aleatoriedad posible, dando una mayor variedad de mapas.  
+ Genera una matriz cuadrada de NxN incializada como paredes.
+ Selecciona una de las cuatro direcciones aleatoriamente a partir de una posición inicial y empieza a "cavar" caminos mediante una implementación del algoritmo `DFS Recursivo`
+ En cada iteración decide aleatoriamente (con un `10%` de probabilidad) si crear un ciclo (bucles dentro del mapa) en una dirección aleatoria
+ Una vez generado el laberinto en bruto coloca trampas en cada casilla caminable aleatoriamente con cierta probabilidad de acuerdo a la cercanía a la habitación central atendiendo a tres niveles de cercanía (probabilidad del `10%`, `15%` y `25%` según la posición)
+ Finalmente coloca una habitación en el centro del laberinto (con una dimensión de `3x3` celdas)
