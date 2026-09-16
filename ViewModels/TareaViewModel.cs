using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestorTareas.Models;
using GestorTareas.Data;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;

namespace GestorTareas.ViewModels
{
    public partial class TareaViewModel : ObservableObject 
    {
        private readonly TareaDatabase _database;

        [ObservableProperty]
        ObservableCollection<Tarea> listaTareas;

        [ObservableProperty]
        string nombre = string.Empty;

        [ObservableProperty]
        string descripcion = string.Empty;

        [ObservableProperty]
        DateTime fechaLimite = DateTime.Today.AddDays(1);

        // Variable para capturar la hora en el formulario
        [ObservableProperty]
        TimeSpan horaLimite = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        int? prioridad; // Sin selección inicial; 1: Alta, 2: Media, 3: Baja

        public TareaViewModel()
        {
            _database = new TareaDatabase();
            ListaTareas = new ObservableCollection<Tarea>();
            _ = CargarTareasAsync(); 
        }

        [RelayCommand]
        public async Task CargarTareasAsync()
        {
            var tareas = await _database.ObtenerTareasAsync();
            ListaTareas.Clear();
            foreach (var tarea in tareas)
            {
                ListaTareas.Add(tarea);
            }
        }

        [RelayCommand]
        public async Task GuardarTareaAsync()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || Prioridad is not int prioridadSeleccionada || prioridadSeleccionada < 1 || prioridadSeleccionada > 3) return;

            // Unimos la fecha del DatePicker y la hora del TimePicker
            DateTime fechaHoraCompleta = FechaLimite.Date + HoraLimite;

            var nuevaTarea = new Tarea
            {
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                FechaLimite = fechaHoraCompleta, // Guardamos fecha y hora juntas
                Prioridad = prioridadSeleccionada
            };

            await _database.GuardarTareaAsync(nuevaTarea);
            
            // Limpiar campos del formulario
            Nombre = string.Empty;
            Descripcion = string.Empty;
            FechaLimite = DateTime.Today.AddDays(1);
            HoraLimite = DateTime.Now.TimeOfDay;
            Prioridad = null;

            await CargarTareasAsync(); 
        }

        [RelayCommand]
        public async Task CompletarTareaAsync(Tarea tareaSeleccionada)
        {
            if (tareaSeleccionada != null && tareaSeleccionada.Estado == "Pendiente")
            {
                tareaSeleccionada.Estado = "Completada";
                tareaSeleccionada.FechaUltimoCambio = DateTime.Now; 

                await _database.GuardarTareaAsync(tareaSeleccionada);
                await CargarTareasAsync();
            }
        }

        [RelayCommand]
        public async Task EliminarTareaAsync(Tarea tareaSeleccionada)
        {
            if (tareaSeleccionada != null)
            {
                await _database.EliminarTareaAsync(tareaSeleccionada);
                await CargarTareasAsync();
            }
        }
    }
}