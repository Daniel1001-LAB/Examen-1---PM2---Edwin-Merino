using E1201710050004.Clases;
using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace E1201710050004
{
    public partial class MainPage : ContentPage
    {
        double lat, lon;
        public MainPage()
        {
            InitializeComponent();
            Localizar();
        }
        private async void Localizar()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50; //Precision en metros
            if (locator.IsGeolocationAvailable)//Si el servicio esta disponible en el dispositivo
            {
                if (locator.IsGeolocationEnabled)//GPS activado
                {
                    if (!locator.IsListening)
                    {
                        await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 5);
                    }
                    locator.PositionChanged += (cambio, args) =>
                    {
                        var loc = args.Position;
                        latitud.Placeholder = loc.Latitude.ToString();
                        lat = double.Parse(latitud.Placeholder);
                        longitud.Placeholder = loc.Longitude.ToString();
                        lon = double.Parse(longitud.Placeholder);
                    };
                }
                else
                {
                    await DisplayAlert("Alerta", "La ubicacion esta desactivada, activala para usar el servicio", "ok");
                }
            }
            else
            {
                await DisplayAlert("Alerta", "El servicio de ubicacion esta apagado, activala para usar el servicio", "ok");
            }
        }




        private async void ubicaciones_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Ubicaciones());
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            var ubicacionN = new UbicacionInfo()
            {
                id = 0,
                latitud = latitud.Placeholder,
                longitud = longitud.Placeholder,
                ubicacion = ubicacion.Text,
                descripcion_corta = descripcion_corta.Text,
          
            };

            if (ubicacion.Text == null)
            {
                DisplayAlert("Alerta", "Llena la ubicacion de donde te encuentras ", "Ok");
            }

            if (descripcion_corta.Text == null)
            {
                DisplayAlert("Alerta", "Descripcion corta no puede estar vacio ", "Ok");
            }
            try
            {
                SQLiteConnection conn1 = new SQLiteConnection(App.UBICACIONDB1);
                conn1.CreateTable<UbicacionInfo>();
                conn1.Insert(ubicacionN);
                conn1.Close();

                

                await DisplayAlert("Success", "Ubicacion guardada con exito", "Ok");
                

                

            }
            catch (SQLiteException r)
            {
                DisplayAlert("Success", " Error al guardar datos " + r, "Ok");
            }


        }

    }


}
