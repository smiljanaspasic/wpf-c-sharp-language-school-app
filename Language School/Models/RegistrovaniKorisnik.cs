using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.Models
{
    [Serializable]
    public class RegistrovaniKorisnik
    {

        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _ime;

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        private string _prezime;

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }

        private string _JMBG;

        public string JMBG
        {
            get { return _JMBG; }
            set { _JMBG = value; }
        }

        private EPol _pol;

        public EPol Pol
        {
            get { return _pol; }
            set { _pol = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _lozinka;

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        private Adresa _adresa;

        public Adresa Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }


        private ETipKorisnika _tipKorisnika;

        public ETipKorisnika TipKorisnika
        {
            get { return _tipKorisnika; }
            set { _tipKorisnika = value; }
        }

        private bool _aktivan;

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }


        public RegistrovaniKorisnik() { }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }

        public string KorisnikZaUpisUFajl()
        {
            return Ime + ";" + Prezime + ";" + Email + ";" + Lozinka + ";" + JMBG.ToString() + ";" + Pol + ";" + Adresa.ID + ";" +  TipKorisnika + ";" + Aktivan;
        }
    }
}