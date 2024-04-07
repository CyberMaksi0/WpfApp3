using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        public RodzajMieszkania[] RodzajMieszkaniaValues => (RodzajMieszkania[])Enum.GetValues(typeof(RodzajMieszkania));

        private ObservableCollection<Mieszkanie> mieszkania = new ObservableCollection<Mieszkanie>();

        public MainWindow()
        {
            InitializeComponent();
            listView.ItemsSource = mieszkania;
        }

        public enum RodzajMieszkania
        {
            M1,
            M2,
            M3
        }

        public class Mieszkanie : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public string IdMieszkania { get; set; }
            public string Osiedle { get; set; }
            public string Adres { get; set; }
            public bool ZGarażem { get; set; }
            public RodzajMieszkania Rodzaj { get; set; }
            public int Metraż { get; set; }
            public bool Dostępność { get; set; }
            public string Opis { get; set; }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                var lines = new List<string>();
                foreach (var item in mieszkania)
                {
                    lines.Add($"{item.IdMieszkania},{item.Osiedle},{item.Adres},{item.ZGarażem},{item.Rodzaj},{item.Metraż},{item.Dostępność},{item.Opis}");
                }
                File.WriteAllLines(saveFileDialog.FileName, lines);
            }
        }

        private void Pobierz_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mieszkania.Clear();
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    if (data.Length == 8)
                    {
                        Mieszkanie mieszkanie = new Mieszkanie
                        {
                            IdMieszkania = data[0],
                            Osiedle = data[1],
                            Adres = data[2],
                            ZGarażem = bool.TryParse(data[3], out bool zGarażem) ? zGarażem : false,
                            Rodzaj = Enum.TryParse<RodzajMieszkania>(data[4], out RodzajMieszkania rodzaj) ? rodzaj : RodzajMieszkania.M1,
                            Metraż = int.TryParse(data[5], out int metraż) ? metraż : 0,
                            Dostępność = bool.TryParse(data[6], out bool dostępność) ? dostępność : false,
                            Opis = data[7]
                        };

                        mieszkania.Add(mieszkanie);
                    }
                }
            }
        }

        private void Zamknij_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodajWindow dodajWindow = new DodajWindow();
            dodajWindow.ShowDialog();

            if (dodajWindow.DialogResult == true)
            {
                mieszkania.Add(dodajWindow.NoweMieszkanie);
            }
        }

        private void Zmień_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                DodajWindow dodajWindow = new DodajWindow();
                dodajWindow.DataContext = listView.SelectedItem;
                dodajWindow.ShowDialog();

                if (dodajWindow.DialogResult == true)
                {
                    int index = mieszkania.IndexOf((Mieszkanie)listView.SelectedItem);
                    mieszkania[index] = dodajWindow.NoweMieszkanie;
                }
            }
        }

        private void Usuń_Click(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                mieszkania.Remove((Mieszkanie)listView.SelectedItem);
            }
        }
        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            Formularz formularz = new Formularz();
            formularz.ShowDialog();
        }

    }
}
