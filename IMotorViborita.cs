using System;
using System.Collections.Generic;

namespace Ahorcado
{
    public class MotorViborita : IMotorJuego
    {
        // — Tamaño del tablero —
        public int Ancho { get; } = 20;
        public int Alto { get; } = 15;

        // — Estado de la víbora —
        private readonly LinkedList<(int x, int y)> _cuerpo = new();
        private (int x, int y) _direccion = (1, 0); // Empieza hacia la derecha
        private (int x, int y) _comida;
        private bool _perdido = false;

        public int Puntos { get; private set; } = 0;

        // Propiedades de solo lectura para la UI
        public IEnumerable<(int x, int y)> Cuerpo => _cuerpo;
        public (int x, int y) Comida => _comida;

        public MotorViborita()
        {
            // Víbora inicial: 3 segmentos en el centro
            // El primer elemento de la lista será la "cabeza"
            _cuerpo.AddFirst((Ancho / 2, Alto / 2));
            _cuerpo.AddFirst((Ancho / 2 + 1, Alto / 2));
            _cuerpo.AddFirst((Ancho / 2 + 2, Alto / 2));

            GenerarComida();
        }

        public void CambiarDireccion(ConsoleKey tecla)
        {
            _direccion = tecla switch
            {
                ConsoleKey.UpArrow when _direccion.y != 1 => (0, -1),
                ConsoleKey.DownArrow when _direccion.y != -1 => (0, 1),
                ConsoleKey.LeftArrow when _direccion.x != 1 => (-1, 0),
                ConsoleKey.RightArrow when _direccion.x != -1 => (1, 0),
                _ => _direccion
            };
        }

        public void Avanzar()
        {
            if (_perdido) return;

            var cabeza = _cuerpo.First!.Value;
            var nueva = (x: cabeza.x + _direccion.x, y: cabeza.y + _direccion.y);

            // 1. Colisión con paredes
            if (nueva.x < 0 || nueva.x >= Ancho || nueva.y < 0 || nueva.y >= Alto)
            {
                _perdido = true;
                return;
            }

            // 2. Colisión con sí misma
            if (_cuerpo.Contains(nueva))
            {
                _perdido = true;
                return;
            }

            // 3. Mover cabeza
            _cuerpo.AddFirst(nueva);

            // 4. Lógica de comida
            if (nueva == _comida)
            {
                Puntos++;
                GenerarComida();
                // No quitamos la cola, así crece.
            }
            else
            {
                _cuerpo.RemoveLast(); // Movimiento normal
            }
        }

        private void GenerarComida()
        {
            var random = new Random();
            do
            {
                _comida = (random.Next(Ancho), random.Next(Alto));
            }
            while (_cuerpo.Contains(_comida));
        }

        public bool Ganado() => Puntos >= 10;
        public bool Perdido() => _perdido;
    }
}