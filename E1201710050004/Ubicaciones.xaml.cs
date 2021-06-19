using E1201710050004.Clases;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E1201710050004
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ubicaciones : ContentPage
    {
        SQLiteAsyncConnection conn1 = new SQLiteAsyncConnection(App.UBICACIONDB1);
        public Ubicaciones()
        {
            InitializeComponent();
            mostrar();
        }

        private async void verMapa_Clicked(object sender, EventArgs e)
        {
            var lista = new Lista()
            {
                Id = Convert.ToString(id.Text),
                Latitud = latitud.Text,
                Longitud = longitud.Text,
                Ubicacion = ubicacion.Text,
                Descripcion_corta = ubicacion_corta.Text
            };
            var datos = new Mapa();
            datos.BindingContext = lista;
            await Navigation.PushAsync(datos);

        }

        private async void eliminar_Clicked(object sender, EventArgs e)
        {
            var ubicacionInfo = await getUbicacionAsyncId(Convert.ToInt32(id.Text));


            if (ubicacionInfo != null)
            {
                await DeleteAsync(ubicacionInfo);
                mostrar();
            }
            else
            {
                await DisplayAlert("Warning", "No ha seleccionado ubicacion para borrar", "Ok");
            }


        }
        private Task<UbicacionInfo> getUbicacionAsyncId(int id)
        {
            return conn1
                .Table<UbicacionInfo>()
                .Where(item => item.id == id)
                .FirstOrDefaultAsync();
        }

        private Task<int> DeleteAsync(UbicacionInfo ubicacion)
        {
            return conn1.DeleteAsync(ubicacion);
        }
        public async void mostrar()
        {
            try
            {


                var datos1 = await conn1.Table<UbicacionInfo>().ToArrayAsync();

                if (datos1 != null)
                {
                    datos.ItemsSource = datos1;
                }
            }
            catch (SQLite.SQLiteException e)
            {

            }
        }

        private async void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var ubicacionesInfo = new UbicacionInfo();
            var itemS = (UbicacionInfo)e.SelectedItem;
            if (!string.IsNullOrEmpty(itemS.id.ToString()))
            {
                var ubicacionz = await conn1.Table<UbicacionInfo>().Where(Ubicaciones => Ubicaciones.id == itemS.id).FirstOrDefaultAsync();

                if (ubicacionz != null)
                {
                    var listaUbicacion = new ListaUbicacion()
                    {
                        Id = ubicacionz.id,
                        Latitud = ubicacionz.latitud,
                        Longitud = ubicacionz.longitud,
                        Ubicacion = ubicacionz.ubicacion,
                        Descripcion_corta = ubicacionz.descripcion_corta

                    };

                    latitud.Text = ubicacionz.latitud;
                    longitud.Text = ubicacionz.longitud;
                    id.Text = ubicacionz.id.ToString();
                    ubicacion.Text = ubicacionz.ubicacion;
                    ubicacion_corta.Text = ubicacionz.descripcion_corta;

                }
            }
        }
    }

}