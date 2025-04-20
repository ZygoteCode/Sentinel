using System.Windows.Forms;

public partial class KeywordForm : MetroSuite.MetroForm
{
    public KeywordForm()
    {
        InitializeComponent();
    }

    private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode.Equals(Keys.Enter))
        {
            guna2Button14.PerformClick();
        }
    }

    private void guna2Button14_Click(object sender, System.EventArgs e)
    {
        try
        {
            Utils.WebBrowser.LoadUrl(Utils.Keywords[guna2TextBox1.Text.ToLower().Trim()]);
            Close();
        }
        catch
        {
            guna2TextBox1.Focus();
        }
    }
}