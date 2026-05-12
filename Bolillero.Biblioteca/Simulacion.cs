using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolillero.Biblioteca
{
    public class Simulacion
    {
        // Métodos de la 2da Iteración:
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
                var clon = (Bolillero)bolillero.Clone();
                long cantidadAIterar = (i == hilos - 1) ? porHilo + (cantidad % hilos) : porHilo;
                tareas[i] = Task.Run(() => clon.JugarNVeces(jugada, cantidadAIterar));
            }

            Task.WaitAll(tareas);
            return tareas.Sum(t => t.Result);
        }

        // Método de la 3ra Iteración (Asincrónico)
        public async Task<long> SimularConHilosAsync(Bolillero bolillero, List<int> jugada, long cantidad, int hilos)
        {
            long porHilo = cantidad / hilos;
            List<Task<long>> tareas = new List<Task<long>>();

            for (int i = 0; i < hilos; i++)
            {
                // Mantenemos el requisito de clonación por cada hilo para evitar colisiones
                var clon = (Bolillero)bolillero.Clone();
                long cantidadAIterar = (i == hilos - 1) ? porHilo + (cantidad % hilos) : porHilo;

                // Creamos la tarea asincrónica
                tareas.Add(Task.Run(() => clon.JugarNVeces(jugada, cantidadAIterar)));
            }

            long[] resultados = await Task.WhenAll(tareas);
            return resultados.Sum();
        }
    }
}