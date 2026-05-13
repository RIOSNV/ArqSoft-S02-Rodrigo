using System;
using System.Collections.Generic;

namespace Ahorcado
{
    public interface IMotorJuego
    {
        int Ancho { get; }
        int Alto { get; }
        int Puntos { get; }
        IEnumerable<(int x, int y)> Cuerpo { get; }
        (int x, int y) Comida { get; }

        void CambiarDireccion(ConsoleKey tecla);
        void Avanzar();
        bool Ganado();
        bool Perdido();
    }
}