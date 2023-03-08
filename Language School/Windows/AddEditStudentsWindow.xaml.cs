using SR62_2021_POP2022.Models;
using SR62_2021_POP2022.Services;
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
    /// Interaction logic for AddEditStudentsWindow.xaml
    /// </summary>
    public partial class AddEditStudentsWindow : Window
    {
        private Student student;
        private RegistrovaniKorisnik korisnik;
        private bool isAddMode;
        

        public AddEditStudentsWindow()
        {
            InitializeComponent();
            cmb_Adresa.ItemsSource = Data.Instance.Adrese;
             korisnik = new RegistrovaniKorisnik
            {
                TipKorisnika = ETipKorisnika.STUDENT,
                Aktivan = true,
               
                
            };
            List<Cas> casovi = new List<Cas>();
            student = new Student
            {
                Korisnik = korisnik,
                RezCas = casovi
                
            };

            isAddMode = true;
            DataContext = student;
        }

        public AddEditStudentsWindow(Student student)
        {
            InitializeComponent();
            cmb_Adresa.ItemsSource = Data.Instance.Adrese;
            this.korisnik = student.Korisnik;
            this.student = student;
            DataContext = this.student;
            isAddMode = false;
            txtJMBG.IsReadOnly = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
                if (isAddMode)
                {
                RegistrovaniKorisnik rk = Data.Instance.Korisnici.ToList().Find(k => k.JMBG.Equals(korisnik.JMBG));
                if (rk == null)
                {
                    Data.Instance.Korisnici.Add(korisnik);
                    Data.Instance.Studenti.Add(student);
                    Data.Instance.SacuvajEntitet("korisnici", korisnik);
                    Data.Instance.SacuvajEntitet("studenti", student);
                }
                }
                else
                {

                Data.Instance.AzurirajEntitet("korisnici", korisnik);
                Data.Instance.AzurirajEntitet("studenti", student);
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

