using System.Linq;
using System.Windows;

namespace TaskScheduler
{
    public partial class ChangeStatusWindow : Window
    {
        public TaskItem Task { get; set; } // Получаем задачу для изменения

        public ChangeStatusWindow(TaskItem task)
        {
            InitializeComponent();
            Task = task;
            StatusComboBox.SelectedItem = Task.Status; // Изначально показываем текущий статус задачи
        }

        // Обработчик изменения статуса
        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedStatus = (StatusComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            if (!string.IsNullOrEmpty(selectedStatus))
            {
                // Изменяем статус задачи
                Task.Status = selectedStatus;

                // Закрываем окно, сообщая о том, что изменения были внесены
                DialogResult = true;
                Close();
            }
        }
    }

}
