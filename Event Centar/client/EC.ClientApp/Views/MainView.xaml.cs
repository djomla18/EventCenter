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
        readonly List<Gost> ListaGostiju;

        public MainView()
        {
            InitializeComponent();

            ListaGostiju = MockData();
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
                    Prezime = "Jovanović"
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Ana",
                    InicijalSrednjegImena = "M",
                    Prezime = "Petrović"
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Marko",
                    InicijalSrednjegImena = "J",
                    Prezime = "Nikolić"
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Jelena",
                    InicijalSrednjegImena = "D",
                    Prezime = "Stanković"
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Petar",
                    InicijalSrednjegImena = "V",
                    Prezime = "Marković"

                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Milica",
                    InicijalSrednjegImena = "S",
                    Prezime = "Đorđević"
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Nikola",
                    InicijalSrednjegImena = "M",
                    Prezime = "Jovanović",
                },
                new Gost
                {
                    IdGosta = Guid.NewGuid(),
                    Ime = "Jovana",
                    InicijalSrednjegImena = "P",
                    Prezime = "Ilić"
                }
            };
        }
    }
}
