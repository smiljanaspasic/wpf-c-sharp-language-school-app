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
    /// Interaction logic for NeregistrovaniKorisniciPregledProfesora.xaml
    /// </summary>
    public partial class NeregistrovaniKorisniciPregledProfesora : Window
    {
        ICollectionView pogled;
        private Skola skola;
        public NeregistrovaniKorisniciPregledProfesora(Skola s)
        {
            InitializeComponent();
            dgProfesori.ItemsSource = null;
            pogled = CollectionViewSource.GetDefaultView(Data.Instance.Profesori);
            this.skola = s;
            pogled.Filter = CustomFilter;
            dgProfesori.ItemsSource = pogled;
        }

        private bool CustomFilter(object o)
        {
            Profesor pf = (Profesor)o;

            if (pf.SkolaZaposlen == skola)
            {
                if (txtPretragaPoAdresi.Text != "" || txtPretragaPoEmailu.Text != "" || txtPretragaPoImenu.Text != "" || txtPretragaPoPrezimenu.Text != "" || txtPretragaPoJezicima.Text != "")
                {
                    return (pf.Korisnik.Adresa.ToString().Contains(txtPretragaPoAdresi.Text) && pf.Korisnik.Email.Contains(txtPretragaPoEmailu.Text) &&
                        pf.Korisnik.Ime.Contains(txtPretragaPoImenu.Text) && pf.Korisnik.Prezime.Contains(txtPretragaPoPrezimenu.Text)
                        && pf.ListaJezika.Contains(txtPretragaPoJezicima.Text));
                }
                else
                {
                    return true;
                }
            }
            else { return false; }
        }

        private void txtPretraga_KeyUp(object sender, KeyEventArgs e)
        {
            pogled.Refresh();
        }

        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            if (headername == "Jezici")
            {
                e.Cancel = true;
            }
            if (headername == "Korisnik")
            {
                e.Cancel = true;
            }
            if (headername == "Casovi")
            {
                e.Cancel = true;
            }
            //update column details when generating
            if (headername == "ProfesorZaPretragu")
            {
                e.Column.Header = "Podaci profesora";
            }
            else if (headername == "SkolaZaposlen")
            {
                e.Column.Header = "Skola u kojoj profesor radi";
            }

            else if (headername == "ListaJezika")
            {
                e.Column.Header = "Jezici koje predaje";
            }
        }
       
        private void btn_Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
