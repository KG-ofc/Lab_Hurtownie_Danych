using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class FormBGW02 : Form
    {
        public static FormBGW02 instance;
        public string folderPath { get; set; }
        public int nmbrOfFiles { get; set; }
        public FormBGW02()
        {
            InitializeComponent();

            instance = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy) 
            {
                backgroundWorker1.RunWorkerAsync();
                progressBar1.Value = 0;
                progressBar1.Maximum = nmbrOfFiles;   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) 
            {
                backgroundWorker1.CancelAsync();
            }
        }

        

        ////    Deklaracja list
        List<string> type0 = new List<string>();
        List<string> date = new List<string>();
        List<string> time = new List<string>();
        List<string> source = new List<string>();
        List<string> destination = new List<string>();
        List<string> transport = new List<string>();
        //// 
       
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)         // WORKER DO WORK
        {
            int sum = 0;

            for (int i = 0; i < nmbrOfFiles; i++)        // Wykonujemy tyle razy ile jest plików w folderze
            {
                sum = +1;
                backgroundWorker1.ReportProgress(i+1);

                ///////////////////////////////////////////////////////////////////////////////     Otworzenie i zczytanie plików
                int totalNmbrOfLine = 0;

                var list = Directory.GetFiles(folderPath, "*.txt");

                try
                {
                    using (StreamReader sr = new StreamReader(list[i]))
                    {
                        string line;
                        int a = 0;

                        while ((line = sr.ReadLine()) != null)
                        {
                            a++;
                            int count = line.Split(',').Length - 1;

                            if (line.ToLower().Contains(',') && count == 5)
                            {
                                string s = line;
                                string[] subs = s.Split(',');

                                if (subs[0] != "type")
                                {
                                    type0.Add(subs[0]);    // typ
                                    date.Add(subs[1]);    // data
                                    time.Add(subs[2]);    // czas
                                    source.Add(subs[3]);    // adresWe
                                    destination.Add(subs[4]);    // adresWy
                                    transport.Add(subs[5]);    // protokol
                                    totalNmbrOfLine++;
                                }

                            }

                        }

                        string nmbrOfLines2 = totalNmbrOfLine.ToString();
                        //textBox3.Text = nmbrOfLines2;
                    }
                }
                catch (Exception er)
                {
                    // Let the user know what went wrong.
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(er.Message);
                }


                ///////////////////////////////////////////////////////////////////////////////

                if (backgroundWorker1.CancellationPending)      // Sprawdzenie czy proces został przerwany
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;
                }

            }//FOR
            
            e.Result = sum;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) 
            {
                label1.Text = "Anulowano";
            }
            else if(e.Error != null) 
            {
                label1.Text = e.Error.Message;
            }
            else 
            {
                //label1.Text = "Sum = " + e.Result.ToString();
                label1.Text = "Zakończono";


                for(int i =0; i < type0.Count ; i++) 
                {
                    Form1.form1Instance.listBox6.Items.Add(type0[i]);
                    Form1.form1Instance.listBox5.Items.Add(date[i]);
                    Form1.form1Instance.listBox4.Items.Add(time[i]);
                    Form1.form1Instance.listBox3.Items.Add(source[i]);
                    Form1.form1Instance.listBox2.Items.Add(destination[i]);
                    Form1.form1Instance.listBox1.Items.Add(transport[i]);


                }

                

                //Form1.form1Instance.LB1.Items.Add(xd);      ////DZIAŁA TUTAj
            }
        }

    }
}
