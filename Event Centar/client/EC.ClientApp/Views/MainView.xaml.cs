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
using EC.ClientApp.Helpers.Enums;
using System.Net.Http;
using System.Configuration;

namespace EC.ClientApp.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly List<Gost> ListaGostiju;
        private readonly List<Gost> PristigliGosti;
        private readonly List<Sto> ListaStolova;
        private bool isCapsLockActive = false;
        private readonly string BaseAddress = ConfigurationManager.AppSettings["ApiURL"]!.ToString();


        public MainView()
        {
            InitializeComponent();

            SearchBox.PreviewMouseDown += SearchBox_PreviewMouseDown;

            ListaGostiju = MockData();
            PristigliGosti = new List<Gost>();
            ListaStolova = MockStoloviData();
            NacrtajMapuSedenja();
            SearchBox.Focus();
        }

        private void NacrtajMapuSedenja()
        {
            MapaCanvas.Children.Clear();

            foreach(var sto in ListaStolova)
            {
                NacrtajSto(sto);
                NacrtajStoliceZaSto(sto);
            }
        }

        private void NacrtajStoliceZaSto(Sto sto)
        {
            var gostiZaOdredjeniStolom = ListaGostiju.Where(g => g.BrojStola == sto.BrojStola).ToList();

            List<(double x, double y)> pozicijeStolicaZaStolom = VratiPozicijeStolicaZaStolom(sto);

            for (int i = 0; i < sto.KapacitetStola && i < pozicijeStolicaZaStolom.Count; i++) {

                var gostNaStolici = PristigliGosti.FirstOrDefault(g => g.BrojStola == sto.BrojStola && g.BrojStolice == i + 1);
                bool jeIstaknut = gostNaStolici != null;

                Ellipse chair = new Ellipse
                {
                    Width = 35,  // ← Slightly bigger
                    Height = 35,
                    Fill = jeIstaknut ? Brushes.Gold : Brushes.LightGray,
                    Stroke = jeIstaknut ? Brushes.DarkGoldenrod : Brushes.Gray,
                    StrokeThickness = jeIstaknut ? 3 : 2  // ← Thicker border when highlighted
                };

                Canvas.SetLeft(chair, pozicijeStolicaZaStolom[i].x);
                Canvas.SetTop(chair, pozicijeStolicaZaStolom[i].y);
                MapaCanvas.Children.Add(chair);

                // Chair number
                TextBlock chairLabel = new TextBlock
                {
                    Text = (i + 1).ToString(),
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Foreground = jeIstaknut ? Brushes.Black : Brushes.DarkGray,
                    TextAlignment = TextAlignment.Center,  // ← Center the text
                    Width = 35,  // ← Match chair width
                    Height = 35  // ← Match chair height
                };

                Canvas.SetLeft(chairLabel, pozicijeStolicaZaStolom[i].x);
                Canvas.SetTop(chairLabel, pozicijeStolicaZaStolom[i].y + 8);  
                MapaCanvas.Children.Add(chairLabel);

                if (jeIstaknut)
                {
                    TextBlock nameLabel = new TextBlock
                    {
                        Text = gostNaStolici.Ime,
                        FontSize = 12,
                        FontWeight = FontWeights.SemiBold,
                        Foreground = Brushes.DarkGoldenrod,
                        Background = new SolidColorBrush(Color.FromArgb(200, 255, 255, 255)),
                        Padding = new Thickness(4, 2, 4, 2),
                        TextAlignment = TextAlignment.Center  // ← Center text
                    };

                    // Measure text to center it properly
                    nameLabel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    double textWidth = nameLabel.DesiredSize.Width;

                    Canvas.SetLeft(nameLabel, pozicijeStolicaZaStolom[i].x + 17.5 - (textWidth / 2));  // Center horizontally
                    Canvas.SetTop(nameLabel, pozicijeStolicaZaStolom[i].y + 38);  // Below the chair
                    MapaCanvas.Children.Add(nameLabel);
                }
            }

        }

        private List<(double x, double y)> VratiPozicijeStolicaZaStolom(Sto sto)
        {
            var positions = new List<(double x, double y)>();

            if (sto.OblikStola == OblikStolaEnum.STO_OKRUGLI)
            {
                // Chairs around circular table (4 chairs)
                double centerX = sto.XKordinata + 60;  // Center of 120px circle
                double centerY = sto.YKordinata + 60;
                double radius = 80;  // Distance from center

                for (int i = 0; i < sto.KapacitetStola; i++)
                {
                    double angle = (2 * Math.PI * i) / sto.KapacitetStola;
                    double x = centerX + radius * Math.Cos(angle) - 15;  // -15 to center the chair
                    double y = centerY + radius * Math.Sin(angle) - 15;
                    positions.Add((x, y));
                }
            }
            else if (sto.OblikStola == OblikStolaEnum.STO_PRAVOUGAONI)
            {
                // Chairs around rectangular table (4 chairs: 2 on sides, 1 on each end)
                double tableWidth = 150;
                double tableHeight = 80;

                // Top
                positions.Add((sto.XKordinata + tableWidth / 2 - 15, sto.YKordinata - 40));
                // Right
                positions.Add((sto.XKordinata + tableWidth + 10, sto.YKordinata + tableHeight / 2 - 15));
                // Bottom
                positions.Add((sto.XKordinata + tableWidth / 2 - 15, sto.YKordinata + tableHeight + 10));
                // Left
                positions.Add((sto.XKordinata - 40, sto.YKordinata + tableHeight / 2 - 15));
            }
            else
            {
                double tableSize = 120;

                // Top
                positions.Add((sto.XKordinata + tableSize / 2 - 15, sto.YKordinata - 40));
                // Right
                positions.Add((sto.XKordinata + tableSize + 10, sto.YKordinata + tableSize / 2 - 15));
                // Bottom
                positions.Add((sto.XKordinata + tableSize / 2 - 15, sto.YKordinata + tableSize + 10));
                // Left
                positions.Add((sto.XKordinata - 40, sto.YKordinata + tableSize / 2 - 15));
            }

            return positions;
        }

        private void NacrtajSto(Sto sto)
        {
            if (sto.OblikStola == OblikStolaEnum.STO_OKRUGLI)
            {
                Ellipse oblikStola = new Ellipse
                {
                    Width = 120,
                    Height = 120,
                    Fill = new SolidColorBrush(Color.FromRgb(139, 69, 19)),  // Brown
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(oblikStola, sto.XKordinata);
                Canvas.SetTop(oblikStola, sto.YKordinata);
                MapaCanvas.Children.Add(oblikStola);

                // Labela za broj stola
                TextBlock label = new TextBlock
                {
                    Text = sto.BrojStola.ToString(),
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                    TextAlignment = TextAlignment.Center,
                    Width = 120,  // ← Match table width
                    Height = 120  // ← Match table height
                };

                Canvas.SetLeft(label, sto.XKordinata);
                Canvas.SetTop(label, sto.YKordinata + 45);  // Vertical centering
                MapaCanvas.Children.Add(label);

            }
            else if (sto.OblikStola == OblikStolaEnum.STO_PRAVOUGAONI) 
            {
                Rectangle oblikStola = new Rectangle
                {
                    Width = 150,
                    Height = 80,
                    Fill = new SolidColorBrush(Color.FromRgb(139, 69, 19)),  // Brown
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(oblikStola, sto.XKordinata);
                Canvas.SetTop(oblikStola, sto.YKordinata);
                MapaCanvas.Children.Add(oblikStola);

                // Labela za broj stola
                TextBlock label = new TextBlock
                {
                    Text = sto.BrojStola.ToString(),
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                    TextAlignment = TextAlignment.Center,
                    Width = 150,  // ← Match table width
                    Height = 80   // ← Match table height
                };

                Canvas.SetLeft(label, sto.XKordinata);
                Canvas.SetTop(label, sto.YKordinata + 25);  // Vertical centering
                MapaCanvas.Children.Add(label);

            }
            else
            {
                Rectangle oblikStola = new Rectangle
                {
                    Width = 120,
                    Height = 120,
                    Fill = new SolidColorBrush(Color.FromRgb(139, 69, 19)),  // Brown
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    RadiusX = 5,
                    RadiusY = 5
                };

                Canvas.SetLeft(oblikStola, sto.XKordinata);
                Canvas.SetTop(oblikStola, sto.YKordinata);
                MapaCanvas.Children.Add(oblikStola);

                // Labela za broj stola
                TextBlock label = new TextBlock
                {
                    Text = sto.BrojStola.ToString(),
                    FontSize = 24,
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.White,
                    TextAlignment = TextAlignment.Center,
                    Width = 120,  // ← Match table width
                    Height = 120
                };

                Canvas.SetLeft(label, sto.XKordinata);
                Canvas.SetTop(label, sto.YKordinata + 45);  // Vertical centering
                MapaCanvas.Children.Add(label);

            }
        }

        private List<Sto> MockStoloviData()
        {
            return new List<Sto>
            {
                new Sto { BrojStola = 1, OblikStola = OblikStolaEnum.STO_OKRUGLI, KapacitetStola = 4, XKordinata = 200, YKordinata = 150 },
                new Sto { BrojStola = 2, OblikStola = OblikStolaEnum.STO_PRAVOUGAONI, KapacitetStola = 4, XKordinata = 500, YKordinata = 150 },
                new Sto { BrojStola = 3, OblikStola = OblikStolaEnum.STO_KVADRATNI, KapacitetStola = 4, XKordinata = 800, YKordinata = 150 }
            };
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
                    PokupioRezervaciju = false,
                    Pol = Pol.Muski,
                    BrojStola = 1,
                    BrojStolice = 1
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Ana",
                    InicijalSrednjegImena = "M",
                    Pol = Pol.Zenski,
                    Prezime = "Petrović",
                    PokupioRezervaciju = false,
                    BrojStola = 1,
                    BrojStolice = 2
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Marko",
                    InicijalSrednjegImena = "J",
                    Prezime = "Nikolić",
                    Pol = Pol.Muski,
                    PokupioRezervaciju = false,
                    BrojStola = 2,
                    BrojStolice = 1
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Jelena",
                    InicijalSrednjegImena = "D",
                    Pol = Pol.Zenski,
                    Prezime = "Stanković",
                    PokupioRezervaciju = false,
                    BrojStola = 2,
                    BrojStolice = 2
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Petar",
                    InicijalSrednjegImena = "V",
                    Pol = Pol.Muski,
                    Prezime = "Marković",
                    PokupioRezervaciju = false,
                    BrojStola = 3,
                    BrojStolice = 1

                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Milica",
                    InicijalSrednjegImena = "S",
                    Pol = Pol.Zenski,
                    Prezime = "Đorđević",
                    PokupioRezervaciju = false,
                    BrojStola = 3,
                    BrojStolice = 2

                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Nikola",
                    InicijalSrednjegImena = "M",
                    Pol = Pol.Muski,
                    Prezime = "Jovanović",
                    PokupioRezervaciju = false,
                    BrojStola = 3,
                    BrojStolice = 3
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Jovana",
                    InicijalSrednjegImena = "P",
                    Pol = Pol.Zenski,
                    Prezime = "Ilić",
                    PokupioRezervaciju = false,
                    BrojStola = 3,
                    BrojStolice = 4
                }
            };
        }

        private void UkloniGostaSaMapeSedenja()
        {
            DobrodosliPoruka.ItemsSource = GenerisiPorukuDobrodoslice();
            PrikaziRasporedSedenja();
        }

        private void PrikaziGostaNaMapiSedenja()
        {
            GlavniGrid.RowDefinitions[0].Height = new GridLength(213);
            GlavniGrid.RowDefinitions[1].Height = new GridLength(0);
            GlavniGrid.RowDefinitions[2].Height = new GridLength(0);
            GlavniGrid.RowDefinitions[3].Height = new GridLength(1, GridUnitType.Star);

            BorderTastature.Visibility = Visibility.Collapsed;
            BorderMape.Visibility = Visibility.Visible;
            PrikaziRasporedSedenja();
        }

        private void PrikaziRasporedSedenja()
        {
            PotvrdiBtn.Visibility = Visibility.Visible;

            DobrodosliPoruka.ItemsSource = GenerisiPorukuDobrodoslice();
            NacrtajMapuSedenja();
        }

        private void UkloniMapuSaPrikaza()
        {
            MessageBox.Show($"Uklanjamo mapu");

            GlavniGrid.RowDefinitions[0].Height = new GridLength(213);
            GlavniGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
            GlavniGrid.RowDefinitions[2].Height = new GridLength(350);
            GlavniGrid.RowDefinitions[3].Height = new GridLength(0);

            BorderTastature.Visibility = Visibility.Visible;
            BorderMape.Visibility = Visibility.Collapsed;
        }

        private void VratiSeNaStranuZaPretragu()
        {
            PristigliGosti.Clear();

            DobrodosliPoruka.ItemsSource = null;

            BorderTastature.Visibility = Visibility.Visible;
            GlavniGrid.RowDefinitions[0].Height = new GridLength(213);
            GlavniGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
            GlavniGrid.RowDefinitions[2].Height = new GridLength(350);
            GlavniGrid.RowDefinitions[3].Height = new GridLength(0);

            SearchBox.Text = string.Empty;
            SearchBox.IsReadOnly = false;
            SearchBox.IsEnabled = true;
            BorderMape.Visibility = Visibility.Collapsed;
            BorderTastature.Visibility = Visibility.Visible;
        }
        
        private List<string> GenerisiPorukuDobrodoslice()
        {
            List<string> poruke = new List<string>();

            foreach(var gost in PristigliGosti)
            {
                if (gost.Pol == Pol.Muski)
                {
                    poruke.Add($"Dobrodošli gospodine {gost.Prezime}. Vaše mesto je {gost.BrojStolice} za stolom {gost.BrojStola}.");
  }
                else
                {
                    poruke.Add($"Dobrodošli gospođo {gost.Prezime}. Vaše mesto je {gost.BrojStolice} za stolom {gost.BrojStola}.");
                }
            }

            return poruke;

        }
        private bool IsElementOrChild(DependencyObject parent, DependencyObject child)
        {
            if (child == null) return false;

            DependencyObject current = child;
            while (current != null)
            {
                if (current == parent)
                    return true;

                current = VisualTreeHelper.GetParent(current);
            }
            return false;
        }

        private async Task EvidentirajPristigleGoste(List<Guid> listaIdevaGostiju)
        {

        }

        #region Event Handlers

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchBox.Text;

            if (!string.IsNullOrEmpty(searchText))
            {

                var filteredResults = ListaGostiju.Where(g =>
                                                         (g.Ime.Contains(searchText, StringComparison.InvariantCultureIgnoreCase) ||
                                                         g.Prezime.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)) &&
                                                         g.PokupioRezervaciju == false)
                                                  .ToList();

                RezultatiPretrage.ItemsSource = filteredResults;
            }
            else
            {
                RezultatiPretrage.ItemsSource = ListaGostiju.Where(g => g.PokupioRezervaciju == false);
            }
        }

        private void GostCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var gost = checkBox.DataContext as Gost;
            PristigliGosti.Add(gost);

            if (PristigliGosti.Count >= 1)
            {
                PrikaziGostaNaMapiSedenja();
            }
            else
            {
                UkloniMapuSaPrikaza();
            }
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
                button.Background = new SolidColorBrush(Color.FromRgb(244, 67, 54)); // Darker green
                CapsLockBtn.Content = "⬇";
            }
            else
            {
                button.Background = new SolidColorBrush(Color.FromRgb(76, 175, 80)); // Light green
                CapsLockBtn.Content = "⬆️";
            }
        }

        private async void ButtonPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            var listaIdevaGostiju = PristigliGosti.Select(g => g.IdGosta).ToList();

            await EvidentirajPristigleGoste(listaIdevaGostiju);

            // Pre vracanja na pocetnu stranu
            SearchBox.IsReadOnly = true;
            SearchBox.IsEnabled = false;

            foreach (var gost in PristigliGosti)
            {
                gost.PokupioRezervaciju = true;
               // ListaGostiju.Remove(gost);
            }

            MessageBox.Show("Rezervacije evidentirane");

            //await Task.Delay(3000);
            VratiSeNaStranuZaPretragu();
       
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            GlavniGrid.RowDefinitions[0].Height = new GridLength(213);
            GlavniGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
            GlavniGrid.RowDefinitions[2].Height = new GridLength(350);
            GlavniGrid.RowDefinitions[3].Height = new GridLength(0);

            BorderTastature.Visibility = Visibility.Visible;
            BorderMape.Visibility = Visibility.Collapsed;
        }

        private void GlavniGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element)
            {
                if (IsElementOrChild(SearchBox, element) ||
                    IsElementOrChild(BorderTastature, element) ||
                    element is CheckBox)
                {
                    return;
                }

                // Show map for EVERYTHING else
                if (PristigliGosti.Count >= 1 && BorderMape.Visibility == Visibility.Collapsed)
                {
                    PrikaziGostaNaMapiSedenja();
                }
            }
        }

        private void SearchBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlavniGrid.RowDefinitions[0].Height = new GridLength(213);
            GlavniGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
            GlavniGrid.RowDefinitions[2].Height = new GridLength(350);
            GlavniGrid.RowDefinitions[3].Height = new GridLength(0);

            BorderTastature.Visibility = Visibility.Visible;
            BorderMape.Visibility = Visibility.Collapsed;
        }

        #endregion

    }
}
