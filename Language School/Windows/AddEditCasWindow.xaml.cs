using SR62_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddEditCasWindow.xaml
    /// </summary>
    public partial class AddEditCasWindow : Window
    {
        private Cas cas;
        private bool isAddMode;
        public AddEditCasWindow()
        {
            InitializeComponent();
            if (Data.Instance.trenutniKorisnik.TipKorisnika == ETipKorisnika.PROFESOR)
            {
                Profesor pf = Data.Instance.Profesori.ToList().Find(k => k.Korisnik.JMBG.Equals(Data.Instance.trenutniKorisnik.JMBG));
                ObservableCollection<Profesor> ProfesoriTemp = new ObservableCollection<Profesor>();
                ProfesoriTemp.Add(pf);
                cbProf.ItemsSource = ProfesoriTemp;
                cbProf.SelectedItem = ProfesoriTemp.First();
            }
            else
            {
                cbProf.ItemsSource = Data.Instance.Profesori;
            }
            cas = new Cas
            {
                Status = ECstatus.SLOBODAN,
                Student = null
            };
            isAddMode = true;
            DataContext = cas;
        }

        public AddEditCasWindow(Cas cas)
        {
            InitializeComponent();
            this.cas = cas;
            cbProf.ItemsSource = Data.Instance.Profesori;
            DataContext = this.cas;
            isAddMode = false;
            
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (isAddMode)
            {
                if (cas.Datum_i_Vreme > DateTime.Now)
                {
                    List<Cas> casovinovi = new List<Cas>();
                    Data.Instance.Casovi.Add(cas);
                    Data.Instance.SacuvajEntitet("casovi", cas);
                    cas.Profesor.Casovi = casovinovi;
                    Data.Instance.CitanjeEntiteta("casovi");
                    Data.Instance.AzurirajEntitet("profesori", cas.Profesor);
                }
            }
            else
            {
                Data.Instance.AzurirajEntitet("casovi", cas);
                Data.Instance.CitanjeEntiteta("casovi");
                Data.Instance.AzurirajEntitet("profesori", cas.Profesor);
               
               
            }

            DialogResult = true;
            Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
