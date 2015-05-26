using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;


namespace BarracudaScanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = textBox1.Text.Length;
            }
        }



        private void Scan()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";

            label1.Text = "Processing...";

            try
            {
                webBrowser1.Navigate(textBox1.Text);
                //textBox1.Text = webBrowser1.Url.ToString();
            }
            catch
            {
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Scan();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
                System.Net.WebClient wc = new System.Net.WebClient();
                string url = webBrowser1.Url.ToString();
                byte[] data = wc.DownloadData(url);
                string strData = Encoding.ASCII.GetString(data);
                richTextBox1.Text = strData;

                String html = strData; // File.ReadAllText(url); //File.ReadAllText(path);
                //String pattern = @"http:\/\/[^""]*";
                //String pattern = "http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?";
                //String pattern = "((https?|ftp|gopher|telnet|file):((//)|(\\\\))+[\\w\\d:#@%/;$()~_?\\+-=\\\\\\.&]*)";
                String pattern = "((https?|ftp|gopher|telnet|file):((//)|(\\\\))+[\\w\\d:#@%/;$()~_?\\+-=\\\\\\.&]*)";    
                Regex rx = new Regex(pattern);
                
            
                Match m = rx.Match(html);

                List<objURL> list = new List<objURL>();
                int items = 0;

            
                while (m.Success)
                {
                    string shortened = ShortenUrl(m.Value);

                    if (richTextBox2.Text.Contains(shortened) == false && richTextBox3.Text.Contains(shortened) == false)
                    {
                        if (shortened.IndexOf("http://") == 0 || shortened.IndexOf("https://") == 0)
                        {
                            richTextBox3.Text += (shortened + "\n");
                            try
                            {
                                //objURL i = new objURL(shortened);

                                list.Add(new objURL(shortened));
                                items++;
                                //Thread k = new Thread(new ThreadStart(TestUrl(shortened)));

                                //TestUrl(shortened);
                            }
                            catch
                            {
                                //richTextBox3.Text += (shortened + "\n");
                            }
                        }
                        
                    }
                    m = m.NextMatch();
                }

                while (items > 0)
                {
                    objURL tracker = list.First();

                   if (tracker.blocked.Equals("blocked"))
                   {
                            richTextBox2.Text += (tracker.URL + "\n");
                            list.Remove(tracker);
                            items--;
                    }
                    else if (tracker.blocked.Equals("not blocked"))
                    {
                         list.Remove(tracker);
                         items--;
                    }
                }
                

                label1.Text = "Finished";
        }

        private string ShortenUrl(string u)
        {
            if (u.Length >= 8)
            {
                int endIndex = u.IndexOf("/", 8) + 1;
                u = u.Substring(0, endIndex);
                return u;
            }
            else
                return "";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Scan();
                e.SuppressKeyPress = true;
            }
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
        }

    }
}
