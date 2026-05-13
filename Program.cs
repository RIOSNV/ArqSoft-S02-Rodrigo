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