namespace Ahorcado
{
    public class MotorAhorcado
    {
        private readonly string _palabraSecreta;
        private readonly List<char> _letrasUsadas = new();
        private int _intentosRestantes = 6;
        public string PalabraSecreta => _palabraSecreta;
        public List<char> LetrasUsadas => _letrasUsadas;
        public int IntentosRestantes => _intentosRestantes;
        public MotorAhorcado(IRepositorioPalabras repositorio)
        {
            _palabraSecreta = repositorio.ObtenerPalabraAleatoria();
        }
        public bool LetraYaUsada(char letra) => _letrasUsadas.Contains(letra);
        public bool EsLetraCorrecta(char letra) => _palabraSecreta.Contains(letra);
        public void RegistrarLetra(char letra)
        {
            _letrasUsadas.Add(letra);
            if (!_palabraSecreta.Contains(letra))
                _intentosRestantes--;
        }
        public bool Ganado()
        {
            foreach (char c in _palabraSecreta)
                if (!_letrasUsadas.Contains(c)) return false;
            return true;
        }
        public bool Perdido() => _intentosRestantes <= 0;
    }
}