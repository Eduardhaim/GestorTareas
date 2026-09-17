using GestorTareas.ViewModels;

namespace GestorTareas;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        // Aquí conectamos la Vista con el ViewModel cumpliendo el estándar MVVM
        BindingContext = new TareaViewModel(); 
    }

    private void OnPrioridadAbrirClicked(object sender, EventArgs e)
    {
        PrioridadOpciones.IsVisible = !PrioridadOpciones.IsVisible;
    }

    private void OnPrioridadSeleccionadaClicked(object sender, EventArgs e)
    {
        if (sender is Button button &&
            int.TryParse(button.CommandParameter?.ToString(), out var prioridad) &&
            prioridad is >= 1 and <= 3 &&
            BindingContext is TareaViewModel viewModel)
        {
            viewModel.Prioridad = prioridad;
            PrioridadOpciones.IsVisible = false;
        }
    }
}