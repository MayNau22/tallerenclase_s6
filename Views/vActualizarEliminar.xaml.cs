using mnaupariS6.Models;
using System.Net;

namespace mnaupariS6.Views;

public partial class vActualizarEliminar : ContentPage
{
	public vActualizarEliminar(Estudiante datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre.ToString();
        txtApellido.Text = datos.apellido.ToString();
        txtEdad.Text = datos.edad.ToString();

    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {

        try
        {
            // Validar los datos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                !int.TryParse(txtEdad.Text, out int edad) ||
                edad <= 0 || edad > 120)
            {
                await DisplayAlert(
                    "Alerta",
                    "Ingrese todos los datos correctamente",
                    "OK"
                );
                return;
            }

            bool confirmar = await DisplayAlert(
                "Confirmar",
                "¿Desea actualizar los datos del estudiante?",
                "Sí",
                "No"
            );

            if (!confirmar)
                return;

            btnActualizar.IsEnabled = false;
            btnEliminar.IsEnabled = false;

            using WebClient cliente = new WebClient();

            var parametros =
                new System.Collections.Specialized.NameValueCollection();

            parametros.Add("codigo", txtCodigo.Text);
            parametros.Add("nombre", txtNombre.Text.Trim());
            parametros.Add("apellido", txtApellido.Text.Trim());
            parametros.Add("edad", edad.ToString());

            // Enviar solicitud PUT
            await cliente.UploadValuesTaskAsync(
                new Uri(
                    "http://192.168.56.1/moviles/ws_estudiante/post.php"
                ),
                "PUT",
                parametros
            );

            await DisplayAlert(
                "Correcto",
                "Estudiante actualizado correctamente",
                "OK"
            );

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                "No se pudo actualizar: " + ex.Message,
                "OK"
            );
        }
        finally
        {
            btnActualizar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
        }

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmar = await DisplayAlert(
                "Eliminar estudiante",
                "¿Está seguro de eliminar a " +
                txtNombre.Text + " " + txtApellido.Text + "?",
                "Sí, eliminar",
                "Cancelar"
            );

            if (!confirmar)
                return;

            btnActualizar.IsEnabled = false;
            btnEliminar.IsEnabled = false;

            using WebClient cliente = new WebClient();

            var parametros =
                new System.Collections.Specialized.NameValueCollection();

            parametros.Add("codigo", txtCodigo.Text);

            // Enviar solicitud DELETE
            await cliente.UploadValuesTaskAsync(
                new Uri(
                    "http://192.168.56.1/moviles/ws_estudiante/post.php"
                ),
                "DELETE",
                parametros
            );

            await DisplayAlert(
                "Correcto",
                "Estudiante eliminado correctamente",
                "OK"
            );

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                "No se pudo eliminar: " + ex.Message,
                "OK"
            );
        }
        finally
        {
            btnActualizar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
        }
    }
}