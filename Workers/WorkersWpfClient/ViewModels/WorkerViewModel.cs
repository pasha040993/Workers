using System;

namespace WorkersWpfClient.ViewModels
{
    public class WorkerViewModel : ViewModelBase
    {
        private Guid _id;
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private DateTime _birthday;
        private bool _sex;
        private bool _haveChildren;

        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        public string MiddleName
        {
            get => _middleName;
            set => SetProperty(ref _middleName, value);
        }
        public DateTime Birthday
        {
            get => _birthday;
            set => SetProperty(ref _birthday, value);
        }

        public bool Sex
        {
            get => _sex;
            set
            {
                SetProperty(ref _sex, value);
                OnPropertyChanged(nameof(SexName));
            }
        }

        public bool HaveChildren
        {
            get => _haveChildren;
            set => SetProperty(ref _haveChildren, value);
        }

        public string SexName => Sex ? "муж." : "жен.";

        public void CopyProperties(WorkerViewModel other)
        {
            Id = other.Id;
            LastName = other.LastName;
            FirstName = other.FirstName;
            MiddleName = other.MiddleName;
            Birthday = other.Birthday;
            Sex = other.Sex;
            HaveChildren = other.HaveChildren;
        }
    }
}
