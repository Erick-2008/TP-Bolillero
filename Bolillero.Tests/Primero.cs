using Bolillero.Biblioteca;

namespace Bolillero.Tests
{
    // Esta clase en la carpeta de tests asegura que las bolillas salgan en orden (0, 1, 2...) para las pruebas unitarias.
    public class Primero : IAzar
    {
        public int SacarIndice(int cantidadBolillas) => 0;
    }
}