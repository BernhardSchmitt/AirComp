
namespace Anberkada.AirComp
{
    using Ui;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
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
