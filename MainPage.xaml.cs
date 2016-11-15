using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.Devices.Sensors;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace GyrometerRead
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Gyrometer _gyrometer;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            
            _gyrometer = Gyrometer.GetDefault();
            if (_gyrometer != null)
            {
                uint _minReportIntereval = _gyrometer.MinimumReportInterval;
                uint _reportInterval = _minReportIntereval > 16 ? _minReportIntereval : 16;
                _gyrometer.ReportInterval = _reportInterval;
                _gyrometer.ReadingChanged += new TypedEventHandler<Gyrometer, GyrometerReadingChangedEventArgs>(ReadingChanged);

            }
            _x_abb_start.IsEnabled = false;
             
        }

        private async void ReadingChanged(object sender, GyrometerReadingChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                GyrometerReading _reading = e.Reading;
                _x_txt_xReading.Text = string.Format("{0,5:0.00}", _reading.AngularVelocityX);
                _x_txt_yReading.Text = string.Format("{0,5:0.00}", _reading.AngularVelocityY);
                _x_txt_zReading.Text = string.Format("{0,5:0.00}", _reading.AngularVelocityZ);
        });
        }

    }
}
