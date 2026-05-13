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