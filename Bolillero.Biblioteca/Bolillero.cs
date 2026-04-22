using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolillero.Biblioteca
{
    public class Bolillero : ICloneable
    {
        public List<int> Adentro { get; set; } = new List<int>();
        public List<int> Afuera { get; set; } = new List<int>();
        public IAzar Azar { get; set; }

        public Bolillero(int cantidadBolillas, IAzar azar)
        {
            for (int i = 0; i < cantidadBolillas; i++) Adentro.Add(i);
            Azar = azar;
        }

        public object Clone()
        {
            var clon = new Bolillero(0, this.Azar);
            clon.Adentro = new List<int>(this.Adentro);
            clon.Afuera = new List<int>(this.Afuera);
            return clon;
        }

        public int SacarBolilla()
        {
            int indice = Azar.SacarIndice(Adentro.Count);
            int bolilla = Adentro[indice];
            Adentro.RemoveAt(indice);
            Afuera.Add(bolilla);
            return bolilla;
        }

        public void ReIngresar()
        {
            Adentro.AddRange(Afuera);
            Afuera.Clear();
            // Ordenar para que el azar "Primero" siempre encuentre las bolillas en orden
            Adentro.Sort(); 
        }

        public bool Jugar(List<int> jugada)
        {
            foreach (int bolillaEsperada in jugada)
            {
                if (SacarBolilla() != bolillaEsperada)
                {
                    ReIngresar();
                    return false;
                }
            }
            ReIngresar();
            return true;
        }

        public long JugarNVeces(List<int> jugada, long cantidadVeces)
        {
            long victorias = 0;
            for (long i = 0; i < cantidadVeces; i++)
            {
                if (Jugar(jugada)) victorias++;
            }
            return victorias;
        }
    }
}