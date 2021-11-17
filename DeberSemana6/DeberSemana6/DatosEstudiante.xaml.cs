using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeberSemana6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatosEstudiante : ContentPage
    {
        private const string Url = "http://192.168.100.12:8080/moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<DeberSemana6.Datos> _post;
        public DatosEstudiante()
        {
            InitializeComponent();
            get();
        }

        public async void get()
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<DeberSemana6.Datos> posts = JsonConvert.DeserializeObject<List<DeberSemana6.Datos>>(content);
                _post = new ObservableCollection<DeberSemana6.Datos>(posts);

                MyListView.ItemsSource = _post;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje de alerta", ex.Message, "OK");
            }
        }



        private async void MenuItem_Actualizar(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var datos = menuItem.CommandParameter as DeberSemana6.Datos;

            await Navigation.PushAsync(new DatosActualizar(datos.codigo, datos.nombre, datos.apellido, datos.edad));
        }

        private async void MenuItem_Eliminar(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var datos = menuItem.CommandParameter as DeberSemana6.Datos;

            await Navigation.PushAsync(new DatosEliminar(datos.codigo, datos.nombre, datos.apellido, datos.edad));

        }

        private async void btnInsertar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DatosInsertar());

        }
    }
}