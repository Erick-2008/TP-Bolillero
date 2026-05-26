using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bolillero.Biblioteca
{
    public class Simulacion
    {
        // Métodos de la 2da Iteración (Task):
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

        // Método de la 3ra Iteración (Async)
        public async Task<long> SimularConHilosAsync(Bolillero bolillero, List<int> jugada, long cantidad, int hilos)
        {
            long porHilo = cantidad / hilos;
            List<Task<long>> tareas = new List<Task<long>>();

            for (int i = 0; i < hilos; i++)
            {
                var clon = (Bolillero)bolillero.Clone();
                long cantidadAIterar = (i == hilos - 1) ? porHilo + (cantidad % hilos) : porHilo;

                tareas.Add(Task.Run(() => clon.JugarNVeces(jugada, cantidadAIterar)));
            }

            long[] resultados = await Task.WhenAll(tareas);
            return resultados.Sum();
        }

        // Método de la 4ta Iteración (Parallel + Async)
        public async Task<long> SimularParallelAsync(Bolillero bolillero, List<int> jugada, long cantidad, int hilos)
        {
            return await Task.Run(() =>
            {
                long victoriasTotales = 0;
                
                // Se configura el grado de paralelismo con el parámetro hilos
                var opciones = new ParallelOptions { MaxDegreeOfParallelism = hilos };

                // Implementación de Parallel.For
                Parallel.For(0, cantidad, opciones, i =>
                {
                    // Es fundamental seguir clonando el bolillero para evitar problemas de concurrencia
                    var clon = (Bolillero)bolillero.Clone();
                    
                    if (clon.Jugar(jugada))
                    {
                        // Se usa Interlocked para asegurar un incremento seguro entre hilos
                        Interlocked.Increment(ref victoriasTotales);
                    }
                });

                return victoriasTotales;
            });
        }
    }
}