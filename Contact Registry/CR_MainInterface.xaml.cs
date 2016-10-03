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
using System.Windows.Shapes;

namespace Contact_Registry
{
    /// <summary>
    /// Interaction logic for CR_MainInterface.xaml
    /// </summary>
    public partial class CR_MainInterface : Window
    {
        public CR_MainInterface()
        {
            InitializeComponent();
        }

        private void bLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow fm = new MainWindow();
            fm.Show();
        }

        private void bAddContact_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddUser fm = new AddUser();
            fm.Show();
        }
    }
}
