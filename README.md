	#README DE LA VIBORITA 
de que trata el proyecto: El clasico juego de la viborita, el cual se juega con las flechas del teclado, el objetivo es comer la mayor cantidad de frutas posibles sin chocar contra las paredes o contra uno mismo.

como funciona : el programa se ejecuta en la consola, se muestra un menu de inicio donde el usuario puede elegir entre jugar o salir, si elige jugar se inicia el juego y se muestra la viborita y las frutas, el usuario controla la viborita con las flechas del teclado, cada vez que la viborita come una fruta, esta crece y se genera una nueva fruta en una posición aleatoria, el juego termina cuando la viborita choca contra las paredes o contra uno mismo.

que hice para realizar la practica: lo primero fue hacer una nueva rama en github para no sobre poner las cosas, luego cree un nuevo proyecto de consola en visual studio, luego hice el menu de inicio, luego hice la logica del juego, luego hice la logica para generar las frutas, luego hice la logica para controlar la viborita con las flechas del teclado, luego hice la logica para detectar cuando la viborita choca contra las paredes o contra uno mismo, y por ultimo hice algunos ajustes para mejorar el juego.
	
	
	CAPTURAS DE PANTALLA 
MENU
<img width="1920" height="1080" alt="Captura de pantalla (57)" src="https://github.com/user-attachments/assets/cfa19205-29f6-47a6-926e-ebc559992383" />

Tablero inicial 
<img width="1920" height="1080" alt="Captura de pantalla (58)" src="https://github.com/user-attachments/assets/cf106dc6-e98b-40f7-96ce-d91e38e03c47" />

funcion de crecer cuando comes y cambio de posicion de la manzana 
<img width="1920" height="1080" alt="Captura de pantalla (59)" src="https://github.com/user-attachments/assets/0d1416dd-77cf-474d-bc32-71da0f3798e7" />

interfaz cuando pierdes 
<img width="1920" height="1080" alt="Captura de pantalla (60)" src="https://github.com/user-attachments/assets/c6cca8fa-d1c7-433f-8195-e93d9f42d627" />

clausula de ia 
para este proyecto use gemini para depurar mi codigo ya que al agregar categorias rompi TODO asi que use gemini para encontrar el error y corregirlo.(deje los comentarios de gemini en el codigo para que se vea el proceso de depuracion)

resultado ¡Entendido! Vamos a consolidar todo. He unificado tu menú principal, la lógica del Ahorcado (con categorías, pistas y corazones) y la estructura para la Viborita en solo dos bloques de código.

Para que funcione, borra lo que tengas en esos archivos y pega lo siguiente:

1. Reemplaza todo en Program.cs
Este archivo controla el flujo principal y el menú decorado con el logo de tu proyecto.

C#
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Game Center - Nodo";
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        while (true)
        {
            Console.Clear();
            DibujarMenu();
            
            int centroX = Console.WindowWidth / 2;
            Console.SetCursorPosition(centroX - 10, 15);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" >> Opción: ");
            Console.ResetColor();
            
            var opcion = Console.ReadLine();

            if (opcion == "1")
            {
                // Inicia el Ahorcado desde la Clase Juego
                Ahorcado.Juego partidaAhorcado = new Ahorcado.Juego();
                partidaAhorcado.Jugar();
            }
            else if (opcion == "2")
            {
                // Inicia la Viborita desde la Clase ViboritaJuego
                Ahorcado.ViboritaJuego partidaViborita = new Ahorcado.ViboritaJuego();
                partidaViborita.Jugar();
            }
            else if (opcion.ToLower() == "exit")
            {
                break;
            }
        }
    }

    static void DibujarMenu()
    {
        string[] logo = {
            " ██████╗  █████╗ ███╗   ███╗███████╗███████╗",
            "██╔════╝ ██╔══██╗████╗ ████║██╔════╝██╔════╝",
            "██║  ███╗███████║██╔████╔██║█████╗  ███████╗",
            "██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ╚════██║",
            "╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗███████║",
            " ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝╚══════╝"
        };

        int centroX = Console.WindowWidth / 2;
        int centroY = 4;

        Console.ForegroundColor = ConsoleColor.Magenta;
        for (int i = 0; i < logo.Length; i++)
        {
            Console.SetCursorPosition(centroX - (logo[i].Length / 2), centroY + i);
            Console.WriteLine(logo[i]);
        }

        string[] opciones = { "1. El Ahorcado Clásico", "2. Viborita (Snake)", "Escribe 'exit' para salir" };
        for (int i = 0; i < opciones.Length; i++)
        {
            Thread.Sleep(150); 
            Console.SetCursorPosition(centroX - (opciones[i].Length / 2), centroY + 8 + i);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(opciones[i]);
        }
        Console.ResetColor();
    }
}
2. Reemplaza todo en Class1.cs
Aquí he puesto ambas clases de juego dentro del mismo namespace para que no tengas problemas de referencia.

C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ahorcado
{
    // ==========================================
    // CLASE: JUEGO DEL AHORCADO
    // ==========================================
    public class Juego
    {
        private Dictionary<string, Dictionary<string, string>> _categorias = new()
        {
            { "Peliculas", new() {
                { "inception", "Trata sobre los sueños dentro de los sueños." },
                { "gladiador", "Un general que se convirtió en esclavo." },
                { "batman", "El caballero de la noche de Gotham." }
            }},
            { "Juegos", new() {
                { "halo", "El Jefe Maestro lucha contra el Covenant." },
                { "eldenring", "Mundo abierto creado por Miyazaki y George R.R. Martin." },
                { "minecraft", "Un mundo infinito de cubos." }
            }},
            { "Canciones", new() {
                { "bohemianrhapsody", "La obra maestra de Queen." },
                { "thriller", "El éxito más grande de Michael Jackson." },
                { "stairwaytoheaven", "Un clásico eterno de Led Zeppelin." }
            }}
        };

        private string _palabraSecreta;
        private string _pistaActual;
        private List<char> _letrasUsadas;
        private int _intentosRestantes;
        private string _categoriaSeleccionada;

        public Juego()
        {
            _letrasUsadas = new List<char>();
            _intentosRestantes = 6;
        }

        public void Jugar()
        {
            SeleccionarCategoria();

            while (_intentosRestantes > 0)
            {
                MostrarTablero();

                if (VerificarVictoria())
                {
                    MostrarTablero();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n¡GANASTE! La palabra era: {_palabraSecreta}");
                    Console.ResetColor();
                    PreguntarReinicio();
                    return;
                }

                Console.Write("\n >> Ingresa una letra: ");
                string entrada = Console.ReadLine()?.ToLower();

                if (string.IsNullOrEmpty(entrada)) continue;
                char letra = entrada[0];

                if (_letrasUsadas.Contains(letra))
                {
                    Console.WriteLine("\n[!] Ya usaste esa letra.");
                    Thread.Sleep(600);
                    continue;
                }

                _letrasUsadas.Add(letra);

                if (!_palabraSecreta.Contains(letra))
                {
                    _intentosRestantes--;
                }
            }

            MostrarTablero();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nPERDISTE. La palabra era: {_palabraSecreta}");
            Console.ResetColor();
            PreguntarReinicio();
        }

        private void SeleccionarCategoria()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=== SELECCIONA UNA CATEGORÍA ===");
            Console.ResetColor();

            var listaCats = _categorias.Keys.ToList();
            for (int i = 0; i < listaCats.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaCats[i]}");
            }

            Console.Write("\nEscoge el número: ");
            if (int.TryParse(Console.ReadLine(), out int indices) && indices > 0 && indices <= listaCats.Count)
                _categoriaSeleccionada = listaCats[indices - 1];
            else
                _categoriaSeleccionada = "Peliculas";

            var banco = _categorias[_categoriaSeleccionada];
            var llaves = banco.Keys.ToList();
            var random = new Random();
            _palabraSecreta = llaves[random.Next(llaves.Count)];
            _pistaActual = banco[_palabraSecreta];
            
            Console.WriteLine($"\nCargando {_categoriaSeleccionada}...");
            Thread.Sleep(800);
        }

        private bool VerificarVictoria() => _palabraSecreta.All(c => _letrasUsadas.Contains(c));

        private void PreguntarReinicio()
        {
            Console.Write("\n¿Volver al menú? (Enter) o ¿Jugar de nuevo? (s): ");
            if (Console.ReadLine()?.ToLower() == "s")
                new Juego().Jugar();
        }

        private void MostrarTablero()
        {
            Console.Clear(); 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"=== CATEGORÍA: {_categoriaSeleccionada.ToUpper()} ===");
            Console.ResetColor();

            MostrarAhorcado(); 

            if (_intentosRestantes <= 3)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[PISTA]: {_pistaActual}");
                Console.ResetColor();
            }

            Console.WriteLine($"Vidas: {new string('❤', _intentosRestantes)}");
            Console.WriteLine($"Letras: {string.Join(", ", _letrasUsadas)}");
            
            Console.Write("\nPalabra: ");
            foreach (char c in _palabraSecreta)
                Console.Write(_letrasUsadas.Contains(c) ? $"{c} " : "_ ");
            Console.WriteLine();
        }

        private void MostrarAhorcado()
        {
            int fallos = 6 - _intentosRestantes;
            string[] etapas = {
                "  +---+\n  |   |\n      |\n      |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========",
                "  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="
            };
            Console.WriteLine(etapas[fallos]);
        }
    }

    // ==========================================
    // CLASE: JUEGO DE LA VIBORITA
    // ==========================================
    public class ViboritaJuego
    {
        public void Jugar()
        {
            Console.Clear();
            Console.CursorVisible = false;

            // IMPORTANTE: Asegúrate de que los nombres de estas clases
            // coincidan con los de tus archivos .cs de la viborita.
            var motor = new MotorViborita(); 
            var ui = new ConsolaUIViborita(motor);

            while (!motor.Ganado() && !motor.Perdido())
            {
                ui.MostrarTablero();
                var tecla = ui.LeerTecla();

                if (tecla == ConsoleKey.Q) break;

                if (tecla != ConsoleKey.NoName)
                    motor.CambiarDireccion(tecla);

                motor.Avanzar();
                Thread.Sleep(150); 
            }

            ui.MostrarTablero();
            ui.MostrarMensaje(motor.Ganado()
                ? "\n¡FELICIDADES! Ganaste."
                : "\nGAME OVER - Presiona cualquier tecla para volver al menú.");

            Console.CursorVisible = true;
            Console.ReadKey();
        }
    }
}
Notas Finales para que todo "Jale":
Archivos Originales: No borres tus archivos MotorViborita.cs ni ConsolaUIViborita.cs, ya que la nueva clase ViboritaJuego los necesita para funcionar.

Namespace: He usado namespace Ahorcado en la Clase 1. Si tus otros archivos de la viborita usan un namespace diferente (como Ahorcado1), asegúrate de ponerles a todos el mismo o agregar el using correspondiente.

Compilación: Si te marca error de "No se encuentra el tipo o nombre de la clase", dale a Compilar > Recompilar solución.

¡Listo! Con esto ya deberías tener ambos juegos corriendo desde tu menú personalizado de Nodo.