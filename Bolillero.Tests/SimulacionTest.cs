using Xunit;
using Bolillero.Biblioteca;
using System.Collections.Generic;

namespace Bolillero.Tests
{
    public class SimulacionTest
    {
        [Fact]
        public void ClonarBolilleroTest()
        {
            var bolilleroOriginal = new Bolillero.Biblioteca.Bolillero(10, new Azar());
            var clon = (Bolillero.Biblioteca.Bolillero)bolilleroOriginal.Clone();
            
            Assert.NotSame(bolilleroOriginal, clon);
            Assert.NotSame(bolilleroOriginal.Adentro, clon.Adentro);
            Assert.Equal(bolilleroOriginal.Adentro.Count, clon.Adentro.Count);
        }

        [Fact]
        public void SimularConHilosGanaSiempreTest()
        {
            // 10 bolillas y azar determinista "Primero"
            var bolillero = new Bolillero.Biblioteca.Bolillero(10, new Primero());
            var simulacion = new Simulacion();
            var jugada = new List<int> { 0, 1 };
            
            long cantidadTotal = 1000;
            int hilos = 4;

            long victorias = simulacion.simularConHilos(bolillero, jugada, cantidadTotal, hilos);
            
            Assert.Equal(cantidadTotal, victorias); 
        }

        [Fact]
        public void SimularSinHilosTest()
        {
            var bolillero = new Bolillero.Biblioteca.Bolillero(10, new Primero());
            var simulacion = new Simulacion();
            var jugada = new List<int> { 0, 1 };
            long cantidadTotal = 100;

            // Verifica el método secuencial
            long victorias = simulacion.simularSinHilos(bolillero, jugada, cantidadTotal);
            
            Assert.Equal(cantidadTotal, victorias);
        }

        [Fact]
        public async Task SimularConHilosAsyncGanaSiempreTest()
        {
            var bolillero = new Bolillero.Biblioteca.Bolillero(10, new Primero());
            var simulacion = new Simulacion();
            var jugada = new List<int> { 0, 1 };
    
            // El test debe usar 'await' para llamar al nuevo método
            long victorias = await simulacion.SimularConHilosAsync(bolillero, jugada, 1000, 4);
    
            Assert.Equal(1000L, victorias);
        }
    }
}