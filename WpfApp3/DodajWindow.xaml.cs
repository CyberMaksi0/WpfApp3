using System;
using System.Windows;
using static WpfApp3.MainWindow;

namespace WpfApp3
{
    public partial class DodajWindow : Window
    {
        public Mieszkanie NoweMieszkanie { get; private set; }

        public DodajWindow()
        {
            InitializeComponent();

            cmbRodzaj.ItemsSource = Enum.GetValues(typeof(RodzajMieszkania));
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            NoweMieszkanie = new Mieszkanie
            {
                IdMieszkania = txtIdMieszkania.Text,
                Osiedle = txtOsiedle.Text,
                Adres = txtAdres.Text,
                ZGarażem = chkZGarazem.IsChecked ?? false,
                Rodzaj = (RodzajMieszkania)cmbRodzaj.SelectedItem,
                Metraż = int.TryParse(txtMetraz.Text, out int metraz) ? metraz : 0,
                Dostępność = chkDostepnosc.IsChecked ?? false,
                Opis = txtOpis.Text
            };

            DialogResult = true;
            Close();
        }
    }
}
