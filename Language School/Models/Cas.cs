using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.Models
{
    public class Cas

    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private Profesor _profesor;

        public Profesor Profesor
        {
            get { return _profesor; }

            set { _profesor = value; }

        }
        public DateTime _datum=DateTime.Now;

        public DateTime Datum_i_Vreme
        {
            get { return _datum; }

            set { _datum = value; }

        }
        private int _trajanje;

        public int Trajanje
        {
            get { return _trajanje; }

            set { _trajanje = value; }
        }

        private ECstatus _cstatus;

        public ECstatus Status
        {
            get { return _cstatus; }

            set { _cstatus = value; }

        }

        private Student _student;

        public Student Student
        {
            get { return _student; }
            set { _student = value; }
        }
        private bool _obrisan;

        public bool Obrisan
        {
            get { return _obrisan; }
            set { _obrisan = value; }
        }

        public string CasZaUpisUFajl()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ID);
            sb.Append(";");
            sb.Append(Profesor.Korisnik.JMBG);
            sb.Append(";");
            sb.Append(Datum_i_Vreme);
            sb.Append(";");
            sb.Append(Trajanje);
            sb.Append(";");
            sb.Append(Status);
            sb.Append(";");
            if (Student != null)
            {
                sb.Append(Student.Korisnik.JMBG);

            }
            sb.Append(";");
            return sb.ToString();
        }
        public override string ToString()
        {
            return "Profesor" + _profesor.Korisnik.Ime + " " + _profesor.Korisnik.Prezime + ",Datum i Vreme:" + _datum + ",Trajanje:" + _trajanje;
        }
    }



}