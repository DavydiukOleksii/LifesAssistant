using System;
using System.ComponentModel;

namespace LifesAssistant.ViewModel
{
    //class for communication between View and ViewModel
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region ElementChanged
        public event PropertyChangedEventHandler PropertyChanged;

        //if you want to work with different flow, each will generate different events that they did not use a collective resource
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        #endregion

        #region FreeData

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
        #endregion
    }
}
