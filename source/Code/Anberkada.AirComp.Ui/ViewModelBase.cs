
namespace Anberkada.AirComp.Ui
{
    using System.ComponentModel;

    /// <summary>
    /// Base class for view models.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        protected void RaisePropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
