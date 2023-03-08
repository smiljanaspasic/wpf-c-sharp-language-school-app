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
    /// Interaction logic for PregledProfila.xaml
    /// </summary>
    public partial class PregledProfila : Window
    {
        private RegistrovaniKorisnik registrovani;
        public PregledProfila()
        {
            InitializeComponent();
        }
        public PregledProfila(RegistrovaniKorisnik rk)
        {
            InitializeComponent();
            cmb_Adresa.ItemsSource = Data.Instance.Adrese;
            this.registrovani = rk;
            DataContext = this.registrovani;
            txtJMBG.IsReadOnly = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Data.Instance.AzurirajEntitet("korisnici", registrovani);
           
            DialogResult = true;
            Close();
        }
    }
}
