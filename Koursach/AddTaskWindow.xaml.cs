using System;
using System.Windows;

namespace TaskScheduler
{
    public partial class AddTaskWindow : Window
    {
        // Свойство для возвращаемой задачи
        public TaskItem Task { get; set; }

        public AddTaskWindow()
        {
            InitializeComponent();
        }

        // Обработчик кнопки "Сохранить"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные с формы
            var title = TitleTextBox.Text;
            var description = DescriptionTextBox.Text;
            var dueDate = DueDatePicker.SelectedDate ?? DateTime.Now; // Если дата не выбрана, ставим текущую
            var priority = (PriorityComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();

            // Определяем приоритет
            int priorityValue;
            switch (priority)
            {
                case "1 (Высокий)":
                    priorityValue = 1;
                    break;
                case "2 (Средний)":
                    priorityValue = 2;
                    break;
                case "3 (Низкий)":
                    priorityValue = 3;
                    break;
                default:
                    priorityValue = 3; // В случае, если приоритет не выбран, ставим низкий
                    break;
            }

            // Создаем новую задачу
            Task = new TaskItem(title, description, dueDate, priorityValue);

            // Закрываем окно и возвращаем результат
            DialogResult = true;
            Close();
        }
    }
}
