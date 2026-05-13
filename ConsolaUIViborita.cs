using Ahorcado;
using System;
using System.Linq;

namespace Ahorcado
{
    public class ConsolaUI
    {
        private readonly MotorAhorcado _motor;

        public ConsolaUI(MotorAhorcado motor)
        {
            _motor = motor;
        }

        public void MostrarTablero()
        {
            Console.WriteLine();
            Console.WriteLine($"Intentos restantes: {_motor.IntentosRestantes}");
            Console.Write("Palabra: ");
            foreach (var c in _motor.PalabraSecreta)
            {
                var lower = char.ToLower(c);
                Console.Write(_motor.LetrasUsadas.Contains(lower) ? c : '_');
                Console.Write(' ');
            }
            Console.WriteLine();
            Console.Write("Letras usadas: ");
            Console.WriteLine(string.Join(' ', _motor.LetrasUsadas));
        }

        public char PedirLetra()
        {
            Console.Write("Introduce una letra: ");
            var line = Console.ReadLine();
            return !string.IsNullOrEmpty(line) ? line[0] : '\0';
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        public bool PreguntarOtraVez()
        {
            Console.Write("¿Jugar otra vez? (s/n): ");
            var r = Console.ReadLine();
            return !string.IsNullOrEmpty(r) && (r[0] == 's' || r[0] == 'S');
        }
    }

    public class ConsolaUIViborita
    {
        private readonly MotorViborita _motor;

        public ConsolaUIViborita(MotorViborita motor)
        {
            _motor = motor;
        }

        public void MostrarTablero()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"=== VIBORITA === Puntos: {_motor.Puntos}");
            Console.WriteLine("+" + new string('-', _motor.Ancho) + "+");

            for (int y = 0; y < _motor.Alto; y++)
            {
                Console.Write("|");
                for (int x = 0; x < _motor.Ancho; x++)
                {
                    var pos = (x, y);

                    // Lógica de dibujo corregida
                    if (_motor.Cuerpo.First() == pos)
                    {
                        Console.Write("@"); // cabeza
                    }
                    else if (_motor.Cuerpo.Contains(pos))
                    {
                        Console.Write("o"); // cuerpo
                    }
                    else if (_motor.Comida == pos)
                    {
                        Console.Write("*"); // comida
                    }
                    else
                    {
                        Console.Write(" "); // vacío
                    }
                }
                Console.WriteLine("|");
            }

            Console.WriteLine("+" + new string('-', _motor.Ancho) + "+");
            Console.WriteLine("Flechas: mover | Q: salir");
        }

        public ConsoleKey LeerTecla()
        {
            if (Console.KeyAvailable)
                return Console.ReadKey(intercept: true).Key;

            return ConsoleKey.NoName;
        }

        public void MostrarMensaje(string mensaje) =>
        Console.WriteLine(mensaje);
    }
}