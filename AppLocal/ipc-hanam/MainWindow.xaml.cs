using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        private LoginPage _loginPage;
        private MainPage _mainPage;

        public MainWindow()
        {
            InitializeComponent();
            _loginPage = new LoginPage();
            _mainPage = new MainPage();
            _mainFrame.Navigate(_loginPage);
            Instance = this;
        }

        public void NavigateToLogin()
        {
            if (_mainFrame.Content != _loginPage)
            {
                _mainFrame.Navigate(_loginPage);
            }
        }

        public void NavigateToMain()
        {
            if (_mainFrame.Content != _mainPage)
            {
                _mainFrame.Navigate(_mainPage);
            }
        }
    }
}
