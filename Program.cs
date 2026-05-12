var repositorio = new Ahorcado.PalabrasEnMemoria();
var motor = new Ahorcado.MotorAhorcado(repositorio);
var ui = new Ahorcado.ConsolaUI(motor);

Console.WriteLine("=== AHORCADO ===");

while (!motor.Ganado() && !motor.Perdido())
{
    ui.MostrarTablero();
    char letra = ui.PedirLetra();

    if (motor.LetraYaUsada(letra))
    {
        ui.MostrarMensaje("Ya usaste esa letra.");
        continue;
    }

    motor.RegistrarLetra(letra);
}

ui.MostrarTablero();

if (motor.Ganado())
{
    ui.MostrarMensaje($"\n¡Ganaste! La palabra era: {motor.PalabraSecreta}");
}
else
{
    ui.MostrarMensaje($"\nPerdiste. La palabra era: {motor.PalabraSecreta}");
}

if (ui.PreguntarOtraVez())
{
    var nuevoMotor = new Ahorcado.MotorAhorcado(repositorio);
    var nuevaUI = new Ahorcado.ConsolaUI(nuevoMotor);
    // Nota: Para que el reinicio funcione, este bloque suele ir dentro de un bucle do-while.
}