using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Contact_Registry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Connection = "server= .\\SQLEXPRESS; Integrated Security = True";
        string Query = "Use ContactRegistry; Select * from USERS where Users_Name=@Users_Name and Users_Password=@Users_Password";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbUserName.Text == "" || passwordBox.Password == "")
            {
                MessageBox.Show("Please enter a username and passw!");
            }
            else
            { 
                try
                {
                    SqlConnection mySQLConnection = new SqlConnection(Connection); //Create Connection
                    SqlCommand cmd = new SqlCommand(Query, mySQLConnection); //Create Query

                    cmd.Parameters.AddWithValue("@Users_Name", tbUserName.Text);
                    cmd.Parameters.AddWithValue("@Users_Password", passwordBox.Password);

                    mySQLConnection.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapt.Fill(ds);
                    mySQLConnection.Close();

                    int count = ds.Tables[0].Rows.Count;
                
                    if (count == 1)
                    {
                        MessageBox.Show("Login Successful!");
                        this.Hide();
                        CR_MainInterface fm = new CR_MainInterface();
                        fm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed!");
                    }

                    if (Debugger.IsAttached)
                    {
                        Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bExit_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
