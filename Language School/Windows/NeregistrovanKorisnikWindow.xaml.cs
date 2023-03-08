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
    /// Interaction logic for NeregistrovanKorisnikWindow.xaml
    /// </summary>
    public partial class NeregistrovanKorisnikWindow : Window
    {
        ICollectionView view;
        public NeregistrovanKorisnikWindow()
        {
            InitializeComponent();
            dgNeprijavljeni.ItemsSource = null;
            view = CollectionViewSource.GetDefaultView(Data.Instance.Skole);
            view.Filter = CustomFilter;
            dgNeprijavljeni.ItemsSource = view;


        }

        private bool CustomFilter(object o)
        {
            Skola sk = (Skola)o;
            
            
                if (txtPretragaPoJezicima.Text != "" || txtPretragaPoGradu.Text!="" || txtPretragaPoNazivu.Text!="")
                {
                    return ((sk.Adresa.ToString().Contains(txtPretragaPoGradu.Text)) && (sk.ListaJezika.Contains(txtPretragaPoJezicima.Text)) && (sk.Naziv.Contains(txtPretragaPoNazivu.Text)));
                }
                
                else { return true; }
                       
        }
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Jezici")
            {
                e.Cancel = true;
            }
            if(headername=="ID")
            {
                e.Cancel = true;
            }
            //update column details when generating
            if (headername == "Naziv")
            {
                e.Column.Header = "Naziv";
            }
            else if (headername == "Adresa")
            {
                e.Column.Header = "Adresa";
            }
            
            else if(headername=="ListaJezika")
            {
                e.Column.Header = "Jezici";
            }
        }

            private void txtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }


        private void RefreshDataGrid()
        {
            dgNeprijavljeni.ItemsSource = Data.Instance.Skole;
        }
        private void btn_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }

        private void miViewProfessors_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = dgNeprijavljeni.SelectedIndex;

            if (selectedIndex >= 0)
            {
                var sveSkole = Data.Instance.Skole;

                var viewProfessorsWindow = new NeregistrovaniKorisniciPregledProfesora(sveSkole[selectedIndex]);

                var successeful = viewProfessorsWindow.ShowDialog();

                if ((bool)successeful)
                {
                    RefreshDataGrid();
                }
            }
        }
    }
}
