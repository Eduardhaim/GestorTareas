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
}