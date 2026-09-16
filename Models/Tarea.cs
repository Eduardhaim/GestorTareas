using SQLite;
using System;

namespace GestorTareas.Models
{
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        
        // Almacena fecha y hora exacta
        public DateTime FechaLimite { get; set; }
        
        public int Prioridad { get; set; } 
        public string Estado { get; set; } = "Pendiente"; 
        public DateTime? FechaUltimoCambio { get; set; } 

        // Propiedad calculada para saber si está vencida (compara fecha y hora actual)
        [Ignore]
        public bool EsVencida => Estado == "Pendiente" && FechaLimite < DateTime.Now;

        // Propiedad calculada que lanza el mensaje dinámico para la interfaz
        [Ignore]
        public string MensajeEstadoTiempo
        {
            get
            {
                if (Estado == "Completada")
                    return "✅ Tarea completada a tiempo";
                
                if (EsVencida)
                    return "⚠️ ¡Alerta! No se completó a tiempo (Vencida)";
                
                return "⏳ En tiempo y forma";
            }
        }
    }
}