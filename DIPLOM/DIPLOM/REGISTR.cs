using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DIPLOM
{
    public partial class REGISTR : Form
    {
        private string connectionString = "Server=localhost;Database=AutoSoundStore;Integrated Security=True;"; // Замените на вашу строку подключения

        public REGISTR()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Обработка нажатия кнопки "Зарегистрироваться"
        {
            string email = textBox1.Text; // Предполагается, что это текстовое поле для email
            string phoneNumber = textBox2.Text; // Это текстовое поле для номера телефона
            string password = textBox3.Text; // Это текстовое поле для пароля

            if (RegisterUser(email, phoneNumber, password))
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Закрытие формы или можно открыть другую форму
            }
            else
            {
                MessageBox.Show("Ошибка регистрации. Пользователь с таким email или номером телефона уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool RegisterUser(string email, string phoneNumber, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Email, PhoneNumber, Password, Role) VALUES (@Email, @PhoneNumber, @Password, 'Покупатель')";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Password", password); // Хранение пароля в открытом виде

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true; // Успех регистрации
                    }
                    catch (SqlException ex) when (ex.Number == 2627) // Код ошибки уникального ограничения
                    {
                        return false; // Пользователь с таким email или номером телефона уже существует
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Обработка текста, если нужно
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Обработка текста, если нужно
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = (Form1)sender;
            Hide();
            form.ShowDialog();
        }
    }
}
