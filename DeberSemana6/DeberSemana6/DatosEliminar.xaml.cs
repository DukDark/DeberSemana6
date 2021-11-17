using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeberSemana6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatosEliminar : ContentPage
    {
        public DatosEliminar(int codigo, string nombre, string apellido, int edad)
        {
            InitializeComponent();
            lblCodigo.Text = codigo.ToString();
            lblNombre.Text = nombre;
            lblApellido.Text = apellido;
            lblEdad.Text = edad.ToString();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                //Eliminar por ID
                var response = client.DeleteAsync(String.Format("http://192.168.100.12:8080/moviles/post.php?codigo={0}", lblCodigo.Text)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    await DisplayAlert("Mensaje de alerta", "Error al eliminar", "OK");
                }

                

                //Poner en blanco los campos
                lblNombre.Text = "";
                lblApellido.Text = "";
                lblEdad.Text = "";

                //Regresar a la pantalla de Vacunas
                await Navigation.PushAsync(new DatosEstudiante());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje de alerta", ex.Message, "OK");
            }

        }
    }
}