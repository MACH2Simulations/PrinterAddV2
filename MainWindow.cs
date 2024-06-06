using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterAddV2
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            //StatusText.Text = args[3];
            Handle_Args(args);

        }

        private void MainProgressIndicator_Click(object sender, EventArgs e)
        {

        }


        public string Handle_Args(Array Args) {
            string[] PrintersToAdd = { "Null" };
            string Server = "Null";
            int Tries = 0;

            foreach (var arg in Args)
            {
                if (Convert.ToString(arg) == "/qn" || Convert.ToString(arg) == "/QN")
                {
                    this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                }
                else if (Convert.ToString(arg).Contains("/s") || Convert.ToString(arg).Contains("/S"))
                {
                    Server = Convert.ToString(arg).Remove(0, 3);
                    StatusText.Text = Server;
                }
                else if (Convert.ToString(arg).Contains("/t") || Convert.ToString(arg).Contains("/T"))
                {
                    Tries = int.Parse((Convert.ToString(arg).Remove(0, 3)));
                    Console.WriteLine(Tries);
                }
                else if (Convert.ToString(arg).Contains("/p") || Convert.ToString(arg).Contains("/P"))
                {
                    string Printers = Convert.ToString(arg).Remove(0, 3);
                    PrintersToAdd = Printers.Split(' ');

                }

            }

            Add_Printers(PrintersToAdd,Server,Tries);

















            return "done";

        
        }

        public string Add_Printers(string[] Args,string Server, int Tries = 5)
        {
            StatusText.Text = Server + " " + Args[1];


            return "done";
        }
















    }
}
