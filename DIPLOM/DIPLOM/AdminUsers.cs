using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DIPLOM
{
    public partial class AdminUsers : Form
    {
        private string connectionString = "Server=localhost;Database=AutoSoundStore;Integrated Security=True;";

        public AdminUsers()
        {
            InitializeComponent();
        }

        private void AdminUsers_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Администратор");
            comboBox1.Items.Add("Покупатель");
            comboBox1.Items.Add("Менеджер");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string phoneNumber = textBox2.Text;
            string password = textBox3.Text;

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите роль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string role = comboBox1.SelectedItem.ToString();

            if (RegisterUser(email, phoneNumber, password, role))
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка регистрации. Пользователь с таким email или номером телефона уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool RegisterUser(string email, string phoneNumber, string password, string role)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Email, PhoneNumber, Password, Role) VALUES (@Email, @PhoneNumber, @Password, @Role)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Role", role);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException ex) when (ex.Number == 2627)
                    {
                        return false;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                switch (selectedValue)
                {
                    case "Администратор":
                        break;
                    case "Менеджер":
                        break;
                    case "Покупатель":
                        break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Administr adm= (Administr)sender;
            Hide();
            adm.ShowDialog();
        }
    }
}
