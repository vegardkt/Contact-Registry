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

namespace Contact_Registry
{
    /// <summary>
    /// Interaction logic for CR_EditContact.xaml
    /// </summary>
    public partial class CR_EditContact : Window
    {
        public string id_selected;
        public CR_EditContact()
        {
            InitializeComponent();
            Loaded += CR_EditContact_Loaded;

        }

        private void CR_EditContact_Loaded(object sender, RoutedEventArgs e)
        {

            string o_editName = tbName.Text.Trim();
            string o_editSurname = tbSurname.Text.Trim();
            string o_editCompany = tbCompany.Text.Trim();
            string o_editPhone = tbPhone.Text.Trim();
            string o_editMail = tbMail.Text.Trim();
            string o_editTitle = tbTitle.Text.Trim();

            //Search for the selected user so we have the nescessary search infromation after everything is updated. Probably not the best way to do it.
            string Connection = "server= .\\SQLEXPRESS; Integrated Security = True";
            using (var sc = new SqlConnection(Connection))
            {
                using (var cmd = sc.CreateCommand())
                {
                    try
                    {
                        sc.Open();
                        cmd.CommandText = @"USE ContactRegistry;
                                      SELECT * FROM CONTACTS WHERE
                                      Contact_FirstName =@o_editName AND
                                      Contact_Surname =@o_editSurname AND
                                      Contact_Company =@o_editCompany AND
                                      Contact_Phone =@o_editPhone AND
                                      Contact_Mail =@o_editMail AND
                                      Contact_Title =@o_editTitle;";

                        cmd.Parameters.AddWithValue("@o_editName", o_editName);
                        cmd.Parameters.AddWithValue("@o_editSurname", o_editSurname);
                        cmd.Parameters.AddWithValue("@o_editCompany", o_editCompany);
                        cmd.Parameters.AddWithValue("@o_editPhone", o_editPhone);
                        cmd.Parameters.AddWithValue("@o_editMail", o_editMail);
                        cmd.Parameters.AddWithValue("@o_editTitle", o_editTitle);
                        cmd.ExecuteNonQuery();

                        //Store results of search
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id_selected = reader["Contact_ID"].ToString().Trim();
                            }
                        }
                        sc.Close(); //Done getting initial information
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }



        private void bEditContact_Click(object sender, RoutedEventArgs e)
        {
            string Connection = "server= .\\SQLEXPRESS; Integrated Security = True";
            using (var sc = new SqlConnection(Connection))
            {
                using (var cmd = sc.CreateCommand())
                {
                    try
                    {
                        sc.Open();
                        cmd.CommandText = @"USE ContactRegistry;
                                      Update CONTACTS SET
                                      Contact_FirstName =@n_editName,
                                      Contact_Surname =@n_editSurname,
                                      Contact_Company =@n_editCompany,
                                      Contact_Phone =@n_editPhone,
                                      Contact_Mail =@n_editMail,
                                      Contact_Title =@n_editTitle
                                      WHERE 
                                      Contact_ID = @id_selected;";

                        cmd.Parameters.AddWithValue("@id_selected", id_selected);
                        cmd.Parameters.AddWithValue("@n_editName", tbName.Text.Trim());
                        cmd.Parameters.AddWithValue("@n_editSurname", tbSurname.Text.Trim());
                        cmd.Parameters.AddWithValue("@n_editCompany", tbCompany.Text.Trim());
                        cmd.Parameters.AddWithValue("@n_editPhone", tbPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@n_editMail", tbMail.Text.Trim());
                        cmd.Parameters.AddWithValue("@n_editTitle", tbTitle.Text.Trim());
                        
                        cmd.ExecuteNonQuery();
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
        private void bCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CR_MainInterface fm = new CR_MainInterface();
            fm.Show();
        }
    }
}
