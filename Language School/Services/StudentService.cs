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
    class StudentService : IUserService
    {

        public void DeleteUser(string jmbg)
        {
            Student student = Data.Instance.Studenti.ToList().Find(p => p.Korisnik.JMBG.Equals(jmbg));
            if (student == null)
            {

                throw new UserNotFoundException($"Ne postoji student sa tim {jmbg} brojem");
            }
            student.Korisnik.Aktivan = false;
        }

        public bool Provera(RegistrovaniKorisnik reg)
        {
            throw new NotImplementedException();
        }

        public void ReadUsers(string filename)
        {
            Data.Instance.Studenti = new ObservableCollection<Student>();
            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                DataSet ds = new DataSet();

                string selectedStudent = @"select * from Student";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectedStudent, conn);
                dataAdapter.Fill(ds, "Student");

                foreach (DataRow dataRow in ds.Tables["Student"].Rows)
                {

                    RegistrovaniKorisnik registrovaniKorisnik = Data.Instance.Korisnici.ToList().Find(korisnik => korisnik.ID.Equals(dataRow["UserId"].ToString()));
                    if (registrovaniKorisnik.Aktivan)
                    {

                        List<Cas> listaCasova = new List<Cas>();
                        if (Data.Instance.Casovi != null)
                        {
                            string line = dataRow["Casovi"].ToString();
                            string[] casovi = line.Split(',');
                            if (line != "")
                            {
                                foreach (string c in casovi)
                                {
                                    int idCas = int.Parse(c);
                                    Cas cas = Data.Instance.Casovi.ToList().Find(cc => cc.ID.Equals(idCas));
                                    listaCasova.Add(cas);
                                }
                            }
                        }
                        Student student = new Student
                        {
                            Korisnik = registrovaniKorisnik,
                            RezCas = listaCasova

                        };

                        Data.Instance.Studenti.Add(student);

                    }
                }
                conn.Close();
            }
        }




        public void SaveUsers(Object s)
        {
            Student student = (Student)s;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand komanda = conn.CreateCommand();
                komanda.CommandText = @"select max(id) from dbo.Korisnik";
                int UserId = (int)komanda.ExecuteScalar();

                StringBuilder sb = new StringBuilder();
                foreach (Cas c in student.RezCas)
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
                   insert into dbo.Student(UserId,Casovi) 
                   values (@UserId, @Casovi)";

                command.Parameters.Add(new SqlParameter("UserId", UserId));
                command.Parameters.Add(new SqlParameter("Casovi", casovi));

                command.ExecuteNonQuery();


                conn.Close();
            }
        }

        public void UpdateUser(object s)
        {
            Student student = (Student)s;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                int UserId;
                if (Data.Instance.trenutniKorisnik == null)
                {
                    SqlCommand komanda = conn.CreateCommand();
                    komanda.CommandText = @"select max(id) from dbo.Korisnik";
                    UserId = (int)komanda.ExecuteScalar();
                }
                else
                {
                    SqlCommand komanda = conn.CreateCommand();
                    komanda.CommandText = @"select * from dbo.Korisnik where Id=@id";
                    komanda.Parameters.Add(new SqlParameter("id", Data.Instance.trenutniKorisnik.ID));
                    UserId = (int)komanda.ExecuteScalar();
                }
                StringBuilder sb = new StringBuilder();
                foreach (Cas c in student.RezCas)
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
                command.CommandText = @"update dbo.Student set 
                        Casovi = @Casovi
                        where UserId=@id";

                command.Parameters.Add(new SqlParameter("id", UserId));
                command.Parameters.Add(new SqlParameter("Casovi", casovi));

                command.ExecuteNonQuery();


                conn.Close();
            }
        }


    }
}