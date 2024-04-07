using System;
using System.Windows;

namespace WpfApp3
{
    public partial class Formularz : Window
    {
        public Formularz()
        {
            InitializeComponent();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            Formularz formularz = new Formularz();
            formularz.ShowDialog();
        }



        private void Wyslij_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wiadomość została pomyślnie wysłana.", "Potwierdzenie wysłania", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
