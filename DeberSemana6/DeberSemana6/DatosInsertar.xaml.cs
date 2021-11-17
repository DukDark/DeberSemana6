using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeberSemana6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatosInsertar : ContentPage
    {
        public DatosInsertar()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtedad.Text) > 0 && Convert.ToInt32(txtCodigo.Text) > 0)
                {
                    WebClient client = new WebClient();

                    var parametros = new System.Collections.Specialized.NameValueCollection();

                    parametros.Add("codigo", txtCodigo.Text);
                    parametros.Add("nombre", txtNombre.Text);
                    parametros.Add("apellido", txtapellido.Text);
                    parametros.Add("edad", txtedad.Text);

                    client.UploadValues("http://192.168.100.12:8080/moviles/post.php", "POST", parametros);




                    //Poner en blanco los campos
                    txtCodigo.Text = "";
                    txtNombre.Text = "";
                    txtapellido.Text = "";
                    txtedad.Text = "";

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
