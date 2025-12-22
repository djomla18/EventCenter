using EC.ClientApp.Models;
using System;
using System.Collections.Generic;
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

namespace EC.ClientApp.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly List<Gost> ListaGostiju;
        private readonly List<Gost> PristigliGosti;
        private bool isCapsLockActive = false;

        public MainView()
        {
            InitializeComponent();

            ListaGostiju = MockData();
            PristigliGosti = new List<Gost>();
            SearchBox.Focus();
        }

        private List<Gost> MockData()
        {
            return new List<Gost>()
            {
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Marko",
                    InicijalSrednjegImena = "P",
                    Prezime = "Jovanović",
                    PokupioRezervaciju = false
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Ana",
                    InicijalSrednjegImena = "M",
                    Prezime = "Petrović",
                    PokupioRezervaciju = false
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Marko",
                    InicijalSrednjegImena = "J",
                    Prezime = "Nikolić",
                    PokupioRezervaciju = false
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Jelena",
                    InicijalSrednjegImena = "D",
                    Prezime = "Stanković",
                    PokupioRezervaciju = false
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Petar",
                    InicijalSrednjegImena = "V",
                    Prezime = "Marković",
                    PokupioRezervaciju = false

                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Milica",
                    InicijalSrednjegImena = "S",
                    Prezime = "Đorđević",
                    PokupioRezervaciju = false

                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Nikola",
                    InicijalSrednjegImena = "M",
                    Prezime = "Jovanović",
                    PokupioRezervaciju = false
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Jovana",
                    InicijalSrednjegImena = "P",
                    Prezime = "Ilić",
                    PokupioRezervaciju = false
                }
            };
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchBox.Text;

            if (!string.IsNullOrEmpty(searchText))
            {

                var filteredResults = ListaGostiju.Where(g =>
                                                         g.Ime.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                                                         g.Prezime.Contains(searchText, StringComparison.InvariantCultureIgnoreCase))
                                                  .ToList();

                RezultatiPretrage.ItemsSource = filteredResults;
            }
            else
            {
                RezultatiPretrage.ItemsSource = ListaGostiju;
            }
        }

        private void GostCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var gost = checkBox.DataContext as Gost;
            PristigliGosti.Add(gost);


            if(PristigliGosti.Count >= 1)
            {
                PrikaziGostaNaMapiSedenja();
            }
            else
            {
                UkloniMapuSaPrikaza();
            }
        }

        private void UkloniGostaSaMapeSedenja()
        {
            MessageBox.Show($"Uklonjen gost sa mape. Trenutno ih je {PristigliGosti.Count}");
            PrikaziRasporedSedenja();
        }

        private void PrikaziGostaNaMapiSedenja()
        {
            MessageBox.Show($"Mapa za goste koji su do sada pristigli. Trenutno ih je {PristigliGosti.Count}");

            GlavniGrid.RowDefinitions[0].Height = new GridLength(209);
            GlavniGrid.RowDefinitions[1].Height = new GridLength(250);
            GlavniGrid.RowDefinitions[2].Height = new GridLength(0);
            GlavniGrid.RowDefinitions[3].Height = new GridLength(630);

            BorderTastature.Visibility = Visibility.Collapsed;
            BorderMape.Visibility = Visibility.Visible;

            PrikaziRasporedSedenja();
        }

        private void PrikaziRasporedSedenja()
        {
            var gosti = PristigliGosti.Select(g => g.KompletnoImePrezimeGosta).ToList();
            PristigliGostiTextBox.Visibility = Visibility.Visible;
            PotvrdiBtn.Visibility = Visibility.Visible;
            PristigliGostiTextBox.Text = string.Join(Environment.NewLine, gosti);
        }

        private void UkloniMapuSaPrikaza()
        {
            MessageBox.Show($"Uklanjamo mapu");

            GlavniGrid.RowDefinitions[0].Height = new GridLength(209);
            GlavniGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
            GlavniGrid.RowDefinitions[2].Height = new GridLength(350);
            GlavniGrid.RowDefinitions[3].Height = new GridLength(0);

            BorderTastature.Visibility = Visibility.Visible;
            BorderMape.Visibility = Visibility.Collapsed;
        }

        private void GostCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            Gost? gost = checkBox.DataContext as Gost;

            if (gost != null)
            {
                PristigliGosti.Remove(gost);
            }

            if (PristigliGosti.Count >= 1)
            {
                UkloniGostaSaMapeSedenja();
            }
            else
            {
                UkloniMapuSaPrikaza();
            }
        }

        private void KeyButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var searchBoxText = SearchBox.Text;

            var textDugmeta = button.Content.ToString();
            int trenutnaPozicijaKursora = SearchBox.CaretIndex;

            if (string.IsNullOrEmpty(searchBoxText) || isCapsLockActive)
            {
                SearchBox.Text = SearchBox.Text.Insert(trenutnaPozicijaKursora, textDugmeta);
            }
            else
            {
                SearchBox.Text = SearchBox.Text.Insert(trenutnaPozicijaKursora,textDugmeta.ToLower());
            }

            SearchBox.CaretIndex = trenutnaPozicijaKursora + 1;
            SearchBox.Focus();
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            string searchBoxText = SearchBox.Text;
            int indexKursora = SearchBox.CaretIndex;
            if (searchBoxText.Length > 0) {

                SearchBox.Text = searchBoxText.Substring(0, searchBoxText.Length - 1);
                if(indexKursora > 0)
                {
                    SearchBox.CaretIndex = indexKursora--;
                }
            }
            SearchBox.Focus();
        }

        private void SpaceButton_Click(object sender, RoutedEventArgs e)
        {
            int trenutniIndeks = SearchBox.CaretIndex;
            SearchBox.Text = SearchBox.Text.Insert(trenutniIndeks, " ");
            SearchBox.CaretIndex = trenutniIndeks + 1;
            SearchBox.Focus();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = string.Empty;
            SearchBox.Focus();
        }

        private void CapsLockButton_Click(object sender, RoutedEventArgs e)
        {
            isCapsLockActive = !isCapsLockActive;

            var button = sender as Button;
            if (isCapsLockActive)
            {
                button.Background = new SolidColorBrush(Color.FromRgb(56, 142, 60)); // Darker green
            }
            else
            {
                button.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80)); // Light green
            }
        }

        private async void ButtonPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            var listaIdevaGostiju = PristigliGosti.Select(g => g.IdGosta).ToList();

            await EvidentirajPristigleGoste(listaIdevaGostiju);

            MessageBox.Show("Rezervacije evidentirane");

            await Task.Delay(3000);
            VratiSeNaStranuZaPretragu();
       
        }

        private void VratiSeNaStranuZaPretragu()
        {
            PristigliGosti.Clear();
            BorderTastature.Visibility = Visibility.Visible;
            GlavniGrid.RowDefinitions[1].Height = new GridLength(209);
            GlavniGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
            GlavniGrid.RowDefinitions[2].Height = new GridLength(350);
            GlavniGrid.RowDefinitions[3].Height = new GridLength(0);
            SearchBox.Text = string.Empty;
            BorderMape.Visibility = Visibility.Hidden;
            BorderTastature.Visibility = Visibility.Visible;
        }

        private async Task EvidentirajPristigleGoste(List<Guid> listaIdevaGostiju)
        {
            
        }
    }
}
