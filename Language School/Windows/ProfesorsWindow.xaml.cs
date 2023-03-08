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
    /// Interaction logic for ProfesorsWindow.xaml
    /// </summary>
    public partial class ProfesorsWindow : Window
    {
        public ProfesorsWindow()
        {
            InitializeComponent();
            dgProfesori.ItemsSource = null;
            dgProfesori.ItemsSource = Data.Instance.Profesori.Select(s => new { Profesor = s.Korisnik, Skola = s.SkolaZaposlen, Casovi = dohvatiCasove(s.Casovi), Jezici = dohvatiJezike(s.Jezici) }).ToList();
        }

        public string dohvatiCasove(List<Cas> casovi)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Cas c in casovi)
            {
                if (c != null)
                {
                    sb.Append("Cas ");
                    sb.Append(c.ID);
                    sb.Append(" ) Datum i vreme: ");
                    sb.Append(c.Datum_i_Vreme);
                    sb.Append(" , Trajanje: ");
                    sb.Append(c.Trajanje);
                    sb.Append(" minuta");
                    sb.Append(",Status :");
                    sb.Append(c.Status);
                    if (c.Student != null)
                    {
                        sb.Append(", Student: ");
                        sb.Append(c.Student.Korisnik.Ime);
                        sb.Append(" ");
                        sb.Append(c.Student.Korisnik.Prezime);
                    }
                    sb.Append("\n");
                }
            }

            return sb.ToString();

        }

        public string dohvatiJezike(List<Jezik> jezici)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Jezik j in jezici)
            {
                sb.Append(j.GetJezik());
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private void miAddProfessor_Click(object sender, RoutedEventArgs e)
        {
            var addeditProfesorWindow = new AddEditProfessorsWindow();

            var successeful = addeditProfesorWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miUpdateProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgProfesori.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var sviProfesori = Data.Instance.Profesori;

                var addeditProfesorWindow = new AddEditProfessorsWindow(sviProfesori[selectedIndex]);

                var successeful = addeditProfesorWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }
        private void miDeleteProfessor_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgProfesori.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var sviProfesori = Data.Instance.Profesori;
                Profesor profesor = sviProfesori[selectedIndex];
                Data.Instance.ObrisiKorisnika(profesor.Korisnik.ID);
                Data.Instance.CitanjeEntiteta("profesori");
                Data.Instance.CitanjeEntiteta("casovi");
                RefreshDataGrid();
            }
        }
        private void RefreshDataGrid()
        {
            dgProfesori.ItemsSource = Data.Instance.Profesori.Select(s => new { Profesor = s.Korisnik, Skola = s.SkolaZaposlen, Casovi = dohvatiCasove(s.Casovi), Jezici = dohvatiJezike(s.Jezici) }).ToList();
        }
        private void btn_Back(object sender, RoutedEventArgs e)
        {
            AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
            this.Hide();
            adminPanelWindow.Show();
        }
    }
}