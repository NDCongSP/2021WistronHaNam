using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace ipc_hanam
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        private ImageSource _imgBackgroundSource;

        public LoginPage()
        {
            InitializeComponent();
            _btnLogin.Click += _btnLogin_Click;
            Loaded += LoginPage_Loaded;
        }

        private void _btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshBackground();
        }

        public void Login()
        {
            if (_txbUsername.Text == "admin" &&
                _txbPassword.Password == "admin")
            {
                MainWindow.Instance.NavigateToMain();
            }
            else
            {
                MessageBox.Show($"Wrong user name or password !");
            }
        }

        public void RefreshBackground()
        {
            try
            {
                _imgBackground.Source = null;
                _imgBackgroundSource = null;
                string backgroundPath = System.IO.Path.Combine(IoC.Instance.AppPath, "background.jpg");

                if (File.Exists(backgroundPath))
                {
                    Uri imgUri = new Uri(backgroundPath);
                    _imgBackgroundSource = new BitmapImage(imgUri);
                }

                _imgBackground.Source = _imgBackgroundSource;
            }
            catch { }
        }
    }
}
