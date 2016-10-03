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
using System.Data.SqlClient;
using System.Diagnostics;

namespace Contact_Registry
{

    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {

        string Connection = "server= .\\SQLEXPRESS; Integrated Security = True";
        string Query = @"Use ContactRegistry; 
                            INSERT INTO CONTACTS(Contact_ID,Contact_Firstname,Contact_Surname,Contact_Company,Contact_Phone,Contact_Mail,Contact_Title) 
                            VALUES(
                            '@Contact_ID',
                            '@Contact_FirstName',
                            '@Contact_Surname',
                            '@Contact_Company',
                            '@Contact_Phone',
                            '@Contact_Mail',
                            '@Contact_Title')";

        public AddUser()
        {
            InitializeComponent();
            
        }

        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CR_MainInterface fm = new CR_MainInterface();
            fm.Show();
        }

        private void bAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection mySQLConnection = new SqlConnection(Connection); //Create Connection
                SqlCommand cmd = new SqlCommand(Query, mySQLConnection); //Create Query

                cmd.Parameters.AddWithValue("@Contact_ID", tbName.Text);
                cmd.Parameters.AddWithValue("@Contact_FirstName", tbName.Text);
                cmd.Parameters.AddWithValue("@Contact_Surname", tbSurname.Text);
                cmd.Parameters.AddWithValue("@Contact_Company", tbCompany.Text);
                cmd.Parameters.AddWithValue("@Contact_Phone", tbPhone.Text);
                cmd.Parameters.AddWithValue("@Contact_Mail", tbMail.Text);
                cmd.Parameters.AddWithValue("@Contact_Title", tbTitle.Text);

                mySQLConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                MessageBox.Show("Contact Added!");

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
}
