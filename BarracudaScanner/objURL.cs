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
    class objURL
    {
        public string URL = "";
        public string blocked = "Unknown";

        public objURL(string u)
        {
            this.URL = u;
            //TestUrl(URL);

            Thread U = new Thread(new ThreadStart(TestUrl));
            U.Start();

            //TestUrl();

        }

        public void TestUrl()
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] data = wc.DownloadData(URL);
                string strData = Encoding.ASCII.GetString(data);

                //strData = strData.Substring(0, strData.IndexOf("</title>"));

                if (strData.Contains("Blocked Site"))
                {
                    blocked = "blocked";
                    //MessageBox.Show(URL + "\n" + blocked);

                }
                else
                {
                    blocked = "not blocked";
                }
            }
            catch
            {
                blocked = "not blocked";
            }

        }


    }
}
