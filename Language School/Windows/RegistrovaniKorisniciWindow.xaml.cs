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
    /// Interaction logic for RegistrovaniKorisniciWindow.xaml
    /// </summary>
    public partial class RegistrovaniKorisniciWindow : Window
    {
        ICollectionView view;
        public RegistrovaniKorisniciWindow()
        {
            InitializeComponent();
            view = CollectionViewSource.GetDefaultView(Data.Instance.Korisnici);
            view.Filter = CustomFilter;
            dgKorisnici.ItemsSource = view;
        }

        private bool CustomFilter(object o)
        {
            RegistrovaniKorisnik rk = (RegistrovaniKorisnik)o;

            if (txtPretragaPoAdresi.Text != "" || txtPretragaPoEmailu.Text!="" || txtPretragaPoImenu.Text!="" || txtPretragaPoPrezimenu.Text!="" || txtPretragaPoTipu.Text!="")
            {
                return (rk.Adresa.ToString().Contains(txtPretragaPoAdresi.Text) && rk.Email.Contains(txtPretragaPoEmailu.Text) && 
                    rk.Ime.Contains(txtPretragaPoImenu.Text) && rk.Prezime.Contains(txtPretragaPoPrezimenu.Text) && rk.TipKorisnika.ToString().Contains(txtPretragaPoTipu.Text));
            }


            else { return true; }



        }
        private void btn_Back(object sender, RoutedEventArgs e)
        {
            AdminPanelWindow adminPanelWindow = new AdminPanelWindow();
            this.Hide();
            adminPanelWindow.Show();
        }
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Aktivan")
            {
                e.Cancel = true;
            }
            if (headername == "ID")
            {
                e.Cancel = true;
            }
            //update column details when generating
            if (headername == "Ime")
            {
                e.Column.Header = "Ime";
            }
            else if (headername == "Prezime")
            {
                e.Column.Header = "Prezime";
            }

            else if (headername == "JMBG")
            {
                e.Column.Header = "JMBG";
            }
            
            else if (headername == "Pol")
            {
                e.Column.Header = "Pol";
            }

            else if (headername == "Email")
            {
                e.Column.Header = "Email";
            }

            else if (headername == "Lozinka")
            {
                e.Column.Header = "Lozinka";
            }

            else if (headername == "Adresa")
            {
                e.Column.Header = "Adresa";
            }
            else if (headername == "TipKorisnika")
            {
                e.Column.Header = "TipKorisnika";
            }
        }
        private void txtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
    }
}
