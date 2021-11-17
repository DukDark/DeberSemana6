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
    public partial class DatosActualizar : ContentPage
    {
        public DatosActualizar(int codigo, string nombre, string apellido, int edad)
        {
            InitializeComponent();
            lblCodigo.Text = codigo.ToString();
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtEdad.Text = edad.ToString();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtEdad.Text) > 0)
                {
                    HttpClient client = new HttpClient();

                    //Actualizar con PUT
                    var response = client.PutAsync(String.Format("http://192.168.100.12:8080/moviles/post.php?codigo={0}&nombre={1}&apellido={2}&edad={3}", lblCodigo.Text, txtNombre.Text, txtApellido.Text, txtEdad.Text), null).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var resultString = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        await DisplayAlert("Mensaje de alerta", "Error al actualizar", "OK");
                    }

                    //Utilizar TOAST
                    

                    //Poner en blanco los campos
                    txtNombre.Text = "";
                    txtApellido.Text = "";
                    txtEdad.Text = "";

                    //Regresar a la pantalla de Vacunas
                    await Navigation.PushAsync(new DatosEstudiante());
                }
                else
                {
                    await DisplayAlert("Mensaje de alerta", "La dosis debe ser mayor a 0", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje de alerta", ex.Message, "OK");
            }


        }
    }
}