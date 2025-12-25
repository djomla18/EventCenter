using EC.ClientApp.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EC.ClientApp.Models
{
    class Gost
    {
        public Guid IdGosta { get; set; }

        public string Ime { get; set; }

        public string InicijalSrednjegImena { get; set; }

        public string Prezime { get; set; }

        public Pol Pol { get; set; }

        private bool _poupioRezervaciju { get; set; }

        public bool PokupioRezervaciju
        {
            get => _poupioRezervaciju;
            set 
            {
                if (_poupioRezervaciju != value)
                {
                    _poupioRezervaciju = value;
                    OnPropertyChanged();
                }

            }

        }

        public string KompletnoImePrezimeGosta =>
            !string.IsNullOrEmpty(InicijalSrednjegImena) ? $"{Ime} {InicijalSrednjegImena}. {Prezime}" : $"{Ime} {Prezime}";

        public int BrojStola { get; set; }

        public int BrojStolice { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
