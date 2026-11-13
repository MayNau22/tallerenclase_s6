
using mnaupariS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace mnaupariS6.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "http://192.168.56.1/moviles/ws_estudiante/post.php";

    private readonly HttpClient _cliente = new HttpClient();

    private ObservableCollection<Estudiante> _estudiante = new();

    public vEstudiante()
    {
        InitializeComponent();

        listaEstudiante.ItemsSource = _estudiante;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await MostrarEstudiante();
    }

    public async Task MostrarEstudiante()
    {
        try
        {
            // Consultar servicio web
            var response = await _cliente.GetAsync(Url);

            // Verificar respuesta HTTP
            response.EnsureSuccessStatusCode();

            // Obtener contenido JSON
            var content = await response.Content.ReadAsStringAsync();

            // Convertir JSON a lista de estudiantes
            List<Estudiante> mostrar =
                JsonConvert.DeserializeObject<List<Estudiante>>(content)
                ?? new List<Estudiante>();

            // Actualizar colección
            _estudiante.Clear();

            foreach (var estudiante in mostrar)
            {
                _estudiante.Add(estudiante);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                "No se pudieron cargar los estudiantes: " + ex.Message,
                "Aceptar"
            );
        }
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vAgregarEstudiante());
    }

    private async void listaEstudiante_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        Estudiante estudianteSeleccionado =
            e.CurrentSelection[0] as Estudiante;

        if (estudianteSeleccionado == null)
            return;

        // Limpiar la selección
        listaEstudiante.SelectedItem = null;

        // Abrir formulario con los datos seleccionados
        await Navigation.PushAsync(
            new vActualizarEliminar(estudianteSeleccionado)
        );

    }
}
