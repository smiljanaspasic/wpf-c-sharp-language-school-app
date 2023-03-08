using SR62_2021_POP2022.Models;
using SR62_2021_POP2022.MyExceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR62_2021_POP2022.Services
{
    class ProfessorService : IUserService
    {

        public void DeleteUser(string jmbg)
        {
            Profesor profesor = Data.Instance.Profesori.ToList().Find(p => p.Korisnik.JMBG.Equals(jmbg));
            if (profesor == null)
            {

                throw new UserNotFoundException($"Ne postoji profesor sa tim {jmbg} brojem");
            }
            profesor.Korisnik.Aktivan = false;
        }

        public bool Provera(RegistrovaniKorisnik reg)
        {
            throw new NotImplementedException();
        }

        public void ReadUsers(string filename)
        {
            Data.Instance.Profesori = new ObservableCollection<Profesor>();
            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                DataSet ds = new DataSet();

                string selectedProfesor = @"select * from Profesor";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectedProfesor, conn);
                dataAdapter.Fill(ds, "Profesor");

                foreach (DataRow dataRow in ds.Tables["Profesor"].Rows)
                {

                    RegistrovaniKorisnik registrovaniKorisnik = Data.Instance.Korisnici.ToList().Find(korisnik => korisnik.ID.Equals(dataRow["UserId"].ToString()));
                    if (registrovaniKorisnik.Aktivan)
                    {
                        Skola skola = Data.Instance.Skole.ToList().Find(sk => sk.ID.Equals(dataRow["SkolaId"].ToString()));
                        string line = dataRow["Jezici"].ToString();
                        string[] jezici = line.Split(',');
                        List<Jezik> listaJezika = new List<Jezik>();
                        foreach (string j in jezici)
                        {
                            Jezik jezik = new Jezik();
                            jezik.SetJezik(j);
                            listaJezika.Add(jezik);

                        }
                        List<Cas> listaCasova = new List<Cas>();
                        if (Data.Instance.Casovi != null)
                        {
                            string line1 = dataRow["Casovi"].ToString();
                            string[] casovi = line1.Split(',');
                            if (line1 != "")
                            {
                                foreach (string c in casovi)
                                {
                                    int idCas = int.Parse(c);
                                    Cas cas = Data.Instance.Casovi.ToList().Find(cc => cc.ID.Equals(idCas));
                                    listaCasova.Add(cas);
                                }
                            }
                        }
                        Profesor profesor = new Profesor
                        {
                            Korisnik = registrovaniKorisnik,
                            SkolaZaposlen = skola,
                            Jezici = listaJezika,
                            Casovi = listaCasova,
                            ListaJezika = line
                        };
                        profesor.ProfesorZaPretragu = profesor.ProfesorZaUpisUFajl();
                        Data.Instance.Profesori.Add(profesor);
                    }
                }
                conn.Close();
            }
        }

        public void SaveUsers(Object p)
        {
            Profesor profa = (Profesor)p;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand komanda = conn.CreateCommand();
                komanda.CommandText = @"select max(id) from dbo.Korisnik";
                int UserId = (int)komanda.ExecuteScalar();
                StringBuilder sb = new StringBuilder();
                foreach (Jezik j in profa.Jezici)
                {
                    sb.Append(j.GetJezik());
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
                string jezici = sb.ToString();
                sb = new StringBuilder();
                foreach (Cas c in profa.Casovi)
                {
                    if (c != null)
                    {
                        sb.Append(c.ID);
                        sb.Append(",");
                    }
                }
                if (sb.Length != 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                string casovi = sb.ToString();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                   insert into dbo.Profesor(UserId,SkolaId,Jezici,Casovi)
                   values (@UserId, @SkolaId, @Jezici, @Casovi)";

                command.Parameters.Add(new SqlParameter("UserId", UserId));
                command.Parameters.Add(new SqlParameter("SkolaId", profa.SkolaZaposlen.ID));
                command.Parameters.Add(new SqlParameter("Jezici", jezici));
                command.Parameters.Add(new SqlParameter("Casovi", casovi));

                command.ExecuteNonQuery();


                conn.Close();
            }
        }

        public void UpdateUser(Object p)
        {
            Profesor profa = (Profesor)p;
            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                foreach (Jezik j in profa.Jezici)
                {
                    sb.Append(j.GetJezik());
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);
                string jezici = sb.ToString();
                sb = new StringBuilder();
                foreach (Cas c in profa.Casovi)
                {
                    if (c != null)
                    {
                        sb.Append(c.ID);
                        sb.Append(",");
                    }
                }
                if (sb.Length != 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                string casovi = sb.ToString();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Profesor set 
                        SkolaId = @SkolaId,
                        Jezici = @Jezici,
                        Casovi = @Casovi
                        where UserId=@id";

                command.Parameters.Add(new SqlParameter("id", profa.Korisnik.ID));
                command.Parameters.Add(new SqlParameter("SkolaId", profa.SkolaZaposlen.ID));
                command.Parameters.Add(new SqlParameter("Jezici", jezici));
                command.Parameters.Add(new SqlParameter("Casovi", casovi));

                command.ExecuteScalar();
            }
        }
    }
}