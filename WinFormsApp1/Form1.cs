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

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public static Form1 form1Instance;
        public ListBox LB1;

        public int transferedNbrOfLines { get; set; }


        public Form1()
        {
            InitializeComponent();

            form1Instance = this;

            LB1 = listBox1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if(ofd.ShowDialog() == DialogResult.OK) 
            {
                this.textBox1.Text = ofd.FileName;  //test tu jest

                int czytosieklika = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); // Cleaning listBoxes before initialization 
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            listBox6.Items.Clear();
            listBox7.Items.Clear();

            string path01 = textBox1.Text;



            listBox7.Items.Add(path01);



            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path01))
                {
                    string line;
                    int i =0;
                    int k = 0;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        listBox7.Items.Add(line); // top textbox
                        i++;
                        int count = line.Split(',').Length - 1;

                        if (line.ToLower().Contains(',') && count==5)
                        {
                            string s = line;
                            string[] subs = s.Split(',');

                            if( subs[0] != "type") 
                            {
                                listBox6.Items.Add(subs[0]);    // typ
                                listBox5.Items.Add(subs[1]);    // data
                                listBox4.Items.Add(subs[2]);    // czas
                                listBox3.Items.Add(subs[3]);    // adresWe
                                listBox2.Items.Add(subs[4]);    // adresWy
                                listBox1.Items.Add(subs[5]);    // protokol
                                k++;
                            }

                        }

                    }
                    string nmbrOfLines = i.ToString();
                    textBox2.Text = nmbrOfLines;
                    string nmbrOfLines2 = k.ToString();
                    textBox3.Text = nmbrOfLines2;
                }
            }
            catch (Exception er)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(er.Message);
            }




            /*Void OdczytajPlik(string nazwa)
            {
                //otwarcie pliku
                //while (nie koniec pliku)
                {
                    //czytanie linii z pliku
                    //wyświetlenie każdej linii w listboxie
                    //przetwarzanie każdej linii PrzetwarzanieLinii(Linia);
                }
                //zamkniecie pliku
            }*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {
            var newform = new Form2();
            
            newform.ShowDialog();   // Open as dialog window

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)  
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = fbd.SelectedPath;
            }

            var list = Directory.GetFiles(textBox1.Text, "*.txt");   
            if (list.Length > 0)
            {
                int g = list.Length;
                string text01 = g.ToString();
                textBox4.Text = text01;
            }


            ///////////////////////////////////////////////////////////////////////////////

            
            int totalNmbrOfLine = 0;

            for (int i = 0; i < list.Length; i++) 
            {
                listBox8.Items.Add(list[i]);
                ///////////////////////////////////


                try
                {
                    // Create an instance of StreamReader to read from a file.
                    // The using statement also closes the StreamReader.
                    using (StreamReader sr = new StreamReader(list[i]))
                    {
                        string line;
                        int a = 0;
                        
                        // Read and display lines from the file until the end of
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            listBox7.Items.Add(line); // top textbox
                            a++;
                            int count = line.Split(',').Length - 1;

                            if (line.ToLower().Contains(',') && count == 5)
                            {
                                string s = line;
                                string[] subs = s.Split(',');

                                if (subs[0] != "type")
                                {
                                    listBox6.Items.Add(subs[0]);    // typ
                                    listBox5.Items.Add(subs[1]);    // data
                                    listBox4.Items.Add(subs[2]);    // czas
                                    listBox3.Items.Add(subs[3]);    // adresWe
                                    listBox2.Items.Add(subs[4]);    // adresWy
                                    listBox1.Items.Add(subs[5]);    // protokol
                                    totalNmbrOfLine++;
                                }

                            }

                        }
                        
                        string nmbrOfLines2 = totalNmbrOfLine.ToString();
                        textBox3.Text = nmbrOfLines2;
                    }
                }
                catch (Exception er)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(er.Message);
                }

            





            }






        }

        private void button5_Click(object sender, EventArgs e)      /// BGW BUTTON
        {

            /*
            
            var newform = new FormBGW();
            newform.folderPath = textBox1.Text;
            newform.nmbrOfFiles = g;


            newform.Show();   // Open as dialog window
            */


            //BGW02
            var newform = new FormBGW02();
            newform.folderPath = textBox1.Text;
            newform.nmbrOfFiles = g;

            newform.Show();

        }

        public int g = 0;

        private void button6_Click(object sender, EventArgs e)      /// FOLDER SELECT BUTTON
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = fbd.SelectedPath;
            }

            //int g = 0;
            var list = Directory.GetFiles(textBox1.Text, "*.txt");
            if (list.Length > 0)
            {
                g = list.Length;
                string text01 = g.ToString();
                textBox4.Text = text01;
            }


            //var newform = new FormBGW();

            
            


        }
    }
}
