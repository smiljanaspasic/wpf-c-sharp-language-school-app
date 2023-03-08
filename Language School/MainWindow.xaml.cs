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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SR62_2021_POP2022
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Data.Instance.Initialize();
            Data.Instance.CitanjeEntiteta("korisnici");
            Data.Instance.CitanjeEntiteta("profesori");
            Data.Instance.CitanjeEntiteta("studenti");
            Data.Instance.CitanjeEntiteta("casovi");

        }
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            var addeditStudentWindow = new AddEditStudentsWindow();

            addeditStudentWindow.ShowDialog();

        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Hide();
            loginWindow.Show();
        }

        private void btnNeregistrovani_Click(object sender, RoutedEventArgs e)
        {
            NeregistrovanKorisnikWindow mainWindow = new NeregistrovanKorisnikWindow();
            this.Hide();
            mainWindow.Show();
        }
    }
}