using System.Collections.Generic;
using System.Linq;

namespace Bolillero.Biblioteca
{
    public class Bolillero
    {
        public List<int> Adentro { get; set; } = new List<int>();
        public List<int> Afuera { get; set; } = new List<int>();
        public IAzar Azar { get; set; }

        public Bolillero(int cantidadBolillas, IAzar azar)
        {
            for (int i = 0; i < cantidadBolillas; i++) Adentro.Add(i);
            Azar = azar;
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
        }

        public bool Jugar(List<int> jugada)
        {
            foreach (int bolillaEsperada in jugada)
            {
                if (SacarBolilla() != bolillaEsperada)
                {
                    ReIngresar(); // Se restauran para la próxima jugada
                    return false;
                }
            }
            ReIngresar();
            return true;
        }

        public int JugarNVeces(List<int> jugada, int cantidadVeces)
        {
            int victorias = 0;
            for (int i = 0; i < cantidadVeces; i++)
            {
                if (Jugar(jugada)) victorias++;
            }
            return victorias;
        }
    }
}