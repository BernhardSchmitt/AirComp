
namespace Anberkada.AirComp.Ui
{
    /// <summary>
    /// Interaction logic for AirCompConfigView.xaml
    /// </summary>
    public partial class AirCompConfigView
    {
        public AirCompConfigView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public AirCompViewModel ViewModel
        {
            get
            {
                return DataContext as AirCompViewModel;
            }

            set
            {
                DataContext = value;
            }
        }
    }
}
