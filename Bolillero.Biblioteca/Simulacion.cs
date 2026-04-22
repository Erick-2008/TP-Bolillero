using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolillero.Biblioteca
{
    public class Simulacion
    {
        public long simularSinHilos(Bolillero bolillero, List<int> jugada, long cantidad)
        {
            return bolillero.JugarNVeces(jugada, cantidad);
        }

        public long simularConHilos(Bolillero bolillero, List<int> jugada, long cantidad, int hilos)
        {
            long porHilo = cantidad / hilos;
            Task<long>[] tareas = new Task<long>[hilos];

            for (int i = 0; i < hilos; i++)
            {
                // Clonamos por cada hilo para evitar colisiones de datos
                var clon = (Bolillero)bolillero.Clone();
                
                long cantidadAIterar = (i == hilos - 1) 
                    ? porHilo + (cantidad % hilos) 
                    : porHilo;

                // Se pasa el clon al hilo, asegurando independencia total
                tareas[i] = Task.Run(() => clon.JugarNVeces(jugada, cantidadAIterar));
            }

            Task.WaitAll(tareas);
            return tareas.Sum(t => t.Result);
        }
    }
}