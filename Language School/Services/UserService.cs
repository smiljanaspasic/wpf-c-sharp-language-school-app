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
    class UserService : IUserService
    {

        public bool Provera(RegistrovaniKorisnik rk)
        {
            RegistrovaniKorisnik registrovaniKorisnik = Data.Instance.Korisnici.ToList().Find(k => k.JMBG.Equals(rk.JMBG) && k.Lozinka.Equals(rk.Lozinka) && k.TipKorisnika.Equals(rk.TipKorisnika));
            if (registrovaniKorisnik != null)
            {
                Data.Instance.trenutniKorisnik = registrovaniKorisnik;
                return true;
            }
            else return false;
        }
        public void SaveUsers(Object rk)
        {
            RegistrovaniKorisnik registrovani = (RegistrovaniKorisnik)rk;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                    insert into dbo.Korisnik(Ime,Prezime,Jmbg,Email,Password,Pol,Tip,Aktivan,AdresaId)
                    values (@Ime, @Prezime, @Jmbg, @Email, @Password, @Pol, @Tip, @Aktivan, @AdresaId)";

                command.Parameters.Add(new SqlParameter("Ime", registrovani.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", registrovani.Prezime));
                command.Parameters.Add(new SqlParameter("Jmbg", registrovani.JMBG));
                command.Parameters.Add(new SqlParameter("Email", registrovani.Email));
                command.Parameters.Add(new SqlParameter("Password", registrovani.Lozinka));
                command.Parameters.Add(new SqlParameter("Pol", registrovani.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Tip", registrovani.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", registrovani.Aktivan));
                command.Parameters.Add(new SqlParameter("AdresaId", registrovani.Adresa.ID));
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void ReadUsers(string filename)
        {
            Data.Instance.Korisnici = new ObservableCollection<RegistrovaniKorisnik>();

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                DataSet ds = new DataSet();

                string selectedKorisnik = @"select * from Korisnik";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectedKorisnik, conn);
                dataAdapter.Fill(ds, "Korisnik");

                foreach (DataRow dataRow in ds.Tables["Korisnik"].Rows)
                {


                    Enum.TryParse(dataRow["Pol"].ToString(), out EPol pol); ;
                    Adresa adresa = Data.Instance.Adrese.ToList().Find(adresaa => adresaa.ID.Equals(dataRow["AdresaId"].ToString()));
                    Enum.TryParse(dataRow["Tip"].ToString(), out ETipKorisnika tip);
                    Boolean.TryParse(dataRow["Aktivan"].ToString(), out Boolean aktivan);



                    RegistrovaniKorisnik registrovaniKorisnik = new RegistrovaniKorisnik
                    {

                        ID = dataRow["Id"].ToString(),
                        Ime = dataRow["Ime"].ToString(),
                        Prezime = dataRow["Prezime"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        Lozinka = dataRow["Password"].ToString(),
                        JMBG = dataRow["Jmbg"].ToString(),
                        Pol = pol,
                        Adresa = adresa,
                        TipKorisnika = tip,
                        Aktivan = aktivan
                    };

                    Data.Instance.Korisnici.Add(registrovaniKorisnik);
                }
                conn.Close();
            }

        }

        public void DeleteUser(string id)
        {
            RegistrovaniKorisnik registrovaniKorisnik = Data.Instance.Korisnici.ToList().Find(k => k.ID.Equals(id));
            if (registrovaniKorisnik == null)
            {

                throw new UserNotFoundException($"Ne postoji korisnik sa tim {id} brojem");
            }
            registrovaniKorisnik.Aktivan = false;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Korisnik set Aktivan=0 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();

                conn.Close();
            }

        }

        public void UpdateUser(object rk)
        {
            RegistrovaniKorisnik registrovani = (RegistrovaniKorisnik)rk;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update dbo.Korisnik set
                        Ime = @Ime,
                        Prezime = @Prezime,
                        Jmbg = @Jmbg,
                        Email = @Email,
                        Password = @Password,
                        Pol = @Pol,
                        Tip = @Tip,
                        Aktivan = @Aktivan,
                        AdresaId = @AdresaId
                        where Id = @id";

                command.Parameters.Add(new SqlParameter("id", registrovani.ID));
                command.Parameters.Add(new SqlParameter("Ime", registrovani.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", registrovani.Prezime));
                command.Parameters.Add(new SqlParameter("Jmbg", registrovani.JMBG));
                command.Parameters.Add(new SqlParameter("Email", registrovani.Email));
                command.Parameters.Add(new SqlParameter("Password", registrovani.Lozinka));
                command.Parameters.Add(new SqlParameter("Pol", registrovani.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Tip", registrovani.TipKorisnika.ToString()));
                command.Parameters.Add(new SqlParameter("Aktivan", registrovani.Aktivan));
                command.Parameters.Add(new SqlParameter("AdresaId", registrovani.Adresa.ID));
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}