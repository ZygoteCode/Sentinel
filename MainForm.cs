using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using DnsClient;
using Microsoft.VisualBasic;
using PhoneNumbersLib;

public partial class MainForm : MetroSuite.MetroForm
{
    public MainForm()
    {
        InitializeComponent();
        CheckForIllegalCrossThreadCalls = false;
        chromiumWebBrowser1.LifeSpanHandler = new CustomLifeSpanHandler();
        chromiumWebBrowser1.AddressChanged += ChromiumWebBrowser1_AddressChanged;
        chromiumWebBrowser1.LoadUrl("https://priv.au/");
        Utils.WebBrowser = chromiumWebBrowser1;
    }

    private byte[] ReadFully(Stream input)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }

    private string GetWhoIs(string domain)
    {
        try
        {
            var request1 = (HttpWebRequest)WebRequest.Create($"https://www.whois.com/whois/{domain}");

            request1.Proxy = null;
            request1.UseDefaultCredentials = false;
            request1.AllowAutoRedirect = false;
            request1.Timeout = 70000;

            var field1 = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);
            request1.Method = "GET";

            var headers1 = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "www.whois.com",
                ["Connection"] = "keep-alive",
                ["Upgrade-Insecure-Requests"] = "1",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/133.0.0.0 Safari/537.36",
                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "navigate",
                ["Sec-Fetch-User"] = "?1",
                ["Sec-Fetch-Dest"] = "document",
                ["sec-ch-ua-mobile"] = "?0",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept-Language"] = "it-IT,it;q=0.9,en-US;q=0.8,en;q=0.7",
                ["Cookie"] = "cookieconsent_prompt=1",
            });

            field1.SetValue(request1, headers1);

            var response1 = request1.GetResponse();
            string content1 = Encoding.UTF8.GetString(ReadFully(response1.GetResponseStream()));
            string[] splitted = Strings.Split(content1, "http://web-whois.nic.it");
            content1 = splitted[1];
            splitted = Strings.Split(content1, "Domain: ");
            content1 = "Domain: " + splitted[1];
            splitted = Strings.Split(content1, "</pre></div></div>");
            content1 = splitted[0];

            return content1;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private void ChromiumWebBrowser1_AddressChanged(object sender, AddressChangedEventArgs e)
    {
        guna2TextBox1.Text = e.Address;
    }

    private void guna2Button1_Click(object sender, System.EventArgs e)
    {
        chromiumWebBrowser1.Back();
    }

    private void guna2Button2_Click(object sender, System.EventArgs e)
    {
        chromiumWebBrowser1.Forward();
    }

    private void guna2Button3_Click(object sender, System.EventArgs e)
    {
        chromiumWebBrowser1.Reload();
    }

    private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            if (Utils.Keywords.ContainsKey(guna2TextBox1.Text.ToLower().Trim()))
            {
                chromiumWebBrowser1.LoadUrl(Utils.Keywords[guna2TextBox1.Text.ToLower().Trim()]);
                return;
            }

            if (Uri.IsWellFormedUriString(guna2TextBox1.Text, UriKind.Absolute))
            {
                chromiumWebBrowser1.LoadUrl(guna2TextBox1.Text);
            }
            else
            {
                chromiumWebBrowser1.LoadUrl($"https://www.google.com/search?q={guna2TextBox1.Text}");
            }
        }
    }

    private void guna2Button4_Click(object sender, System.EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://who.is/");
    }

    private void guna2Button5_Click(object sender, System.EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://www.vedbex.com/skyperesolver");
    }

    private void guna2Button6_Click(object sender, System.EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://haveibeenpwned.com/");
    }

    private void guna2Button7_Click(object sender, System.EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://shodan.io/");
    }

    private void guna2Button8_Click(object sender, System.EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://www.exploit-db.com/google-hacking-database/");
    }

    private void guna2Button9_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://priv.au/");
    }

    private void guna2Button10_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://www.tineye.com/");
    }

    private void guna2Button11_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://whatsmyname.app/");
    }

    private void guna2Button12_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://perplexity.ai/");
    }

    private void guna2Button13_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://osintframework.com/");
    }

    private void guna2Button14_Click_1(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://ipinfo.io/");
    }

    private void guna2Button15_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://maps.google.com/");
    }

    private void guna2Button16_Click(object sender, EventArgs e)
    {
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);
        LookupClient lookupClient = new LookupClient(ipEndPoint);
        IDnsQueryResponse theQuery = lookupClient.Query(guna2TextBox2.Text, QueryType.A);
        string ips = "";

        foreach (DnsClient.Protocol.ARecord aRecord in theQuery.Answers.ARecords())
        {
            if (ips == "")
            {
                ips = aRecord.Address.ToString();
            }
            else
            {
                ips += ", " + aRecord.Address.ToString();
            }
        }

        guna2TextBox3.Text = ips;
    }

    private bool IsDomainSeized(string domain)
    {
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);
        LookupClient lookupClient = new LookupClient(ipEndPoint);
        IDnsQueryResponse theQuery = lookupClient.Query(domain, QueryType.SOA);

        foreach (DnsClient.Protocol.SoaRecord soaRecord in theQuery.Answers.SoaRecords())
        {
            if (soaRecord.MName.ToString().Contains("fbi.seized.gov"))
            {
                return true;
            }
        }

        return false;
    }

    private void guna2TextBox2_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button16.PerformClick();
        }
    }

    private void guna2Button17_Click(object sender, EventArgs e)
    {
        guna2TextBox4.Text = IsDomainSeized(guna2TextBox5.Text).ToString();
    }

    private void guna2TextBox5_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button17.PerformClick();
        }
    }

    private void guna2Button18_Click(object sender, EventArgs e)
    {
        guna2TextBox6.Text = (SuperEmailValidator.IsEmailValid(guna2TextBox7.Text, true, true, true, true, true)).ToString();
    }

    private void guna2TextBox7_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button18.PerformClick();
        }
    }

    private void guna2Button19_Click(object sender, EventArgs e)
    {
        KeywordForm keywordForm = new KeywordForm();
        keywordForm.Show();
    }

    private void guna2Button20_Click(object sender, EventArgs e)
    {
        PhoneNumber informations = new PhoneNumber(guna2TextBox9.Text);

        guna2TextBox8.Text = $"Complete phone number: {informations.ToString()}\r\n" +
            $"Prefix: +{informations.Prefix}\r\n" +
            $"Number: {informations.Number}\r\n" +
            $"Country: {informations.Country}\r\n" +
            $"Country code: {informations.CountryCode}\r\n" +
            $"Country with code: {informations.Country} ({informations.CountryCode})\r\n" +
            (informations.Country == "Italy" ? $"Possible italian operator: {informations.GetPossibleItalianOperator()}\r\n" : "");
    }

    private void guna2TextBox9_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button20.PerformClick();
        }
    }

    private void guna2Button21_Click(object sender, EventArgs e)
    {
        try
        {
            IPAddress ipAddress = null;

            if (!ValidateIPv4(guna2TextBox11.Text))
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);
                LookupClient lookupClient = new LookupClient(ipEndPoint);
                IDnsQueryResponse theQuery = lookupClient.Query(guna2TextBox11.Text, QueryType.A);

                foreach (DnsClient.Protocol.ARecord aRecord in theQuery.Answers.ARecords())
                {
                    ipAddress = aRecord.Address;
                    break;
                }
            }
            else
            {
                ipAddress = IPAddress.Parse(guna2TextBox11.Text);
            }

            Ping ping = new Ping();
            PingReply pingReply = ping.Send(ipAddress);
            guna2TextBox10.Text = pingReply.RoundtripTime.ToString() + "ms";
        }
        catch (Exception ex)
        {
            guna2TextBox10.Text = $"{ex.Message}";
        }
    }

    public bool ValidateIPv4(string ipString)
    {
        if (String.IsNullOrWhiteSpace(ipString))
        {
            return false;
        }

        string[] splitValues = ipString.Split('.');
        if (splitValues.Length != 4)
        {
            return false;
        }

        byte tempForParsing;

        return splitValues.All(r => byte.TryParse(r, out tempForParsing));
    }

    private void guna2TextBox11_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button21.PerformClick();
        }
    }


    private long PingHost(IPAddress ipAddress, int port)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                client.Connect(ipAddress, port);
                stopwatch.Stop();
                return stopwatch.ElapsedMilliseconds;
            }
        }
        catch (Exception)
        {
            return -1;
        }
    }

    private void guna2Button22_Click(object sender, EventArgs e)
    {
        try
        {
            IPAddress ipAddress = null;

            if (!ValidateIPv4(guna2TextBox13.Text))
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);
                LookupClient lookupClient = new LookupClient(ipEndPoint);
                IDnsQueryResponse theQuery = lookupClient.Query(guna2TextBox13.Text, QueryType.A);

                foreach (DnsClient.Protocol.ARecord aRecord in theQuery.Answers.ARecords())
                {
                    ipAddress = aRecord.Address;
                    break;
                }
            }
            else
            {
                ipAddress = IPAddress.Parse(guna2TextBox13.Text);
            }

            guna2TextBox12.Text = PingHost(ipAddress, 80) + "ms";
        }
        catch (Exception ex)
        {
            guna2TextBox12.Text = $"{ex.Message}";
        }
    }

    private void guna2TextBox13_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button22.PerformClick();
        }
    }

    private void guna2Button23_Click(object sender, EventArgs e)
    {
        guna2TextBox14.Text = GetWhoIs(guna2TextBox15.Text);
    }

    private void guna2TextBox15_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button23.PerformClick();
        }
    }

    private void guna2Button24_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://namemc.com/");
    }

    private void guna2Button25_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://steamdb.info/");
    }

    private void guna2Button26_Click(object sender, EventArgs e)
    {
        chromiumWebBrowser1.LoadUrl("https://web.archive.org/");
    }

    private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\Nmap";
        cmd.Start();

        cmd.StandardInput.WriteLine("\"C:\\Program Files (x86)\\Nmap\\zenmap\\bin\\pythonw.exe\" -c \"from zenmapGUI.App import run;run()\"");
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
    }

    private void guna2TextBox17_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button27.PerformClick();
        }
    }

    private void guna2TextBox19_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button28.PerformClick();
        }
    }

    private void guna2Button29_Click(object sender, EventArgs e)
    {
        openFileDialog1.FileName = "";

        if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
        {
            guna2TextBox17.Text = openFileDialog1.FileName;
        }
    }

    private static IEnumerable<string> SplitToLines(string input)
    {
        if (input == null)
        {
            yield break;
        }

        using (System.IO.StringReader reader = new System.IO.StringReader(input))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }

    private void guna2Button27_Click(object sender, EventArgs e)
    {
        try
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WorkingDirectory = Path.GetFullPath("tools");
            cmd.Start();

            cmd.StandardInput.WriteLine($"exiftool \"{guna2TextBox17.Text}\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            string[] splitted = Strings.Split(cmd.StandardOutput.ReadToEnd(), "ExifTool Version Number");
            string result = "ExifTool Version Number" + splitted[1];
            List<string> lines = new List<string>();

            foreach (string line in SplitToLines(result))
            {
                string newLine = line;

                while (newLine.Contains("  "))
                {
                    newLine = newLine.Replace("  ", " ");
                }

                while (newLine.Contains('\t'.ToString()))
                {
                    newLine = newLine.Replace('\t'.ToString(), "");
                }

                lines.Add(newLine);
            }

            result = "";

            foreach (string line in lines)
            {
                if (result == "")
                {
                    result = line;
                }
                else
                {
                    result += "\r\n" + line;
                }
            }

            guna2TextBox16.Text = result;
        }
        catch (Exception ex)
        {
            guna2TextBox16.Text = ex.Message;
        }
    }

    private void guna2Button28_Click(object sender, EventArgs e)
    {
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = true;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.StartInfo.WorkingDirectory = Path.GetFullPath("tools\\sherlock");
        cmd.Start();

        cmd.StandardInput.WriteLine($"env\\python.exe sherlock.py {guna2TextBox19.Text}");
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();

        string result = "";

        foreach (string line in SplitToLines(cmd.StandardOutput.ReadToEnd()))
        {
            if (line.Contains("[+] "))
            {
                if (result == "")
                {
                    result = line.Trim();
                }
                else
                {
                    result += "\r\n" + line.Trim();
                }
            }
        }

        guna2TextBox18.Text = result;
    }

    private void guna2Button32_Click(object sender, EventArgs e)
    {
        if (betterFolderBrowser1.ShowDialog().Equals(DialogResult.OK))
        {
            guna2TextBox20.Text = betterFolderBrowser1.SelectedFolder;
        }
    }

    private void guna2TextBox21_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button30.PerformClick();
        }
    }

    private void guna2TextBox22_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button31.PerformClick();
        }
    }

    private void guna2Button31_Click(object sender, EventArgs e)
    {

    }

    private void guna2Button30_Click(object sender, EventArgs e)
    {
        string command = "", dir = "";

        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = false;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.StartInfo.WorkingDirectory = Path.GetFullPath("tools");
        cmd.Start();

        if (betterFolderBrowser2.ShowDialog().Equals(DialogResult.OK))
        {
            dir = betterFolderBrowser2.SelectedFolder;
        }

        if (guna2RadioButton2.Checked)
        {
            command = $"yt-dlp -f bestvideo+bestaudio --merge-output-format mkv --write-info-json -o \"{dir}\\%(title)s.%(ext)s\" {guna2TextBox21.Text}";

        }
        else
        {
            command = $"yt-dlp -x --audio-format opus --audio-quality 0 URL --write-info-json -o \"{dir}\\%(title)s.%(ext)s\"  {guna2TextBox21.Text}";
        }

        cmd.StandardInput.WriteLine(command);
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();

        MessageBox.Show("The YouTube video is succesfully downloaded.", "Sentinel - YouTube Video Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void guna2Button33_Click(object sender, EventArgs e)
    {
        guna2TextBox23.Text = $"env\\python.exe main.py email {guna2TextBox24.Text} --json user_data.json";
        Process.Start(Path.GetFullPath("tools\\ghunt"));
        Clipboard.SetText(guna2TextBox23.Text);
    }

    private void guna2TextBox24_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button33.PerformClick();
        }
    }

    private void guna2Button34_Click(object sender, EventArgs e)
    {
        string result = "";
        Process cmd = new Process();
        cmd.StartInfo.FileName = "cmd.exe";
        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.RedirectStandardOutput = false;
        cmd.StartInfo.CreateNoWindow = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.StartInfo.WorkingDirectory = Path.GetFullPath("tools\\subdomainsfinder");
        cmd.Start();

        cmd.StandardInput.WriteLine($"env\\python.exe sublist3r.py -d {guna2TextBox26.Text} -t 10 -o subdomains.txt");
        cmd.StandardInput.Flush();
        cmd.StandardInput.Close();
        cmd.WaitForExit();

        foreach (string line in File.ReadAllLines(Path.GetFullPath("tools\\subdomainsfinder\\subdomains.txt")))
        {
            if (result == "")
            {
                result = line;
            }
            else
            {
                result += " " + line;
            }
        }

        guna2TextBox25.Text = result.Replace("  ", " ");
    }

    private void guna2TextBox26_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button34.PerformClick();
        }
    }

    private void guna2TextBox3_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox3.SelectAll();
    }

    private void guna2TextBox4_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox4.SelectAll();
    }

    private void guna2TextBox14_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox14.SelectAll();
    }

    private void guna2TextBox8_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox8.SelectAll();
    }

    private void guna2TextBox10_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox10.SelectAll();
    }

    private void guna2TextBox12_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox12.SelectAll();
    }

    private void guna2TextBox6_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox6.SelectAll();
    }

    private void guna2TextBox16_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox16.SelectAll();
    }

    private void guna2TextBox18_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox18.SelectAll();
    }

    private void guna2TextBox23_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox23.SelectAll();
    }

    private void guna2TextBox25_MouseClick(object sender, MouseEventArgs e)
    {
        guna2TextBox25.SelectAll();
    }

    private void guna2TextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        guna2TextBox1.SelectAll();
    }

    private void guna2Button35_Click(object sender, EventArgs e)
    {
        test1();
        test2();
    }

    void test1()
    {
        string toBeFound = guna2TextBox28.Text;

        string filePath = @"C:\Users\ZygoteCode\Desktop\Computer\Sentinel\Sentinel\bin\Release\data\Combolists\Collection #1\Collection1_30.txt";
        int bufferSize = 16 * 1024 * 1024;
        List<string> foundPasswords = new List<string>();

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (StreamReader reader = new StreamReader(fs, Encoding.UTF8, true, bufferSize))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int lastColon = line.LastIndexOf(':');
                if (lastColon == -1)
                    continue;

                int secondLastColon = (lastColon > 0) ? line.LastIndexOf(':', lastColon - 1) : -1;
                int startIndex = (secondLastColon != -1) ? secondLastColon + 1 : 0;

                string segment = line.Substring(startIndex);

                int separatorIndex = segment.IndexOf(':');
                if (separatorIndex == -1)
                    continue;

                string username = segment.Substring(0, separatorIndex);
                string password = segment.Substring(separatorIndex + 1);

                if (username == toBeFound)
                {
                    foundPasswords.Add(password);
                }
            }
        }

        foreach (string foundPassword in foundPasswords)
        {
            MessageBox.Show(foundPassword);
        }
    }

    void test2()
    {
        string toBeFound = guna2TextBox28.Text;

        Parallel.For(0, 38, new ParallelOptions { MaxDegreeOfParallelism = 4 }, i =>
        {
            int bufferSize = 16 * 1024 * 1024;
            string filePath = $@"C:\Users\ZygoteCode\Desktop\Computer\Sentinel\Sentinel\bin\Release\data\Combolists\Collection #1\Collection1_{i + 1}.txt";
            List<string> foundPasswords = new List<string>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8, true, bufferSize))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int lastColon = line.LastIndexOf(':');
                    if (lastColon == -1)
                        continue;

                    int secondLastColon = (lastColon > 0) ? line.LastIndexOf(':', lastColon - 1) : -1;
                    int startIndex = (secondLastColon != -1) ? secondLastColon + 1 : 0;

                    string segment = line.Substring(startIndex);
                    int separatorIndex = segment.IndexOf(':');
                    if (separatorIndex == -1)
                        continue;

                    string username = segment.Substring(0, separatorIndex);
                    string password = segment.Substring(separatorIndex + 1);

                    if (username == toBeFound)
                    {
                        foundPasswords.Add(password);
                    }
                }
            }

            MessageBox.Show("Finished: " + i);
        });
    }
}
