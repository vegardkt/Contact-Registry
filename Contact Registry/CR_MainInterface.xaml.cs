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
using System.Data;

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

        private void bRefreshContacts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Connection = "server= .\\SQLEXPRESS; Integrated Security = True";
                string Query = "Use ContactRegistry; Select * from CONTACTS";

                SqlConnection mySQLConnection = new SqlConnection(Connection); //Create Connection
                SqlCommand cmd = new SqlCommand(Query, mySQLConnection); //Create Query
                mySQLConnection.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);

                DataTable ds = new DataTable();
                
                adapt.Fill(ds);
                mySQLConnection.Close();
                ds.Columns.RemoveAt(0); //Remove the first column as this is simply the ID
                ds.Columns[0].ColumnName = "Name";
                ds.Columns[1].ColumnName = "Surname";
                ds.Columns[2].ColumnName = "Company";
                ds.Columns[3].ColumnName = "Phone";
                ds.Columns[4].ColumnName = "Mail";
                ds.Columns[5].ColumnName = "Title";

                dGrid.ItemsSource = ds.DefaultView;
                dGrid.Columns[0].Width = 120;
                dGrid.Columns[1].Width = 120;
                dGrid.Columns[2].Width = 140;
                dGrid.Columns[3].Width = 75;
                dGrid.Columns[4].Width = 145;
                dGrid.Columns[5].Width = 50;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bDeleteContacts_Click(object sender, RoutedEventArgs e)
        {
            
            if (dGrid.SelectedIndex != -1)
            {
                DataRowView drv = (DataRowView)dGrid.SelectedItem;


                string delName = drv[0].ToString().Trim();
                string delSurname = drv[1].ToString().Trim();
                string delCompany = drv[2].ToString().Trim();
                string delPhone = drv[3].ToString().Trim();
                string delMail = drv[4].ToString().Trim();
                string delTitle = drv[5].ToString().Trim();

                string Connection = "server= .\\SQLEXPRESS; Integrated Security = True";

                using (var sc = new SqlConnection(Connection))
                {
                    using (var cmd = sc.CreateCommand())
                    {
                        try
                        {
                            sc.Open();
                            cmd.CommandText = @"USE ContactRegistry;
                                      DELETE FROM CONTACTS WHERE
                                      Contact_FirstName =@delName AND
                                      Contact_Surname =@delSurname AND
                                      Contact_Company =@delCompany AND
                                      Contact_Phone =@delPhone AND
                                      Contact_Mail =@delMail AND
                                      Contact_Title =@delTitle;";

                            cmd.Parameters.AddWithValue("@delName", delName);
                            cmd.Parameters.AddWithValue("@delSurname", delSurname);
                            cmd.Parameters.AddWithValue("@delCompany", delCompany);
                            cmd.Parameters.AddWithValue("@delPhone", delPhone);
                            cmd.Parameters.AddWithValue("@delMail", delMail);
                            cmd.Parameters.AddWithValue("@delTitle", delTitle);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }


                }


                ((DataRowView)(dGrid.SelectedItem)).Row.Delete(); //Deletes row from table so we dont have to refresh from database
            }
            else
            {
                MessageBox.Show("You must select a row to delete!");
            }
           
        }

        private void bEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
