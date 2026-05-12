using System;
using System.Collections.Generic;

namespace Ahorcado
{
    public class Juego
    {
        private List<string> _palabras = new()
        {
            "arquitectura",
            "interfaz",
            "polimorfismo",
            "encapsulamiento",
            "herencia"
        };

        private string _palabraSecreta;
        private List<char> _letrasUsadas;
        private int _intentosRestantes;

        public Juego()
        {
            var random = new Random();
            _palabraSecreta = _palabras[random.Next(_palabras.Count)];
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

            // Si llega aquí, es porque se acabaron los intentos
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
            Console.WriteLine("=== AHORCADO ===");
            MostrarAhorcado();
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

            // Mostramos la etapa basada en cuántos errores lleva el usuario
            Console.WriteLine(etapas[6 - _intentosRestantes]);
        }
    }
}