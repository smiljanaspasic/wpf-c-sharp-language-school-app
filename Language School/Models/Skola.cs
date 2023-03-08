using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SR62_2021_POP2022.Models
{
    public class Skola
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _naziv;

        public string Naziv
        {
            get { return _naziv; }

            set { _naziv = value; }
        }

        private Adresa _adresa;

        public Adresa Adresa
        {

        get {return _adresa;}

        set {_adresa = value; }
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

        public Skola()
        { }
        
        public override string ToString()
        {
            return _naziv ;
        }
    }
}
