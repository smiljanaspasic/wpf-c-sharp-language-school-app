using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SR62_2021_POP2022.Services;

namespace SR62_2021_POP2022.Models
{
    sealed class Data
    {
        private static readonly Data instance = new Data();
        public static readonly string CONNECTION_STRING = @"Server=.;Database=SkolaJezika;Trusted_Connection=True;";
        private IUserService userService;
        private IUserService professorService;
        private IUserService studentService;
        private ICasInterface casService;

        static Data() { }

        private Data()
        {
            userService = new UserService();
            professorService = new ProfessorService();
            studentService = new StudentService();
            casService = new CasService();
        }

        public static Data Instance
        {
            get
            {
                return instance;
            }
        }

        public ObservableCollection<RegistrovaniKorisnik> Korisnici { get; set; }
        public ObservableCollection<Profesor> Profesori { get; set; }

        public ObservableCollection<Skola> Skole { get; set; }

        public ObservableCollection<Cas> Casovi { get; set; }

        public ObservableCollection<Student> Studenti { get; set; }

        public ObservableCollection<Adresa> Adrese { get; set; }
        public RegistrovaniKorisnik trenutniKorisnik { get; set; }
        public void Initialize()
        {
            Adrese = new ObservableCollection<Adresa>();
            Skole = new ObservableCollection<Skola>();
            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                DataSet ds = new DataSet();

                string selectedAdresa = @"select * from Adresa";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectedAdresa, conn);
                dataAdapter.Fill(ds, "Adresa");

                foreach (DataRow dataRow in ds.Tables["Adresa"].Rows)
                {

                    Adresa adresa = new Adresa
                    {
                        ID = dataRow["Id"].ToString(),
                        Grad = dataRow["Grad"].ToString(),
                        Drzava = dataRow["Drzava"].ToString(),
                        Ulica = dataRow["Ulica"].ToString(),
                        Broj = dataRow["Broj"].ToString(),
                    };
                    Adrese.Add(adresa);
                }

                string selectedSkola = @"select * from Skola";
                dataAdapter = new SqlDataAdapter(selectedSkola, conn);
                dataAdapter.Fill(ds, "Skola");

                foreach (DataRow dataRow1 in ds.Tables["Skola"].Rows)
                {
                    Adresa adresa = Adrese.ToList().Find(sk => sk.ID.Equals(dataRow1["AdresaId"].ToString()));
                    string line = dataRow1["Jezici"].ToString();
                    string[] jezici = line.Split(',');
                    List<Jezik> listaJezika = new List<Jezik>();
                    foreach (string j in jezici)
                    {
                        Jezik jezik = new Jezik();
                        jezik.SetJezik(j);
                        listaJezika.Add(jezik);

                    }
                    Skola skola = new Skola
                    {
                        ID = dataRow1["Id"].ToString(),
                        Naziv = dataRow1["Naziv"].ToString(),
                        Adresa = adresa,
                        ListaJezika = line,
                        Jezici = listaJezika

                    };
                    Skole.Add(skola);
                }



                conn.Close();
            }


        }

        public void SacuvajEntitet(string filename, Object o)
        {
            if (filename.Contains("korisnici"))
            {
                userService.SaveUsers(o);
            }
            else if (filename.Contains("casovi"))
            {
                casService.SaveCas(o);
            }
            else if (filename.Contains("profesori"))
            {
                professorService.SaveUsers(o);
            }
            else if (filename.Contains("studenti"))
            {
                studentService.SaveUsers(o);
            }

        }

        public void AzurirajEntitet(string filename, Object o)
        {
            if (filename.Contains("korisnici"))
            {
                userService.UpdateUser(o);
            }
            else if (filename.Contains("casovi"))
            {
                casService.UpdateCas(o);
            }
            else if (filename.Contains("profesori"))
            {
                professorService.UpdateUser(o);
            }
            else if (filename.Contains("studenti"))
            {
                studentService.UpdateUser(o);
            }

        }

        public void CitanjeEntiteta(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                userService.ReadUsers(filename);
            }
            else if (filename.Contains("casovi"))
            {
                casService.ReadCas(filename);
            }
            else if (filename.Contains("profesori"))
            {
                professorService.ReadUsers(filename);
            }
            else if (filename.Contains("studenti"))
            {
                studentService.ReadUsers(filename);
            }

        }

        public void ObrisiKorisnika(string id)
        {
            userService.DeleteUser(id);
        }

        public void ObrisiCas(string id)
        {
            casService.DeleteCas(id);
        }

        public bool ProveriKorisnika(RegistrovaniKorisnik reg)
        {
            bool vrednost;
            vrednost = userService.Provera(reg);
            return vrednost;
        }
    }
}