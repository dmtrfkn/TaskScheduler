using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TaskScheduler
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Tasks = new ObservableCollection<TaskItem>();
            this.DataContext = this;

            // Загрузка задач при старте
            // LoadTasks();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var addTaskWindow = new AddTaskWindow();
            if (addTaskWindow.ShowDialog() == true)
            {
                Tasks.Add(addTaskWindow.Task);
            }
        }


        private void SaveTasksButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохранение задач в файл
            SaveTasks();
        }
        private void LoadTasksButton_Click(object sender, RoutedEventArgs e)
        {
            
            LoadTasks();
        }

        private void OnStatusFilterChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedStatus = (StatusComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();

            if (selectedStatus == "Все")
                TasksDataGrid.ItemsSource = Tasks;
            else
                TasksDataGrid.ItemsSource = Tasks.Where(t => t.Status == selectedStatus).ToList();
        }

        private void OnDateFilterChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedDate = DateFilterPicker.SelectedDate;

            if (selectedDate.HasValue)
            {
                // Фильтрация по выбранной дате
                var filteredTasks = Tasks.Where(t => t.DueDate.Date == selectedDate.Value.Date).ToList();
                TasksDataGrid.ItemsSource = filteredTasks;
            }
            else
            {
                // Если дата не выбрана, показываем все задачи
                TasksDataGrid.ItemsSource = Tasks;
            }
        }

        private void OnDateSortChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedSortOrder = (DateSortComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();

            if (selectedSortOrder == "Сначала старые")
            {
                // Сортировка по дате: сначала старые
                var sortedTasks = Tasks.OrderBy(t => t.DueDate).ToList();
                TasksDataGrid.ItemsSource = sortedTasks;
            }
            else if (selectedSortOrder == "Сначала новые")
            {
                // Сортировка по дате: сначала новые
                var sortedTasks = Tasks.OrderByDescending(t => t.DueDate).ToList();
                TasksDataGrid.ItemsSource = sortedTasks;
            }
        }
        private void TasksDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedTask = TasksDataGrid.SelectedItem as TaskItem;
            if (selectedTask != null)
            {
                // Открываем окно изменения статуса, передаем ссылку на задачу
                var changeStatusWindow = new ChangeStatusWindow(selectedTask);
                if (changeStatusWindow.ShowDialog() == true)
                {
                    // После того как окно закрыто, изменения статуса уже обновили объект задачи.
                    // Коллекция Tasks обновится автоматически, если TaskItem реализует INotifyPropertyChanged.
                }
            }
        }
        private void OnPrioritySortChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedSortOrder = (PrioritySortComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();

            if (selectedSortOrder == "Сначала высокий")
            {
                // Сортировка по приоритету: сначала высокий
                var sortedTasks = Tasks.OrderBy(t => t.Priority).ToList();
                TasksDataGrid.ItemsSource = sortedTasks;
            }
            else if (selectedSortOrder == "Сначала низкий")
            {
                // Сортировка по приоритету: сначала низкий
                var sortedTasks = Tasks.OrderByDescending(t => t.Priority).ToList();
                TasksDataGrid.ItemsSource = sortedTasks;
            }
        }



        private void SaveTasks()
        {
            string filePath = "tasks.json";

            // Сериализация задач в формат JSON
            var json = JsonConvert.SerializeObject(Tasks, Formatting.Indented);

            // Запись JSON в файл
            File.WriteAllText(filePath, json);

            MessageBox.Show("Задачи успешно сохранены.");
        }

        private void LoadTasks()
        {
            string filePath = "tasks.json";
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var loadedTasks = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(json);
                if (loadedTasks != null)
                {
                    Tasks.Clear();
                    foreach (var task in loadedTasks)
                    {
                        Tasks.Add(task);
                    }
                }
            }
        }

    }
}
