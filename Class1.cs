using System;
using System.Collections.Generic;
using System.Linq;

namespace Ahorcado
{
    public class Juego
    {
        // Diccionario que contiene Palabra -> Pista
        private Dictionary<string, string> _bancoPalabras = new()
        {
            { "arquitectura", "Estructura y diseño lógico de un sistema de software." },
            { "interfaz", "Punto de interacción y comunicación entre dos capas o sistemas." },
            { "polimorfismo", "Capacidad de un objeto para ofrecer diferentes respuestas a un mismo mensaje." },
            { "encapsulamiento", "Protección y aislamiento de los datos internos de un objeto." },
            { "herencia", "Mecanismo para que una clase obtenga atributos y métodos de otra." }
        };

        private string _palabraSecreta;
        private string _pistaActual;
        private List<char> _letrasUsadas;
        private int _intentosRestantes;

        public Juego()
        {
            var random = new Random();
            // Seleccionamos una entrada aleatoria del diccionario
            var listaLlaves = new List<string>(_bancoPalabras.Keys);
            _palabraSecreta = listaLlaves[random.Next(listaLlaves.Count)];
            _pistaActual = _bancoPalabras[_palabraSecreta];

            _letrasUsadas = new List<char>();
            _intentosRestantes = 6;
        }

        public void Jugar()
        {
            Console.Clear();
            while (_intentosRestantes > 0)
            {
                MostrarTablero();

                if (VerificarVictoria())
                {
                    Console.WriteLine($"\n¡Ganaste! La palabra era: {_palabraSecreta}");
                    PreguntarReinicio();
                    return;
                }

                Console.Write("\nIngresa una letra: ");
                string entrada = Console.ReadLine()?.ToLower();

                if (string.IsNullOrEmpty(entrada)) continue;

                char letra = entrada[0];

                if (_letrasUsadas.Contains(letra))
                {
                    Console.WriteLine("\nYa usaste esa letra. Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                _letrasUsadas.Add(letra);

                if (!_palabraSecreta.Contains(letra))
                {
                    _intentosRestantes--;
                }
            }

            // Game Over
            MostrarTablero();
            Console.WriteLine($"\nPerdiste. La palabra era: {_palabraSecreta}");
            PreguntarReinicio();
        }

        private bool VerificarVictoria()
        {
            foreach (char c in _palabraSecreta)
            {
                if (!_letrasUsadas.Contains(c))
                    return false;
            }
            return true;
        }

        private void PreguntarReinicio()
        {
            Console.Write("\n¿Jugar otra vez? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                new Juego().Jugar();
            }
        }

        private void MostrarTablero()
        {
            Console.Clear();
            Console.WriteLine("=== AHORCADO: MODO CLASE DIOS ===");
            MostrarAhorcado();

            // Lógica de Pista: Se muestra cuando fallas 3 veces (te quedan 3 intentos o menos)
            if (_intentosRestantes <= 3)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[PISTA REVELADA]: {_pistaActual}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("[PISTA]: Se revelará cuando te queden 3 intentos.");
            }

            Console.WriteLine($"Intentos restantes: {_intentosRestantes}");
            Console.WriteLine($"Letras usadas: {string.Join(", ", _letrasUsadas)}");
            Console.Write("Palabra: ");

            foreach (char c in _palabraSecreta)
            {
                Console.Write(_letrasUsadas.Contains(c) ? $"{c} " : "_ ");
            }
            Console.WriteLine();
        }

        private void MostrarAhorcado()
        {
            string[] etapas = new string[]
            {
                "  +---+\n  |   |\n      |\n      |\n      |\n      |\n=========", // 0 errores
                "  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========", // 1 error
                "  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========", // 2 errores
                "  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========", // 3 errores
                "  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========", // 4 errores
                "  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========", // 5 errores
                "  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="  // 6 errores
            };

            Console.WriteLine(etapas[6 - _intentosRestantes]);
        }
    }
}