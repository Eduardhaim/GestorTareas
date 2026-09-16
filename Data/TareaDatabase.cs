using SQLite;
using GestorTareas.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GestorTareas.Data
{
    public class TareaDatabase
    {
        private SQLiteAsyncConnection? _database;

        async Task Init()
        {
            if (_database is not null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Tareas.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            await _database.CreateTableAsync<Tarea>();
        }

        // Crear o Actualizar
        public async Task<int> GuardarTareaAsync(Tarea tarea)
        {
            await Init();
            if (tarea.Id != 0)
                return await _database.UpdateAsync(tarea);
            else
                return await _database.InsertAsync(tarea);
        }

        // Leer tareas: ORDENADO ESTRICTAMENTE por Fecha Límite y luego por Prioridad (Requisito del Proyecto 3)
        public async Task<List<Tarea>> ObtenerTareasAsync()
        {
            await Init();
            return await _database.Table<Tarea>()
                                  .OrderBy(t => t.FechaLimite)
                                  .ThenBy(t => t.Prioridad)
                                  .ToListAsync();
        }

        // Eliminar
        public async Task<int> EliminarTareaAsync(Tarea tarea)
        {
            await Init();
            return await _database.DeleteAsync(tarea);
        }
    }
}