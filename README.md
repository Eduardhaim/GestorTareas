# 📋 Proyecto 3: Gestor de Tareas con Prioridades (.NET MAUI & SQLite)

**Estudiante:**  
**Asignatura:** Programación 2  
**Institución:** CEUTEC  

---

## 🔍 1. Pregunta de Investigación (Problema a Resolver)
¿Cómo diseñar y desarrollar una aplicación de escritorio multiplataforma eficiente que permita organizar tareas pendientes mediante la asignación de prioridades, fechas y horas límite, garantizando la persistencia local de los datos y la detección visual de elementos vencidos bajo el patrón arquitectónico MVVM?

---

## 🎯 2. Objetivos del Proyecto
* **Objetivo General:** Construir un sistema de gestión de tareas utilizando .NET MAUI y SQLite, implementando la arquitectura MVVM estricta mediante el Community Toolkit para separar la lógica de negocio de la interfaz de usuario.
* **Objetivos Específicos:**
  1. Diseñar un modelo de datos robusto con atributos de SQLite que almacene nombre, descripción, fecha/hora límite, prioridad y estado.
  2. Implementar reglas de negocio automáticas para el cambio de estado de "Pendiente" a "Completada" con registro de marcas de tiempo (`DateTime.Now`).
  3. Desarrollar una interfaz gráfica intuitiva en XAML utilizando `DataTriggers` para el resaltado visual de tareas vencidas y prioridades diferenciadas por color.
  4. Garantizar el ordenamiento automático de la lista principal de tareas priorizando la fecha límite y el nivel de urgencia.

---

## 🧠 3. Saberes Previos
Para el desarrollo satisfactorio de este proyecto se requirió el dominio de los siguientes conceptos teóricos y prácticos:
* Programación Orientada a Objetos (POO) en C#.
* Fundamentos del patrón arquitectónico **Model-View-ViewModel (MVVM)** y enlace de datos (*Data Binding*).
* Manipulación de bases de datos relacionales locales utilizando la librería **SQLite-net-pcl**.
* Diseño de interfaces de usuario responsivas en entornos multiplataforma mediante **.NET MAUI (XAML)**.

---

## 🔬 4. Investigación y Marco Teórico
* **.NET MAUI:** Evolución de Xamarin.Forms que permite crear aplicaciones nativas para Windows, macOS, iOS y Android desde un único código base en C#.
* **CommunityToolkit.Mvvm:** Paquete oficial de Microsoft que facilita la implementación del patrón MVVM mediante atributos como `[ObservableProperty]` y `[RelayCommand]`, reduciendo drásticamente el código repetitivo (*boilerplate*).
* **SQLite Async:** Librería que permite realizar operaciones CRUD asíncronas en bases de datos embebidas, evitando el bloqueo del hilo principal de la interfaz gráfica durante transacciones de lectura y escritura.

---

## 📊 5. Análisis y Desarrollo (Implementación)
El proyecto se estructuró dividiendo responsabilidades en tres capas fundamentales:
1. **Modelos (`Models/Tarea.cs`):** Entidad principal con claves primarias autoincrementables y propiedades calculadas (`EsVencida`, `MensajeEstadoTiempo`) que evalúan dinámicamente las alertas de tiempo frente a `DateTime.Now`.
2. **Capa de Datos (`Data/TareaDatabase.cs`):** Conexión asíncrona local y consultas LINQ optimizadas para ordenar los registros con `.OrderBy(t => t.FechaLimite).ThenBy(t => t.Prioridad)`.
3. **ViewModels y Vistas (`ViewModels/TareaViewModel.cs` & `Views/MainPage.xaml`):** Gestión de comandos mediante `RelayCommand` y diseño visual basado en tarjetas con disparadores de datos condicionales (`DataTrigger`) para estados críticos (vencido o prioridades altas).

---

## 📝 6. Conclusiones
* Se cumplió a cabalidad con todos los requerimientos funcionales del Proyecto 3, logrando una sincronización perfecta entre la entrada de formularios (con fecha, hora y selectores de prioridad) y el almacenamiento persistente en SQLite.
* La implementación del patrón MVVM demostró ser altamente eficiente para mantener un código limpio, modular y fácil de escalar o someter a pruebas unitarias.
* El uso de alertas visuales mediante disparadores de datos incrementa notablemente la experiencia de usuario (UX), permitiendo identificar de un vistazo las tareas críticas o vencidas.