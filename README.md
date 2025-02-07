# FOXBOUND: Trials of the Wild

**FOXBOUND: Trials of the Wild** es un videojuego tipo rogue-like en 2D desarrollado con Unity. El juego combina mecánicas desafiantes, exploración de salas generadas proceduralmente, y elementos de estrategia que pondrán a prueba tus habilidades.

---

## INDICE:
1. [Características Principales](#características-principales)
2. [História](#história)
3. [Género](#género)
4. [Estilo](#estilo-visual-y-ambiente)
5. [Mecánicas](#mecánicas)
6. [Controles](#controles)
7. [Escenas](#escenas)
8. [Diagramas de Clase](#diagramas-de-clase)

---

## Características principales ⭐

- **Movimiento en 8D**: El jugador se puede mover en las 8 direcciones.
- **Salas Procedurales**: Cada partida es única gracias a la generación aleatoria de niveles.
- **New Input System**: Eficiencia a la hora de detectar inputs.
- **Sistema de Combate**: Variedad de armas y enemigos con comportamientos exclusivos.
- **Progresión del Personaje**: Ganas monedas para comprar nuevas armas y power ups.
- **Sistema de inventario**: Facilidad en la gestión del inventario.

---

## História ⭐

Hace mucho tiempo, los portales comenzaron a aparecer en todo el mundo, conectando diferentes dimensiones que parecen idénticas pero esconden secretos únicos. Cada portal está custodiado por una Llave Primordial, y obtenerla requiere superar una legión de enemigos mecánicos conocidos como **Explosivos de la Primavera** y **Disparadores de Energia**. Estas criaturas explotan al acercarse y disparan proyectiles cargados de energía, protegiendo celosamente los fragmentos de la llave.

Faylen, un solitario explorador, encuentra a Arden, un astuto vendedor ambulante que también es un zorro. Arden se gana la vida comerciando con armas, pociones y curiosos artefactos recolectados de otros mundos. Aunque sospechoso en un principio, Faylen descubre que Arden siempre aparece en el momento justo, ofreciendo justo lo que necesita, aunque a un precio elevado. Entre ellos surge una relación de camaradería y competencia.

Mientras Faylen explora los vastos campos verdes y playas bañadas por la brisa, se enfrenta a enemigos que intentan detener su avance. Cada batalla es un paso hacia el descubrimiento de las Llaves Primordiales, que abren los portales hacia dimensiones desconocidas. Sin embargo, con cada portal que cruza, Faylen siente que se acerca más a un propósito mayor.

---

## Género ⭐

- Principal: Roguelike.
- Subgéneros: Acción, RPG (juego de rol), Estrategia, Aventura.

---

## Estilo Visual y Ambiente ⭐

- Gráficos: Pixel Art, 2D.
- Estética: Entorno natural primaveral (césped, mar, flores), pero con elementos mecánicos/tecnológicos (enemigos explosivos).
- Tono: Aventurero y dinámico, con un trasfondo misterioso.

---

## Mecánicas ⭐

### JUGADOR
- Movimiento: 8 Direcciones
- Ataque: Sigue al raton.
- Cuando recibe daño, su tono cambia a un color rojizo.
- Cuando muere, explota provocando un sonido.

### ENEMIGOS

Los dos enemigos tienen un sistema de deteccion del jugador, si este esta dentro de sus rango veran si entre medio de ellos existe algun obstaculo, si no hay nada lo detectan y en caso contrario, no.

#### Melee (Bomba):
- **IDLE:** Patrulla por el mapa hasta llegar a su destino que se le assigna de forma aleatoria dentro de su zona navegable, también hay momentos que para y esta estatico porque esta "descansando".
- **CHASE:** En cuando detecta al jugador, se le assigna como destino la posición del jugador de forma dinámica.
- **ATTACK:** Cuando entre en contacto con el jugador, explota, ampliando su trigger para que las entidades de su alrededor reciba daño.
- **DIE:** Muere cuando explota (provoca sonido de explosion), o su vida este por debajo o igual a 0. No se destruye el objeto sino que se guarda en una Pool.

##### Spawener de enemigos bomba
Los "Bombas" se generan en puntos especificos del mapa, pero de forma aleatoria. El funcionamiento es la de un "Pool compartida", especificamos cuantos "Bombas" queremos que aparezcan en la sala, si dentro del pool hay suficientes enemigos, entonces no se crean de más, pero si se pide más enemigos de las que hay, entonces si se instanciara más "Bombas".

#### Tirador (Torreta):
- **IDLE:** Rota constantemente su torreta a las direcciones que se le assigna aleatoriamente, este enemigo es estatico, permanece en un punto fijo durante toda la partida.
- **CHASE:** En cuando detecte al jugador, rota la torreta hasta tenerlo en su mira.
- **ATTACK:** Una vez que tiene al jugador en su mira, dispara balas que van rectas.
- **DIE:** Muere cuando su vida sea igual o menor de 0.

### OBJETOS
Hay varios objetos que el jugador puede conseguir a medida que va avanzando en el juego. Estos objetos solo se podran adquirir a través de la tienda y para ello el jugador necesitara tener la cantidad de dinero que pide el vendedor.

#### Armas:
- **Sword:** Arma melee que hace daño a los enemigos con los que entre en contacto.
- **Rifle:** Arma de distancia que dispara una bala que va en linea recta a la direccion donde ha disparado, hasta colisionar con obstaculos. Las balas no se destruyen sino que se desactivan y se guardan en una pool, cuando este vació, se crean de nuevo y en caso contrario se utiliza la que esta dentro de la pool.
- **Lanzallamas:** Arma de distancia limitada que lanza particulas de llamas que hace daño a enemigos y colisiona.
- **Lanzagranadas:** Arma de distancia limitada que lanza granadas, realizando movimientos parabolicos predefinidos, para simular el comportamiento del 3D pero en 2D.

#### Consumibles: 
- **Curar:** Aporta puntos de vida al jugador.
- **Velocidad:** Aumenta la velocidad de movimiento dle jugador en un intervalo de tiempo.
- **Daño:** Aumenta el daño de la arma del jugador en un intertvalo de tiempo.

![Objetos](img/items.png)

### INVENTARIO
Cuando el jugador tenga las monedas suficientes podra comprar el objeto y este aparecera en su inventario (Tecla "tab"), maximo podrá tener **6 objetos** encima, pero solo podra equipar **2 armas** y llevar **3 consumibles**. No puede equipar el mismo objeto dos veces, es decir, una vez el jugador haya conseguido el "rifle" no podrá tener dos "rifles" equipados para usar, el sistema le pedira que se equipe con otra arma, sucede lo mismo con los consumibles, al equiparse no se puede repetir. El jugador puede decidir en cualquier momento eliminar un objeto de su inventario, de esta manera, liberar espacio.

### SALAS
**Generación procedural:** Cada nivel será distinto al anterior porque las salas se generan de manera aleatoria.

1. Primero empezamos con una habitación de una sola dirección (en este caso TOP), un poco más alla del borde de la sala habra un punto de generación en donde se generará un pasillo. El pasillo se orientara (rotación) segun la direccion a la que ha sido llamada como entrada, es decir, si en la sala enterior el jugador sale por la parte superior, entonces al entrar en el pasillo habra entrado por TOP, por lo tanto, tendrá que salir por BOT (de la siguente sala).
2. Una vez tengamos el pasillo generado, este tendra otro punto de generación pero de habitación, escogera uno aleatoriamente que tenga la direccion de entrada (BOT en este caso) y lo generará. Esta nueva habitación generada tendra otros puntos de generación de pasillo, pero como en la direccion de BOT ya tenemos un pasillo, a través de un trigger en el medio de cada elemento, evitaremos crear un pasillo encima de otro pasillo, porque antes de generar, mira si esta ocupado o no.
3. Sucede lo mismo con las habitaciones, una vez tengamos los nuevos pasillos, revisa si el lugar donde se generara la habitacion esta ocupada o no, solo se generara en caso de que no este ocupado, y en caso de que si lo este, cerrara el pasillo con un prefab especifico, de esta manera el "camino" no queda abierto a la nada.
4. En la generación de mapa, especificamos las iteraciones de mapas que queremos que se genere (NO ESPECIFICAMOS EL NUMERO DE HABITACIONES), normalmente el numero de habitaciones es cercano a este valor de itración ya que al generarse aleatoriamente las habitaciones, es probable que en una iteración se generen dos.
5. Cuando se acabe el numero de iteracion especificada, se generara las **Habitaciones Sin Salidas**, que son aquellas que solo tienen una direccion (la de entrada).

<img src="img/esquema-salas.png" alt="Salas" width="500" height="300">

### TIENDA
La tienda no tiene UI, es una tienda de interacción. Aparecera aleatoriamente en alguna sala distinta a la que esta la llave y la del portal.

- En cada tienda habra solamente tres objetos que podra escoger el jugador, para adquirirlo, solamente ha de tener la candidad suficiente de monedas y pasar por encima del objeto.
- En caso de no tener las monedas suficiente, este no podra adquirir el objeto, ni podra pasar por encima, habra una colision que lo repele.
- Los objetos de la tienda aparecen de manera aleatoria a través de la "libreria" de objetos.
- Si el jugador tiene suficientes monedas pero tiene el inventario lleno, tampoco podra comprar el objeto. Tendra que vaciar si inventario, pero en este caso si que podrá pasar por encima del objeto, no lo repelerá.

### PORTAL
- Portal: Se genera siempre en la ultima sala y permanecerá bloqueada (rojo y con colision) hasta que el jugador encuentre y recoja la llave.
- LLave: Se genera a partir de la segunda habitación generada, siempre se generara en el medio de la sala, para desbloquear el portal, hay que coger primero la llave.

---
 
## Controles ⭐

- **Movimiento**: Teclas WASD o flechas direccionales
- **Atacar**: Click izquierdo del raton
- **Inventario**: Tecla "Tab"
- **Consumibles (Power Up)**: Tecla "1" "2" y "3"
- **Cambiar de arma**: Tecla "Q", Rueda raton
- **Pausar**: Tecla "Esc"

![Controles](img/controles.png)

---

## Escenas ⭐

![Diagrama escenas](img/diagrama-escenas.png)

---

## Diagramas de clase ⭐

![Diagrama clase](img/diagrama-clase-itemso.png)
![Diagrama clase](img/diagrama-clase-stateso.png)
![Diagrama clase](img/diagrama-clase-entityso.png)
![Diagrama clase](img/diagrama-clase-item.png)
![Diagrama clase](img/diagrama-clase-resto.png)

¡Gracias por jugar *FOXBOUND: Trials of the Wild*!
