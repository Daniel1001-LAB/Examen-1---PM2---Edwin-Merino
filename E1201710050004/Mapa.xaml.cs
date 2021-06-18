using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

namespace E1201710050004
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public Mapa()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Pin ubicacion = new Pin();
            ubicacion.Label = "Santa Barbara";
            ubicacion.Address = "Mikasa daniel";
          
            mapas.Pins.Add(ubicacion);

            var localizacion = await Geolocation.GetLastKnownLocationAsync();

            if (localizacion == null)
            {
                localizacion = await Geolocation.GetLocationAsync();
                
            }
            mapas.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(localizacion.Latitude, localizacion.Longitude), Distance.FromKilometers(1)));
        }
    }
}