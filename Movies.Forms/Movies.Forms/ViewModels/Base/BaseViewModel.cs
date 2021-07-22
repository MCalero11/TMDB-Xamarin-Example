using Movies.Forms.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Forms.ViewModels.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields
        protected readonly IDataService _dataService;
        protected bool _canRefresh = true;
        bool _isBusy = false;
        bool _isEmpty = false;
        string _title = string.Empty;
        #endregion
        #region Properties
        /// <summary>
        /// Indicates a lazy action in progress
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        /// <summary>
        /// Indicates when a data is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { SetProperty(ref _isEmpty, value); }
        }
        /// <summary>
        /// Bind the page title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion
        #region Commands
        /// <summary>
        /// Pull to refresh action
        /// </summary>
        public ICommand RefreshCommand => new Command(async () => await refreshData(), canRefresh);
        #endregion

        private bool canRefresh()
        {
            return _canRefresh;
        }
        protected abstract Task refreshData();
        public BaseViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion
    }
}
