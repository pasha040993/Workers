using System;
using System.Windows;
using WorkersWpfClient.ViewModels;

namespace WorkersWpfClient.View
{
    /// <summary>
    /// Interaction logic for WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        public WorkerViewModel Worker { get; set; }

        public WorkerWindow(WorkerViewModel? editworker = null)
        {
            InitializeComponent();
            Worker = new WorkerViewModel()
            {
                Birthday = DateTime.Now
            };
            if (editworker != null)
            {
                Worker.CopyProperties(editworker);
            }

            DataContext = Worker;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Worker.FirstName))
            {
                MessageBox.Show(this, "Поле Имя не заполнено!");
                return;
            }

            if (string.IsNullOrEmpty(Worker.LastName))
            {
                MessageBox.Show(this, "Поле Фамилия не заполнено!");
                return;
            }

            if (string.IsNullOrEmpty(Worker.MiddleName))
            {
                MessageBox.Show(this, "Поле Отчество не заполнено!");
                return;
            }

            DialogResult = true;
        }
    }
}
