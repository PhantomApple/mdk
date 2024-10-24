using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DIPLOM
{
    public partial class Administr : Form
    {
        private string connectionString = "Server=localhost;Database=AutoSoundStore;Integrated Security=True;";

        public Administr()
        {
            InitializeComponent();
        }

        private void Administr_Load(object sender, EventArgs e)
        {
            LoadData(); // Загружаем данные при загрузке формы
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                Console.WriteLine(column.Name); // Выводим имена всех столбцов в консоль
            }
        }

        private void LoadData()
        {
            try
            {
                // Обновляем данные в таблице "Users"
                this.usersTableAdapter.Fill(this.autoSoundStoreDataSet.Users);
                dataGridView1.Refresh(); // Обновляем отображение DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminUsers adminUsers = new AdminUsers();
            // Обработаем событие закрытия формы AdminUsers
            adminUsers.FormClosed += (s, args) => LoadData(); // Перезагрузим данные после закрытия
            adminUsers.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Используем индекс столбца, например, если UserID — это первый столбец, используем 0
                // Замените 0 на правильный индекс столбца при необходимости
                int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // Убедитесь, что индекс правильный

                DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (DeleteUser(userId))
                    {
                        MessageBox.Show("Пользователь успешно удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Обновляем данные в таблице
                    }
                    else
                    {
                        MessageBox.Show("Ошибка удаления пользователя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool DeleteUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Измените "Users" на правильное имя вашей таблицы пользователей, если необходимо
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true; // Успех удаления пользователя
                    }
                    catch (SqlException ex)
                    {
                        // Логирование ошибки можно добавить здесь
                        return false; // Не удалось удалить пользователя
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            Hide();
            form.ShowDialog();
        }
    }
}
