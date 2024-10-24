using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DIPLOM
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Database=AutoSoundStore;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // Кнопка "Войти"
        {
            string emailOrPhone = textBox1.Text; // Предположим, что это текстовое поле для email или номера телефона
            string password = textBox2.Text; // Это текстовое поле для пароля

            string role = AuthenticateUser(emailOrPhone, password);
            if (role != null)
            {
                MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (role == "администратор") // Проверка на роль администратора
                {
                    Administr adminForm = new Administr(); // Открываем форму администратора
                    adminForm.Show();
                }
                if (role == "менеджер") // Проверка на роль администратора
                {
                    Manager Manager = new Manager(); // Открываем форму администратора
                    Manager.Show();
                }
                if (role == "Покупатель")
                {
                    Form2 mainForm = new Form2(); // Открываем главную форму для обычного пользователя
                    mainForm.Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неправильные учетные данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string AuthenticateUser(string emailOrPhone, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Role FROM Users WHERE (Email = @EmailOrPhone OR PhoneNumber = @EmailOrPhone) AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmailOrPhone", emailOrPhone);
                    cmd.Parameters.AddWithValue("@Password", password); // Здесь лучше использовать хешированный пароль

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null; // Возвращаем роль или null
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Кнопка "Регистрация"
        {
            REGISTR regForm = new REGISTR();
            this.Hide(); // Скрыть текущую форму
            regForm.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
