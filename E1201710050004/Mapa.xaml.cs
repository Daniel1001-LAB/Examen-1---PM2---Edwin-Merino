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
            Pin pin = new Pin
            {
                Label = ubicacion.Text,
                Address = ubicacion_corta.Text,
                Type = PinType.Generic,
                Position = new Position(Convert.ToDouble(latitud.Text), Convert.ToDouble(longitud.Text))
            };

            mapas.Pins.Add(pin);
            mapas.MoveToRegion(mapSpan:MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(latitud.Text), Convert.ToDouble(longitud.Text)), Distance.FromKilometers(1)));

        }
    }
}