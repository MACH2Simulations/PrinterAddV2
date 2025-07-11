﻿using System;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace PrinterAddV2
{
    public partial class MainWindow : Form
    {
        int sleep = 500;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ShowInTaskbar = false;

        }

        private void MainProgressIndicator_Click(object sender, EventArgs e)
        {

        }
        public string Handle_Args(Array Args)
        {
            string[] PrintersToAdd = { "Null" };
            string Server = "Null";
            int Tries = 5;
            bool ShouldDel = false;
            bool Quite = false;
            bool DelOnly = false;

            foreach (var arg in Args)
            {
                if (Convert.ToString(arg) == "/delonly" || Convert.ToString(arg) == "/DELONLY" || Convert.ToString(arg) == "/DelOnly")
                {
                    DelOnly = true;
                    Del_Printers(true, PrintersToAdd, "Null", 5, DelOnly);
                }
                else if (Convert.ToString(arg) == "/qn" || Convert.ToString(arg) == "/QN" || Convert.ToString(arg) == "/Q" || Convert.ToString(arg) == "/q")
                {
                    Quite = true;
                }
                else if (Convert.ToString(arg).Contains("/s") || Convert.ToString(arg).Contains("/S"))
                {
                    Server = Convert.ToString(arg).Remove(0, 3);
                }
                else if (Convert.ToString(arg).Contains("/t") || Convert.ToString(arg).Contains("/T"))
                {
                    Tries = int.Parse((Convert.ToString(arg).Remove(0, 3)));
                    //Console.WriteLine(Tries);
                }
                else if (Convert.ToString(arg).Contains("/p") || Convert.ToString(arg).Contains("/P"))
                {
                    string Printers = Convert.ToString(arg).Remove(0, 3);
                    //Console.WriteLine("Printers Passed in args " + Printers);
                    PrintersToAdd = Printers.Split(' ');
                    //Console.WriteLine(PrintersToAdd[1]);

                }
                else if (Convert.ToString(arg).Contains("/del") || Convert.ToString(arg).Contains("/DEL"))
                {
                    ShouldDel = true;
                }
                else if (Convert.ToString(arg).Contains("/dark") || Convert.ToString(arg).Contains("/DARK") || Convert.ToString(arg).Contains("/DM") || Convert.ToString(arg).Contains("/dm"))
                {
                    //Temp for testing
                    this.BackColor = Color.FromArgb(30, 30, 30);
                    MainProgressIndicator.BackColor = Color.White;
                    ProgressBarUpdate(20);



                    this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    this.ShowInTaskbar = true;

                    StatusText.Text = "Dark Mode";

                    StatusText.BackColor = Color.FromArgb(30, 30, 30);
                    StatusText.ForeColor = Color.FromArgb(255, 255, 255);



                    StatusText.Refresh();
                    StatusText.Text = "Dark Mode";
                    StatusText.Text = "Dark Mode";

                }
            }


            if (!Quite)
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
            else
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }


            Del_Printers(ShouldDel, PrintersToAdd, Server, Tries, DelOnly);
            return "Done";

        }

        public string Add_Printers(string[] Args, string Server, int Tries = 5)
        {
            int ProgressBarVals = 80 / Tries;
            //Console.WriteLine("Prog Bar Val " + ProgressBarVals);
            ProgressBarVals = ProgressBarVals / Args.Count();
            //Console.WriteLine("Prog Bar Val " + ProgressBarVals);
            //Console.WriteLine(Args[0]);
            String PrinterString = "null";
            //Console.WriteLine(Server);
            foreach (var arg in Args)

            {
                PrinterString = Convert.ToString(arg);
                //Console.WriteLine("In For loop "+PrinterString);
                //Console.WriteLine(PrinterString);
                for (int i = 0; i < Tries; i++)
                {
                    //Console.WriteLine(i);
                    ManagementClass classInstance =
                    new ManagementClass("root\\CIMV2", "Win32_Printer", null);
                    ManagementBaseObject inParams = classInstance.GetMethodParameters("AddPrinterConnection");
                    inParams["Name"] = "\\\\" + Server + "\\" + PrinterString;
                    ManagementBaseObject outParams = classInstance.InvokeMethod("AddPrinterConnection", inParams, null);
                    int b = i + 1;
                    StatusText.Text = "Trying to add \\\\" + Server + "\\" + PrinterString + " Attempt " + b;
                    Thread.Sleep(sleep);
                    ProgressBarUpdate(ProgressBarVals);
                }
            }
            /// <summary>
            /// /ADD AWAIT HERE
            /// </summary>
            StatusText.Text = "Complete, Goodbye";
            StatusText.Refresh();
            Thread.Sleep(sleep);
            //MainProgressIndicator.Value = 100;
            MainProgressIndicator.Update();

            Quit();

            return "done";
        }
        public string Del_Printers(bool ShouldDel, string[] PrintersToAdd, string Server, int Tries, bool DelOnly)
        {
            //ProgressBarUpdate(20);
            StatusText.Text = "Should I Delete Printers" + Convert.ToString(ShouldDel);
            if (ShouldDel)
            {
                StatusText.Text = "Deleteing Printers";
                ProgressBarUpdate(10);
                ConnectionOptions options = new ConnectionOptions();
                options.EnablePrivileges = true;
                ManagementScope scope = new ManagementScope(ManagementPath.DefaultPath, options);
                scope.Connect();
                ManagementClass win32Printer = new ManagementClass("Win32_Printer");
                ManagementObjectCollection printers = win32Printer.GetInstances();
                foreach (ManagementObject printer in printers)
                {
                    try
                    {
                        printer.Delete();
                    }
                    catch (Exception)
                    {
                        continue;
                        throw;
                    }
                }
                Thread.Sleep(3000);

                if (DelOnly)
                {
                    MainProgressIndicator.Value = 100;
                    MainProgressIndicator.Update();
                    Quit();

                }
                else
                {
                    Add_Printers(PrintersToAdd, Server, Tries);
                }



                return "done";
            }
            else
            {
                StatusText.Text = "Not Deleteing, lets move on";
                ProgressBarUpdate(20);
                Thread.Sleep(sleep);
                Add_Printers(PrintersToAdd, Server, Tries);
                return "done";
            }
        }
        public string ProgressBarUpdate(int Ammount)
        {
            int NewAmmount = MainProgressIndicator.Value + Ammount;
            //Console.WriteLine("New Ammount " + NewAmmount);
            MainProgressIndicator.Value = +NewAmmount;
            MainProgressIndicator.Update();
            Thread.Sleep(100);
            return "Added";
        }
        private void MainWindow_Shown_1(object sender, EventArgs e)
        {
            //MessageBox.Show("You are in the Form.Shown event.");
            string[] args = Environment.GetCommandLineArgs();
            StatusText.Text = "Praseing Arguments";
            StatusText.Refresh();
            Thread.Sleep(sleep);
            Handle_Args(args);
        }

        public string Quit()
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                Thread.Sleep(1000);
                System.Windows.Forms.Application.Exit();
                return " ";
            }
            else
            {
                Thread.Sleep(1000);
                // Console app
                System.Environment.Exit(1);
                return " ";
            }
        }
    }
}
