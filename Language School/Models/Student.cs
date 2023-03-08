using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.Models
{
    public class Student
    {
        private RegistrovaniKorisnik _korisnik;

        public RegistrovaniKorisnik Korisnik
        {
            get { return _korisnik; }
            set { _korisnik = value; }
        }
        private List<Cas> _rezCas;

        public List<Cas> RezCas
        {
            get { return _rezCas; }

            set { _rezCas = value; }

        }

        public string StudentZaUpisUFajl()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Korisnik.JMBG);
            sb.Append(";");


            foreach (Cas c in RezCas)
            {
                if (c != null)
                {
                    sb.Append(c.ID);
                    sb.Append(",");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            
            return sb.ToString();

        }
        public override string ToString()
        {
            return _korisnik.Ime + " " + _korisnik.Prezime;
        }
    }
}