namespace Ahorcado.UI
{
    public static class ConsolaUI
    {
        public static void Ejecutar()
        {
            var juego = new Ahorcado.Juego();
            juego.Jugar();
        }
    }
}