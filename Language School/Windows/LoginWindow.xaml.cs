using SR62_2021_POP2022.Models;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private RegistrovaniKorisnik korisnik;
        public LoginWindow()
        {
            InitializeComponent();
            korisnik = new RegistrovaniKorisnik();
            DataContext = korisnik;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Instance.ProveriKorisnika(korisnik))
            {
                if (korisnik.TipKorisnika == ETipKorisnika.STUDENT)
                {
                    StudentPanelWindow studentsPanelWindow = new StudentPanelWindow();
                    this.Hide();
                    studentsPanelWindow.Show();
                }
                else if (korisnik.TipKorisnika == ETipKorisnika.PROFESOR)
                {
                    ProfesorPanelWindow profesorsPanelWindow = new ProfesorPanelWindow();
                    this.Hide();
                    profesorsPanelWindow.Show();
                }
                else
                {
                    AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
                    this.Hide();
                    adminPanelWindow.Show();
                }
            }
        }
    }
}