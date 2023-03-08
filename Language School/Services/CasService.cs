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
    class CasService : ICasInterface
    {
        public void DeleteCas(string id)
        {
            Cas cas = Data.Instance.Casovi.ToList().Find(c => c.ID.Equals(id));
            if (cas == null)
            {

                throw new CasNotFoundException($"Ne postoji cas sa tim {id} brojem");
            }
            cas.Obrisan = true;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = "update dbo.Cas set Obrisan=1 where Id=@id";

                command.Parameters.Add(new SqlParameter("id", id));
                command.ExecuteNonQuery();

                conn.Close();
            }
        }

        public void ReadCas(string filename)
        {
            Data.Instance.Casovi = new ObservableCollection<Cas>();
            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                DataSet ds = new DataSet();

                string selectedCas = @"select * from Cas";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectedCas, conn);
                dataAdapter.Fill(ds, "Cas");

                foreach (DataRow dataRow in ds.Tables["Cas"].Rows)
                {
                    Boolean.TryParse(dataRow["Obrisan"].ToString(), out Boolean obrisan);
                    if (obrisan == false)
                    {
                        string idCas = dataRow["Id"].ToString();
                        string commandText = $"select * from Profesor where Id={dataRow["ProfesorId"]}";
                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(commandText, conn);

                        DataSet ds1 = new DataSet();

                        dataAdapter1.Fill(ds1, "Profesor");

                        var row = ds1.Tables["Profesor"].Rows[0];
                        Profesor profesor = Data.Instance.Profesori.ToList().Find(prof => prof.Korisnik.ID.Equals(row["UserId"].ToString()));
                        if (profesor != null)
                        {
                            DateTime datumVreme = DateTime.Parse(dataRow["DatumVreme"].ToString());
                            int trajanje = int.Parse(dataRow["Trajanje"].ToString());
                            ECstatus casStatus = (ECstatus)Enum.Parse(typeof(ECstatus), dataRow["Status"].ToString());
                            Student student = null;
                            if (dataRow["StudentId"].ToString() != "")
                            {
                                string commandText1 = $"select * from Student where Id={dataRow["StudentId"]}";
                                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(commandText1, conn);

                                DataSet ds2 = new DataSet();

                                dataAdapter2.Fill(ds2, "Student");

                                var row1 = ds2.Tables["Student"].Rows[0];
                                student = Data.Instance.Studenti.ToList().Find(stud => stud.Korisnik.ID.Equals(row1["UserId"].ToString()));
                            }



                            Cas cas = new Cas
                            {
                                ID = idCas,
                                Profesor = profesor,
                                Datum_i_Vreme = datumVreme,
                                Trajanje = trajanje,
                                Status = casStatus,
                                Obrisan = obrisan,
                                Student = student


                            };
                            profesor.Casovi.Add(cas);

                            if (student != null)
                            {
                                student.RezCas.Add(cas);
                            }
                            Data.Instance.Casovi.Add(cas);
                        }
                    }
                }
            }

        }

        public void SaveCas(Object c)
        {
            Cas cas = (Cas)c;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand komanda = conn.CreateCommand();
                komanda.CommandText = @"select * from dbo.Profesor where UserId=@id";
                komanda.Parameters.Add(new SqlParameter("id", cas.Profesor.Korisnik.ID));
                int ProfesorId = (int)komanda.ExecuteScalar();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"
                   insert into dbo.Cas(ProfesorId,DatumVreme,Trajanje,Status,Obrisan)
                   values (@ProfesorId, @DatumVreme, @Trajanje, @Status,@Obrisan)";

                command.Parameters.Add(new SqlParameter("ProfesorId", ProfesorId));
                command.Parameters.Add(new SqlParameter("DatumVreme", cas.Datum_i_Vreme));
                command.Parameters.Add(new SqlParameter("Trajanje", cas.Trajanje));
                command.Parameters.Add(new SqlParameter("Status", cas.Status.ToString()));
                command.Parameters.Add(new SqlParameter("Obrisan", cas.Obrisan));

                command.ExecuteNonQuery();


                conn.Close();
            }

        }

        public void UpdateCas(Object c)
        {
            Cas cas = (Cas)c;

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();
                SqlCommand komanda = conn.CreateCommand();
                komanda.CommandText = @"select * from dbo.Profesor where UserId=@id";
                komanda.Parameters.Add(new SqlParameter("id", cas.Profesor.Korisnik.ID));
                int ProfesorId = (int)komanda.ExecuteScalar();

                SqlCommand command = conn.CreateCommand();
                command.CommandText =
                         @"update dbo.Cas set
                        ProfesorId = @ProfesorId,
                        DatumVreme = @DatumVreme,
                        Trajanje = @Trajanje,
                        Status = @Status
                        where Id = @id";

                command.Parameters.Add(new SqlParameter("id", cas.ID));
                command.Parameters.Add(new SqlParameter("ProfesorId", ProfesorId));
                command.Parameters.Add(new SqlParameter("DatumVreme", cas.Datum_i_Vreme));
                command.Parameters.Add(new SqlParameter("Trajanje", cas.Trajanje));
                command.Parameters.Add(new SqlParameter("Status", cas.Status.ToString()));


                command.ExecuteNonQuery();
                if(cas.Student!=null)
                {
                    string commandText1 = $"select * from Student where UserId={Data.Instance.trenutniKorisnik.ID}";

                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter(commandText1, conn);

                    DataSet ds2 = new DataSet();

                    dataAdapter2.Fill(ds2, "Student");

                    var row1 = ds2.Tables["Student"].Rows[0];

                    
                    SqlCommand komanda1 = conn.CreateCommand();
                    komanda1.CommandText = @"update dbo.Cas set StudentId=@StudentId where Id=@idd";
                    komanda1.Parameters.Add(new SqlParameter("idd", cas.ID));

                    komanda1.Parameters.Add("StudentId", row1["Id"]);
                    komanda1.ExecuteNonQuery();
                }
            
                conn.Close();
            }
        }
    }
}