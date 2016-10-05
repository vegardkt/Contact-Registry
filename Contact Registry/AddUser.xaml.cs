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

        public string currentMaxID;

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
            string Connection = "server= .\\SQLEXPRESS; Integrated Security = True";
            using (var sc = new SqlConnection(Connection))
            {
                using (var maxCmd = sc.CreateCommand())
                {
                    try
                    {
                        sc.Open();
                        maxCmd.CommandText = @"USE ContactRegistry; SELECT MAX(Contact_ID) AS Contact_ID FROM CONTACTS;";

                        using (var reader = maxCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentMaxID = reader["Contact_ID"].ToString().Trim();
                            }
                        }
                        //sc.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                using (var addCmd = sc.CreateCommand())
                {
                    try
                    {
                        //sc.Open();
                        addCmd.CommandText = @" 
                            INSERT INTO CONTACTS(Contact_ID,Contact_FirstName,Contact_Surname,Contact_Company,Contact_Phone,Contact_Mail,Contact_Title) 
                            VALUES(
                            @Contact_ID,
                            @Contact_FirstName,
                            @Contact_Surname,
                            @Contact_Company,
                            @Contact_Phone,
                            @Contact_Mail,
                            @Contact_Title)";



                        addCmd.Parameters.AddWithValue("@Contact_ID", (Convert.ToInt16(currentMaxID) + 1).ToString());
                        addCmd.Parameters.AddWithValue("@Contact_FirstName", tbName.Text.Trim());
                        addCmd.Parameters.AddWithValue("@Contact_Surname", tbSurname.Text.Trim());
                        addCmd.Parameters.AddWithValue("@Contact_Company", tbCompany.Text.Trim());
                        addCmd.Parameters.AddWithValue("@Contact_Phone", tbPhone.Text.Trim());
                        addCmd.Parameters.AddWithValue("@Contact_Mail", tbMail.Text.Trim());
                        addCmd.Parameters.AddWithValue("@Contact_Title", tbTitle.Text.Trim());

                        

                        addCmd.ExecuteNonQuery();

                        sc.Close();

                        this.Hide();
                        CR_MainInterface fm = new CR_MainInterface();
                        fm.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }                      
            }                   
        }
    }
}
