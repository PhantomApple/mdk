using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DIPLOM
{
    public partial class ManagerProducts : Form
    {
        private string connectionString = "Server=localhost;Database=AutoSoundStore;Integrated Security=True;";

        public ManagerProducts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string productName = textBox1.Text; // Предполагается, это текстовое поле для названия продукта
            string description = textBox2.Text; // Это текстовое поле для описания продукта
            decimal price;
            int stock;

            // Проверка валидности цены
            if (!decimal.TryParse(textBox3.Text, out price))
            {
                MessageBox.Show("Введите корректную цену.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка валидности количества на складе
            if (!int.TryParse(textBox4.Text, out stock))
            {
                MessageBox.Show("Введите корректное количество на складе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AddProduct(productName, description, price, stock))
            {
                MessageBox.Show("Продукт успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка добавления продукта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AddProduct(string productName, string description, decimal price, int stock)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (ProductName, Description, Price, Stock, CreatedAt) VALUES (@ProductName, @Description, @Price, @Stock, @CreatedAt)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now); // Устанавливаем текущую дату и время

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true; // Успех добавления продукта
                    }
                    catch (SqlException ex)
                    {
                        // Логирование ошибки можно добавить здесь
                        return false; // Не удалось добавить продукт
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
