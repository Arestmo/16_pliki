using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _16_pliki
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "Text File|*.txt";
            saveFileDialog1.Title = "Save a txt file";

            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "Text File | *.txt";
            openFileDialog1.Title = "Open File";

            Open_IMG.InitialDirectory = @"c:\";
            Open_IMG.Filter = "Img file | *.jpg | Gif file | *.gif | PNG File | *.png | All Files | *.*";
            Open_IMG.Title = "Open IMG";



        }

        private void Button_Save_Click(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile()); //Uruchomienie StreamWriter, otworzenie pliku

                FileNameLabel.Text = Path.GetFileName(saveFileDialog1.FileName); //Wyświetlenie nazwy pliku
                FileSizeLabel.Text = "--"; //Ustawienie wartości na sztywno

                ActualPath.Text = Path.GetFullPath(saveFileDialog1.FileName); //Wyświetlenie ścieżki

                if (NotepadText.Text.Length != 0) //Sprawdzenie czy nie robimy pustego pliku
                {
                    writer.Write(NotepadText.Text); //Zapis całości do otwartego pliku
                }

                writer.Dispose(); //Wstrzymanie buforu dopóki praca nie zostanie zakończona
                writer.Close();   //Zamknięcie pliku, zwolnienie dostępu
            }

        }

        private void Button_Open_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog1.OpenFile()); //Uruchomienie StreamReadera, otworzenie pliku
                FileNameLabel.Text = Path.GetFileName(openFileDialog1.FileName); //Wyświetlenie nazwy pliku
                FileSizeLabel.Text = new FileInfo(openFileDialog1.FileName).Length.ToString(); //Wyświetlenie rozmiaru pliku

                ActualPath.Text = Path.GetFullPath(openFileDialog1.FileName); //Wyświetlenie ścieżki

                NotepadText.Text = reader.ReadToEnd(); //Wczytanie wartości z pliku do kontrolki 

                FileSizeLabel.Text = NotepadText.Text.Length.ToString();
                reader.Dispose(); //Wstrzymanie buforu dopóki praca nie zostanie zakończona
                reader.Close();   //Zamknięcie pliku, zwolnienie dostępu
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Open_IMG.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = Open_IMG.FileName;
            }
        }

        private void SavePearson_Button_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, true))
                {
                    string value_to_save = NameTextBox.Text + "|" + SurnameTextBox.Text + "|" + AgeTextBox.Text;

                    sw.WriteLine(value_to_save);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Person> list = new List<Person>();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.OpenFile()))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        int counter = 0;
                        string name = "";
                        string surname = "";
                        string age = "";
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] == '|')
                            {
                                counter++;
                            }

                            switch (counter)
                            {
                                case 0:
                                    {
                                        if (line[i] != '|')
                                            name += line[i];
                                        break;
                                    }
                                case 1:
                                    {
                                        if (line[i] != '|')
                                            surname += line[i];
                                        break;
                                    }
                                case 2:
                                    {
                                        if (line[i] != '|')
                                            age += line[i];
                                        break;
                                    }
                            }

                        }
                        list.Add(new Person(name, surname, int.Parse(age)));
                    }
                }
                foreach(var item in list)
                {
                    listBox1.Items.Add(item.Name + " "+ item.Surname + " "+ item.Age.ToString());
                }
            }
        }
    }
}


// Klasa Osoba : Imie, Nazwisko, Wiek -> plik (jedna linijka to jedena osoba) Struktura linijki -> Imie|Nazwisko|Wiek 
