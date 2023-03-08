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
    /// Interaction logic for AddEditProfessorsWindow.xaml
    /// </summary>
    public partial class AddEditProfessorsWindow : Window
    {
        private Profesor profesor;
        private RegistrovaniKorisnik korisnik;
        private bool isAddMode;
        public AddEditProfessorsWindow()
        {
            InitializeComponent();
            cmb_Adresa.ItemsSource = Data.Instance.Adrese;
            cmb_Skola.ItemsSource = Data.Instance.Skole;
            Jezik j = new Jezik();
            Jezik j1 = new Jezik();
            Jezik j2 = new Jezik();
            Jezik j3 = new Jezik();
            Jezik j4 = new Jezik();

            j.SetJezik("nemacki");
            j1.SetJezik("engleski");
            j2.SetJezik("spanski");
            j3.SetJezik("japanski");
            j4.SetJezik("kineski");

            List<Jezik> jezici = new List<Jezik>();
            jezici.Add(j);
            jezici.Add(j1);
            jezici.Add(j2);
            lstbox.ItemsSource = jezici;
            korisnik = new RegistrovaniKorisnik
            {
                TipKorisnika = ETipKorisnika.PROFESOR,
                Aktivan = true,


            };
            List<Cas> casovi = new List<Cas>();
            List<Jezik> listaJezika = new List<Jezik>();
            profesor = new Profesor
            {
                Korisnik = korisnik,
                Casovi = casovi,
                Jezici = listaJezika

            };

            isAddMode = true;
            DataContext = profesor;
        }

        public AddEditProfessorsWindow(Profesor profesor)
        {
            InitializeComponent();
            cmb_Adresa.ItemsSource = Data.Instance.Adrese;
            cmb_Skola.ItemsSource = Data.Instance.Skole;
            Jezik j = new Jezik();
            Jezik j1 = new Jezik();
            Jezik j2 = new Jezik();
            Jezik j3 = new Jezik();
            Jezik j4 = new Jezik();

            j.SetJezik("nemacki");
            j1.SetJezik("engleski");
            j2.SetJezik("spanski");
            j3.SetJezik("japanski");
            j4.SetJezik("kineski");

            List<Jezik> jezici = new List<Jezik>();
            jezici.Add(j);
            jezici.Add(j1);
            jezici.Add(j2);
            jezici.Add(j3);
            jezici.Add(j4);
            lstbox.ItemsSource = jezici;
            this.korisnik = profesor.Korisnik;
            this.profesor = profesor;
            DataContext = this.profesor;
            isAddMode = false;
            txtJMBG.IsReadOnly = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (isAddMode)
            {
                Data.Instance.Korisnici.Add(korisnik);
                Data.Instance.Profesori.Add(profesor);
                Data.Instance.SacuvajEntitet("korisnici",korisnik);
                Data.Instance.SacuvajEntitet("profesori",profesor);
            }
            else
            {
                Data.Instance.AzurirajEntitet("korisnici", korisnik);
                Data.Instance.AzurirajEntitet("profesori", profesor);
               
            }

            DialogResult = true;
            Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


        private void lstbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Jezik jezik = new Jezik();
            jezik.SetJezik(lstbox.SelectedItem.ToString());
            if (!profesor.Jezici.Contains(jezik))
            {
                profesor.Jezici.Add(jezik);
            }
        }
    }
}

