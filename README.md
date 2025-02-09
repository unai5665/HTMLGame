# Juego de Etiquetas HTML en Unity

Este es un juego interactivo creado en Unity, en el que los jugadores deben capturar globos con etiquetas HTML correctas en el orden adecuado. El objetivo es aprender y practicar el orden de las etiquetas HTML mientras te diviertes. El juego incluye una pantalla de inicio, temporizador, puntuación, y una pantalla de "Game Over" al final del juego.

## Características

- **Captura de Globos**: Los jugadores deben capturar globos que contienen etiquetas HTML como `<a>`, `</a>`, `<div>`, `</div>`, etc.
- **Temporizador**: El juego tiene un temporizador que cuenta el tiempo en cada nivel.
- **Puntuación**: Los jugadores ganan puntos cada vez que completan el orden correcto de etiquetas.
- **Niveles**: Cada nivel tiene un conjunto de etiquetas HTML diferentes que el jugador debe ordenar correctamente.
- **Pantallas de UI**: Pantallas de inicio, temporizador, puntuación y final del juego.

## Interfaz
![StartScreen](https://github.com/unai5665/HTMLGame/blob/main/StartScreen.PNG))

![GameScreen]()

![GameOverScreen]()



## Cómo Jugar

- **Pantalla de Inicio**: El juego comienza con una pantalla de inicio. Haz clic en "Start Game" para comenzar.
- **Captura los Globos**: El jugador debe hacer clic en los globos que contienen las etiquetas HTML correctas en el orden adecuado.
- **Orden Correcto**: Si el jugador captura los globos en el orden correcto, avanzará al siguiente nivel y ganará puntos.
- **Game Over**: Cuando el temporizador llegue a cero, el juego mostrará la pantalla de "Game Over" y la puntuación final.

## Estructura del Proyecto

El proyecto está organizado en las siguientes carpetas y scripts:

- **Scripts**:
  - `GameManager.cs`: Controla la lógica principal del juego, incluyendo la creación de globos, la puntuación y el tiempo.
  - `Balloon.cs`: Controla el comportamiento de los globos, incluyendo el movimiento y la asignación de etiquetas HTML.
  - `TagContainer.cs`: Gestiona las etiquetas arrastrables y su orden.
  - `DraggableTag.cs`: Controla la interacción con las etiquetas, permitiendo arrastrarlas dentro del juego.
  - `Slot.cs`: Gestiona la colocación de las etiquetas arrastradas en los slots adecuados.

- **UI**:
  - **Start Screen**: La pantalla inicial del juego donde el jugador puede comenzar.
  - **Game Over Screen**: La pantalla final del juego que muestra la puntuación final.
  - **Score Display**: Muestra la puntuación en tiempo real durante el juego.

