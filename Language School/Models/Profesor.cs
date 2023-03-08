using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.Models
{
    [Serializable]
    public class Profesor
    {
        private RegistrovaniKorisnik _korisnik;

        public RegistrovaniKorisnik Korisnik
        {
            get { return _korisnik; }
            set { _korisnik = value; }
        }

        private Skola _skola;

        public Skola SkolaZaposlen
        {
            get { return _skola; }
            set { _skola = value; }
        }
        public override string ToString()
        {
            return _korisnik.Ime + " " + _korisnik.Prezime;
        }

        private string _pzp;
        public string ProfesorZaPretragu
        {
            get { return _pzp; }
            set { _pzp = value; }
        }
        public string ProfesorZaUpisUFajl()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Ime i prezime: ");
            sb.Append(Korisnik.Ime);
            sb.Append(" ");
            sb.Append(Korisnik.Prezime);
            sb.Append(" ");
            sb.Append(", Adresa: ");
            sb.Append(Korisnik.Adresa);
            sb.Append(", Email: ");
            sb.Append(Korisnik.Email);
            
                       
            return sb.ToString();
        }

        private List<Jezik> _jezici;

        public List<Jezik> Jezici
        {
            get { return _jezici; }
            set { _jezici = value; }
        }
        private string _listajezika;

        public string ListaJezika
        {
            get { return _listajezika; }

            set { _listajezika = value; }
        }
        private List<Cas> _casovi;

        public List<Cas> Casovi
        {
            get { return _casovi; }

            set { _casovi = value; }

        }

    }
}