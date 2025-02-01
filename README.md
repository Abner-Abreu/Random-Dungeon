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
+ Gana el primer jugador en llegar a la habitación del centro del mapa
> [!Note]
> El mapa está plagado de trampas invisibles... hasta que caes en ellas ;)  

## Implementación ##  
### Mapa ###
Generado de manera que se logre la mayor aleatoriedad posible, dando una mayor variedad de mapas  
+ Genera una matriz cuadrada de NxN incializada como paredes
+ Selecciona una de las cuatro direcciones aleatoriamente a partir de una posición inicial y empieza a "cavar" caminos mediante una implementación del algoritmo `DFS Recursivo`
+ En cada iteración decide aleatoriamente (con un `10%` de probabilidad) si crear un ciclo (bucles dentro del mapa) en una dirección aleatoria
+ Una vez generado el laberinto en bruto coloca trampas en cada casilla caminable aleatoriamente con cierta probabilidad de acuerdo a la cercanía a la habitación central atendiendo a tres niveles de cercanía (probabilidad del `10%`, `15%` y `25%` según la posición)
+ Se coloca una habitación en el centro del laberinto (con una dimensión de `3x3` celdas)
+ Después de generado el mapa se colocan los jugadores (en función de su número) en las esquinas
### Trampas ###
A lo largo de todo el tablero están colocadas trampas que son activadas cuando el jugador se coloca en la misma casilla  
+ *Trampa de Drenado de Energía*: al activar esta trampa el jugador pierde energía (-3 puntos) disminuyendo significativamente su capacidad de movimiento (si la energía original es menor que la perdida queda en negativo afectando el siguiente turno)  
+ *Trampa Anti-Maná*: al activar esta trampa el jugador pierde maná (-3 puntos) impidiendo el uso de habilidades o aumentando el tiempo de enfriamiento (si el maná origanal es menor que el perdido queda en negativo aumentando el tiempo de enfriamiento)  
+ *Trampa de Cambio de Pasado*: al activar esta trampa el jugador cambia otra a clase (y a su respectiva habilidad) diferente a la suya elegida al azar
+ *Trampa de Re-Aparición de Trampas*: al activar esta trampa las trampas de todo el mapa se vuelven a generar, cambiando su ubicación y agregando otras nuevas. Primero elimina todas las trampas existentes, entonces vuelve a generar las trampas y actualiza la posición de los jugadores en el mapa
+ *Trampa de Teletransporte*: al activar esta trampa la posición del jugador cambia (en función de la posición original, de manera que se mantenga a una distancia semejante del centro). La nueva posición será siempre una casilla transitable y nunca estará en la habitación central
### Movimiento ###
Cada personaje puede moverse en cuatro direcciones (`arriba`, `abajo`, `izquierda` y `derecha`) por las casillas transitables mientras tenga energía suficiente  
+ Al seleccionar la opción `Moverse` el jugador usa las flechas del teclado para decidir la dirección de movimiento
+ Determina si el movimiento es válido (dentro de los límites del mapa y que sea hacia una casilla caminable)
+ Verifica si en el destino hay una trampa y, de haberla, la activa
+ Actualiza la posición del personaje y resta un punto de energía
### Habilidades ###
Cada jugador puede seleccionar una clase para su personaje al inicio de la partida (esta puede cambiar a lo largo de la partida) a la que se asocia la habilidad correspondiente que pueden activar siempre que tengan el maná requerido (3 puntos)
  + *Guerrero*: su habilidad `Destructor de Muros` selecciona una pared adyacente al azar y la destruye (convirtiendola en un camino), si se escoge una pared del borde del mapa la habilidad falla  
  + *Mago*: su habilidad `Intercambio` selecciona al azar un personaje de entre todos los jugadores e intercambia sus posiciones, si la posición del personaje escogido es igual a la suya la habilidad falla   
  + *Explorador*: su habilidad `Instinto` chequea las casillas adyacentes y vuelve visibles las trampas, marcandolas en color rojo en el mapa
  + *Invocador*: su habilidad `Invocar Goblin` chequea las casillas adyacentes al jugador y elimina las trampas sin revelar su ubicación o si habían trampas  
  + *Viajero*: su habilidad `Brisa Refrescante` aumenta en dos puntos la energía del personaje, permitiendole realizar dos movimientos extra   
### Condición de Victoria ###
Un jugador cumple con la condición de victoria al alcanzar la habitación central
  + En cada movimiento realizado por el jugador se comprueba la `Condición de Victoria` comparando la posición del jugador con el interior de la habitación central  
  + Si cumple la condición la partida termina y se muestran las estadísticas finales
