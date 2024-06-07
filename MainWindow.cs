using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.Xml.Linq;
using System.Printing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using System.Drawing.Printing;
using System.Printing.IndexedProperties;

namespace PrinterAddV2
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            StatusText.Text = "Praseing Arguments";
            
            Handle_Args(args);

        }

        private void MainProgressIndicator_Click(object sender, EventArgs e)
        {

        }


        public string Handle_Args(Array Args) {
            string[] PrintersToAdd = { "Null" };
            string Server = "Null";
            int Tries = 0;
            bool ShouldDel = false;

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
                else if (Convert.ToString(arg).Contains("/del") || Convert.ToString(arg).Contains("/DEL"))
                {
                    ShouldDel = true;
                }

            }

            Del_Printers(ShouldDel, PrintersToAdd, Server, Tries);
            

















            return "done";

        
        }

        public string Add_Printers(string[] Args,string Server, int Tries = 5)
        {
            int ProgressBarVals = 80 / Tries;
            ProgressBarVals = ProgressBarVals / Args.Length;
            String PrinterString = "null";
            Console.WriteLine(Server);
            PrintServer MyServer = new PrintServer(@"\\"+Server);



            foreach (var arg in Args)
            //PrinterString = "\\\\" + Server + "\\" + ;
            PrinterString = Convert.ToString(arg);
            {
                for (int i = 0; i < Tries; i++)
                {
                 StatusText.Text = "Trying to add " + PrinterString + " Attempt " + i;
                    
                    
                    
                    PrintQueueCollection myPrintQueues = MyServer.GetPrintQueues();
                    String printQueueNames = "My Print Queues:\n\n";
                    foreach (PrintQueue pq in myPrintQueues)
                    {
                        printQueueNames += "\t" + pq.Name + "\n";
                    }
                    Console.WriteLine(printQueueNames);
                    Console.WriteLine("\nPress Return to continue.");
                    Console.ReadLine();

                    //TODO Actually make this work 
                    PrintQueue MyQueue = new PrintQueue(MyServer, PrinterString);
                    Console.WriteLine(MyQueue.QueueDriver.Name);
                    string Driver = MyQueue.QueueDriver.Name;
                    String[] port = new String[] { "COM"+i+":" };

                    PrintPropertyDictionary myPrintProperties = MyQueue.PropertiesCollection;
                    PrintStringProperty theLocation = new PrintStringProperty("Location", "FollowMe");
                    myPrintProperties.Remove("Location");
                    myPrintProperties.Add("Location", theLocation);
                    PrintQueue printQueue = MyServer.InstallPrintQueue(Convert.ToString(MyQueue),Driver,port,"WinPrint", myPrintProperties);
                    printQueue.Commit();
                    ProgressBarUpdate(ProgressBarVals);
    
                }

            }




          



            return "done";
        }
        
        
        public string Del_Printers(bool ShouldDel, string[] PrintersToAdd, string Server, int Tries) {
            ProgressBarUpdate(20);
            StatusText.Text = "Should I Delete Printers" + Convert.ToString(ShouldDel);
            if (ShouldDel)
            {
                StatusText.Text = "Deleteing Printers";
                ProgressBarUpdate(10);
                Process scriptProc = new Process();
                scriptProc.StartInfo.FileName = @"cscript";
                scriptProc.StartInfo.Arguments = "//B //Nologo C:\\Windows\\System32\\Printing_Admin_Scripts\\en-US\\prnmngr.vbs " + "-xc";
                scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;  
                scriptProc.Start();
                scriptProc.WaitForExit();
                scriptProc.Close();
                ProgressBarUpdate(10);
                Add_Printers(PrintersToAdd, Server, Tries);
                return "done";
            }
            else {
                StatusText.Text = "Not Deleteing, lets move on";
                ProgressBarUpdate(20);
                Add_Printers(PrintersToAdd, Server, Tries);
                return "done";
            }
        }

        public string ProgressBarUpdate(int Ammount)
        {
            MainProgressIndicator.Value = + Ammount;
            MainProgressIndicator.Refresh();

            return "Added";

        }














    }
}
