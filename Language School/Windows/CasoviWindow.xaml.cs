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
    /// Interaction logic for CasoviWindow.xaml
    /// </summary>
    public partial class CasoviWindow : Window
    {
        public CasoviWindow()
        {
            InitializeComponent();
            dgCasovi.ItemsSource = null;
            dgCasovi.ItemsSource = Data.Instance.Casovi;
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

        private void miUpdateCas_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgCasovi.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var sviCasovi = Data.Instance.Casovi;

                var addeditCasWindow = new AddEditCasWindow(sviCasovi[selectedIndex]);

                var successeful = addeditCasWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }
        private void miDeleteCas_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgCasovi.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var sviCasovi = Data.Instance.Casovi;
                Cas cas = sviCasovi[selectedIndex];
                Data.Instance.ObrisiCas(cas.ID);
                Data.Instance.CitanjeEntiteta("casovi");
                RefreshDataGrid();
            }
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
        private void RefreshDataGrid()
        {
            dgCasovi.ItemsSource = Data.Instance.Casovi;
        }


        private void btn_Back(object sender, RoutedEventArgs e)
        {
            AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
            this.Hide();
            adminPanelWindow.Show();
        }
    }
}