using System;
using System.ComponentModel;

public class TaskItem : INotifyPropertyChanged
{
    private string _title;
    private string _description;
    private DateTime _dueDate;
    private int _priority;
    private string _status;

    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title)); // Уведомляем интерфейс о изменении
        }
    }

    public string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description)); // Уведомляем интерфейс о изменении
        }
    }

    public DateTime DueDate
    {
        get { return _dueDate; }
        set
        {
            _dueDate = value;
            OnPropertyChanged(nameof(DueDate)); // Уведомляем интерфейс о изменении
        }
    }

    public int Priority
    {
        get { return _priority; }
        set
        {
            _priority = value;
            OnPropertyChanged(nameof(Priority)); // Уведомляем интерфейс о изменении
        }
    }

    public string Status
    {
        get { return _status; }
        set
        {
            _status = value;
            OnPropertyChanged(nameof(Status)); // Уведомляем интерфейс о изменении
        }
    }

    // Реализация INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public TaskItem(string title, string description, DateTime dueDate, int priority, string status = "Запланировано")
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        Status = status;
    }
}
