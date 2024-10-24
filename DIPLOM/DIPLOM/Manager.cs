using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DIPLOM
{
    public partial class Manager : Form
    {
        private string connectionString = "Server=localhost;Database=AutoSoundStore;Integrated Security=True;";

        public Manager()
        {
            InitializeComponent();
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            LoadData(); // Загружаем данные при загрузке формы
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                Console.WriteLine(column.Name); // Выводим имена всех столбцов в консоль
            }
        }

        private void LoadData()
        {
            // Обновляем данные в таблице "Products"
            this.productsTableAdapter.Fill(this.autoSoundStoreDataSet.Products);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagerProducts managerProducts = new ManagerProducts();
            managerProducts.FormClosed += (s, args) => LoadData(); // Перезагрузим данные после закрытия
            managerProducts.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Используем индекс столбца, например, если ProductID — это первый столбец, используем 0
                // Замените 0 на правильный индекс столбца при необходимости
                int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить этот продукт?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (DeleteProduct(productId))
                    {
                        MessageBox.Show("Продукт успешно удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Обновляем данные в таблице
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления продукта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool DeleteProduct(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE ProductID = @ProductID"; // Убедитесь, что это правильное название столбца
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true; // Успех удаления продукта
                    }
                    catch (SqlException ex)
                    {
                        // Логирование ошибки можно добавить здесь
                        return false; // Не удалось удалить продукт
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Действия при нажатии на ячейку (если необходимы)
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form= (Form1)sender;
            Hide();
            form.ShowDialog();
        }
    }
}
