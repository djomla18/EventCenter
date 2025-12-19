using EC.ClientApp.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EC.ClientApp.ViewModels
{
    /// <summary>
    /// Interaction logic for DobrodosliView.xaml
    /// </summary>
    public partial class DobrodosliView : Window
    {
        public DobrodosliView()
        {
            InitializeComponent();

            string nazivEventCentra;

            try
            {
               nazivEventCentra = ConfigurationManager.AppSettings["NazivEventCentra"].ToString();
            }
            catch (Exception)
            {
                nazivEventCentra = string.Empty;
            }

            Logo.Source = new BitmapImage(new Uri($"/Assets/Images/{nazivEventCentra}.png", UriKind.Relative));
        }

        private void OnReservationButtonClick(object sender, RoutedEventArgs e)
        {
            var mainView = new MainView();
            mainView.Show();

            this.Close();
        }
    }
}
