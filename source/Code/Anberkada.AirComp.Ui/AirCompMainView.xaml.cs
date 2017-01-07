
namespace Anberkada.AirComp.Ui
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for AirCompMainView.xaml
    /// </summary>
    public partial class AirCompMainView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirCompMainView"/> class.
        /// </summary>
        public AirCompMainView()
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
