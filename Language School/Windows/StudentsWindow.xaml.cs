using SR62_2021_POP2022.Models;
using SR62_2021_POP2022.Windows;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StudentsWindow.xaml
    /// </summary>
    public partial class StudentsWindow : Window
    {
        public StudentsWindow()
        {
            InitializeComponent();
            dgStudenti.ItemsSource = null;
            dgStudenti.ItemsSource = Data.Instance.Studenti.Select(s => new { Student = s.Korisnik, Casovi = dohvatiCasove(s.RezCas) }).ToList();

        }

        private string dohvatiCasove(List<Cas> casovi)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Cas c in casovi)
            {
                if (c != null)
                {
                    sb.Append("Cas ");
                    sb.Append(c.ID);
                    sb.Append(") Profesor: ");
                    sb.Append(c.Profesor.Korisnik.Ime);
                    sb.Append(" ");
                    sb.Append(c.Profesor.Korisnik.Prezime);
                    sb.Append(" , Datum i vreme: ");
                    sb.Append(c.Datum_i_Vreme);
                    sb.Append(" , Trajanje: ");
                    sb.Append(c.Trajanje);
                    sb.Append(" minuta");
                    sb.Append("\n");
                }
            }

            return sb.ToString();

        }
        private void miAddStudent_Click(object sender, RoutedEventArgs e)
        {
            var addeditStudentWindow = new AddEditStudentsWindow();

            var successeful = addeditStudentWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgStudenti.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var sviStudenti = Data.Instance.Studenti;

                var addeditStudentWindow = new AddEditStudentsWindow(sviStudenti[selectedIndex]);

                var successeful = addeditStudentWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }
        private void miDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgStudenti.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var sviStudenti = Data.Instance.Studenti;
                Student student = sviStudenti[selectedIndex];
                Data.Instance.ObrisiKorisnika(student.Korisnik.ID);
                Data.Instance.CitanjeEntiteta("studenti");
                Data.Instance.CitanjeEntiteta("casovi");
                RefreshDataGrid();
            }
        }
        private void RefreshDataGrid()
        {
            dgStudenti.ItemsSource = Data.Instance.Studenti.Select(s => new { Student = s.Korisnik, Casovi = dohvatiCasove(s.RezCas) }).ToList();
        }

        private void btn_Back(object sender, RoutedEventArgs e)
        {
            AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
            this.Hide();
            adminPanelWindow.Show();
        }
    }
}