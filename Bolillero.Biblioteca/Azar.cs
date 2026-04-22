using System;

namespace Bolillero.Biblioteca
{
    public class Azar : IAzar
    {
        private Random _random = new Random();
        public int SacarIndice(int cantidadBolillas) => _random.Next(0, cantidadBolillas);
    }
}