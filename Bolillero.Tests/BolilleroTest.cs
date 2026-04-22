using Xunit;
using Bolillero.Biblioteca;
using System.Collections.Generic;

namespace Bolillero.Tests
{
    public class BolilleroTest
    {
        private Bolillero.Biblioteca.Bolillero _bolillero;

        public BolilleroTest()
        {
            // Se inicializa con 10 bolillas (0-9) y el azar "Primero"
            _bolillero = new Bolillero.Biblioteca.Bolillero(10, new Primero());
        }

        [Fact]
        public void SacarBolillaTest()
        {
            int bolilla = _bolillero.SacarBolilla();
            Assert.Equal(0, bolilla);                 // Verifica que devuelve la bolilla 0
            Assert.Equal(9, _bolillero.Adentro.Count); // 9 bolillas dentro
            Assert.Single(_bolillero.Afuera);          // Una bolilla afuera
        }

        [Fact]
        public void ReIngresarTest()
        {
            _bolillero.SacarBolilla();
            _bolillero.ReIngresar();
            Assert.Equal(10, _bolillero.Adentro.Count); // 10 bolillas dentro
            Assert.Empty(_bolillero.Afuera);            // Ninguna bolilla fuera
        }

        [Fact]
        public void JugarGanaTest()
        {
            // Verifica que se gana con {0, 1, 2, 3} usando la clase Primero
            Assert.True(_bolillero.Jugar(new List<int> { 0, 1, 2, 3 }));
        }

        [Fact]
        public void JugarPierdeTest()
        {
            // Verifica que se pierde con {4, 2, 1}
            Assert.False(_bolillero.Jugar(new List<int> { 4, 2, 1 }));
        }

        [Fact]
        public void GanarNVecesTest()
        {
            // Con la jugada {0, 1}, se juega 1 vez y se verifica 1 victoria
            long victorias = _bolillero.JugarNVeces(new List<int> { 0, 1 }, 1);
            Assert.Equal(1L, victorias);
        }
    }
}