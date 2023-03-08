using SR62_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ProfesorPanelWindow.xaml
    /// </summary>
    public partial class ProfesorPanelWindow : Window
    {
       
        ICollectionView pogled;
        public ProfesorPanelWindow()
        {
            InitializeComponent();
            dgCasovi.ItemsSource = null;
            pogled = CollectionViewSource.GetDefaultView(Data.Instance.Casovi);
            pogled.Filter = CustomFilter;
            dgCasovi.ItemsSource = pogled;
        }

        private bool CustomFilter(object o)
        {
            Profesor pf = Data.Instance.Profesori.ToList().Find(k => k.Korisnik.JMBG.Equals(Data.Instance.trenutniKorisnik.JMBG));
            Cas cas = (Cas)o;
            if (cas.Profesor.Korisnik.JMBG == pf.Korisnik.JMBG) {
                if (datum.Text != "") { return cas.Datum_i_Vreme.Date.Equals(datum.SelectedDate.Value.Date); }
                return true; }
            else { return false; }
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

        private void miAddCas_Click(object sender, RoutedEventArgs e)
        {
            var addeditCasWindow = new AddEditCasWindow();

            var successeful = addeditCasWindow.ShowDialog();

            if ((bool)successeful)
            {
                RefreshDataGrid();
            }
        }

        private void miDeleteCas_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgCasovi.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var sviCasovi = Data.Instance.Casovi.ToList().FindAll(c=>c.Profesor.Korisnik.JMBG.Equals(Data.Instance.trenutniKorisnik.JMBG));
                Cas cas = sviCasovi[selectedIndex];
                if (cas.Status == ECstatus.SLOBODAN)
                {
                    Data.Instance.ObrisiCas(cas.ID);
                    Data.Instance.CitanjeEntiteta("casovi");
                    pogled.Filter = CustomFilter;
                    RefreshDataGrid();
                }
            }
        }

        private void RefreshDataGrid()
        {
            pogled = CollectionViewSource.GetDefaultView(Data.Instance.Casovi);
            pogled.Filter = CustomFilter;
            dgCasovi.ItemsSource = pogled;
        }

        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            pogled.Refresh();
        }

        private void datum_KeyUp(object sender, KeyEventArgs e)
        {
            pogled.Refresh();
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
