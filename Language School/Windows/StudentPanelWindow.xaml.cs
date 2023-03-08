using SR62_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SR62_2021_POP2022.Windows
{
    /// <summary>
    /// Interaction logic for StudentPanelWindow.xaml
    /// </summary>
    public partial class StudentPanelWindow : Window
    {
        ICollectionView pogled;
        private Profesor profesor;
        public StudentPanelWindow()
        {
            InitializeComponent();
            dgCasovi.ItemsSource = null;
            cbProf.ItemsSource = Data.Instance.Profesori;
          //   pogled = CollectionViewSource.GetDefaultView(Data.Instance.Casovi);
          //  pogled.Filter = CustomFilter;
          // dgCasovi.ItemsSource = pogled;
        }

        private bool CustomFilter(object o)
        {
            Cas cas = (Cas)o;
            if (cbProf.Text != "" && cas.Status==ECstatus.SLOBODAN)
            {
                profesor = (Profesor)cbProf.SelectedItem;
                
                    return cas.Profesor.Korisnik.JMBG.Equals(profesor.Korisnik.JMBG);
                
            }
             return false; 
        }
    
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Obrisan")
            {
                e.Cancel = true;
            }

            //update column details when generating
            if (headername == "ID")
            {
                e.Column.Header = "Sifra";
            }
            else if (headername == "Profesor")
            {
                e.Column.Header = "Profesor";
            }
            else if (headername == "Datum_i_Vreme")
            {
                e.Column.Header = "Datum i Vrene";
            }

            else if (headername == "Trajanje")
            {
                e.Column.Header = "Trajanje";
            }
            else if (headername == "Status")
            {
                e.Column.Header = "Status";
            }
            else if (headername == "Student")
            {
                e.Column.Header = "Student";
            }
        }

        private void slobodni_Click(object sender, RoutedEventArgs e)
        {
            pogled = CollectionViewSource.GetDefaultView(Data.Instance.Casovi);
            pogled.Filter = CustomFilter;
            dgCasovi.ItemsSource = pogled;
        }

        private void zauzeti_Click(object sender, RoutedEventArgs e)
        {
            pogled = CollectionViewSource.GetDefaultView(Data.Instance.Casovi);
            pogled.Filter = CustomFilter1;
            dgCasovi.ItemsSource = pogled;
        }

        private bool CustomFilter1(object obj)
        {
            Cas cas = (Cas)obj;
            if (cbProf.Text != "")
            {
                profesor = (Profesor)cbProf.SelectedItem;
                if (cas.Student != null)
                {
                    return (cas.Profesor.Korisnik.JMBG.Equals(profesor.Korisnik.JMBG) && cas.Student.Korisnik.JMBG.Equals(Data.Instance.trenutniKorisnik.JMBG));
                }
            }
            return false;
        }

        private void miRez_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgCasovi.SelectedIndex;
            profesor = (Profesor)cbProf.SelectedItem;
            if (selectedIndex >= 0)
            {
                var sviCasovi = Data.Instance.Casovi.ToList().FindAll(c => (c.Profesor.Korisnik.JMBG.Equals(profesor.Korisnik.JMBG) && (c.Status.Equals(ECstatus.SLOBODAN))));
                Cas cas = sviCasovi[selectedIndex];
               
                    Student student = Data.Instance.Studenti.ToList().Find(k => k.Korisnik.JMBG.Equals(Data.Instance.trenutniKorisnik.JMBG));
                    cas.Status = ECstatus.REZERVISAN;
                    student.RezCas.Add(cas);
                    cas.Student = student;
                    Data.Instance.AzurirajEntitet("casovi", cas);
                    Data.Instance.AzurirajEntitet("studenti", student);
                    Data.Instance.CitanjeEntiteta("studenti");
                    Data.Instance.CitanjeEntiteta("casovi");
                    pogled = CollectionViewSource.GetDefaultView(Data.Instance.Casovi);
                    pogled.Filter = CustomFilter;
                    dgCasovi.ItemsSource = pogled;
                
               
            }
        }

        private void miOsl_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgCasovi.SelectedIndex;
            profesor = (Profesor)cbProf.SelectedItem;
            if (selectedIndex >= 0)
            {
                var sviCasovi = Data.Instance.Casovi.ToList().FindAll(c => (c.Profesor.Korisnik.JMBG.Equals(profesor.Korisnik.JMBG) && c.Status.Equals(ECstatus.REZERVISAN) && (c.Student.Korisnik.JMBG.Equals(Data.Instance.trenutniKorisnik.JMBG))));
                Cas cas = sviCasovi[selectedIndex];
                Student student = Data.Instance.Studenti.ToList().Find(k => k.Korisnik.JMBG.Equals(Data.Instance.trenutniKorisnik.JMBG));
                cas.Status = ECstatus.SLOBODAN;
                student.RezCas.Remove(cas);
                cas.Student = null;               
                using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = @"update dbo.Cas set StudentId=null,Status=@Status where Id=@id";
                    command.Parameters.Add(new SqlParameter("id", cas.ID));
                    command.Parameters.Add(new SqlParameter("Status", cas.Status));
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                Data.Instance.AzurirajEntitet("studenti", student);
                Data.Instance.CitanjeEntiteta("studenti");
                Data.Instance.CitanjeEntiteta("casovi");
                pogled = CollectionViewSource.GetDefaultView(Data.Instance.Casovi);
                pogled.Filter = CustomFilter1;
                dgCasovi.ItemsSource = pogled;
            }
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Data.Instance.trenutniKorisnik = null;
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();

        }
        private void btnProfil_Click(object sender, RoutedEventArgs e)
        {
            var pregledProfilaWindow = new PregledProfila(Data.Instance.trenutniKorisnik);

            pregledProfilaWindow.ShowDialog();
        }
    }
}
