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
    /// Interaction logic for AdminPanelWindow.xaml
    /// </summary>
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
            Data.Instance.Initialize();
            Data.Instance.CitanjeEntiteta("korisnici");
            Data.Instance.CitanjeEntiteta("profesori");
            Data.Instance.CitanjeEntiteta("studenti");
            Data.Instance.CitanjeEntiteta("casovi");
        }
        private void btnProf_Click(object sender, RoutedEventArgs e)
        {
            ProfesorsWindow mainWindow = new ProfesorsWindow();
            this.Hide();
            mainWindow.Show();
        }
        private void btnStud_Click(object sender, RoutedEventArgs e)
        {
            StudentsWindow mainWindow = new StudentsWindow();
            this.Hide();
            mainWindow.Show();
        }
        private void btnCas_Click(object sender, RoutedEventArgs e)
        {
            CasoviWindow mainWindow = new CasoviWindow();
            this.Hide();
            mainWindow.Show();
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

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            RegistrovaniKorisniciWindow rkw = new RegistrovaniKorisniciWindow();
            this.Hide();
            rkw.Show();
        }
    }
}
