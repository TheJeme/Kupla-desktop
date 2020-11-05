using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kupla
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SecretPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MasterPasswordBox.Password) && !string.IsNullOrWhiteSpace(ServiceBox.Text))
            {
                String secretPassword = ComputeMD5Hash(MasterPasswordBox.Password + ServiceBox.Text.ToUpper() + "kupla");
                Clipboard.SetText(secretPassword);
                ClipboardLabel.Visibility = Visibility.Visible;
            }
        }

        static string ComputeMD5Hash(string rawdata)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(rawdata));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < Convert.ToInt32(bytes.Length / 2); i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                for (int i = Convert.ToInt32(bytes.Length / 2); i < bytes.Length - 5; i++)
                {
                    builder.Append(bytes[i].ToString("x2").ToUpper());
                }
                builder.Append("!");
                return builder.ToString();
            }
        }
    }
}
