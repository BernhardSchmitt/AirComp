
namespace Anberkada.AirComp
{
    using System;
    using System.Diagnostics;
    using System.Windows;

    using Comp.MidiAdapter.Contracts;
    
    using Biz;
    using Ui;
    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private MidiOutModelBinding _midiOutModelBinding;
        private IMidiOutAdapter _midiOutAdapter;
        private AirCompModel _model;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var config = LoadConfig();
            _midiOutAdapter = MidiOutAdapterFactory.GetInstance();
            _model = SetupModel(config, _midiOutAdapter);
            _midiOutModelBinding = SetupMidiOutBinding(_model, _midiOutAdapter);
            _model.InitOutDevice();
            ShowUi(_model);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Exit" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            if (_midiOutModelBinding != null)
            {
                _midiOutModelBinding.Dispose();
            }

            if (_midiOutAdapter != null)
            {
                _midiOutAdapter.Dispose();
            }

            if (_model != null)
            {
                _model.Dispose();
            }
            
            base.OnExit(e);
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <returns>The configuration.</returns>
        private CompConfig LoadConfig()
        {
            return new CompConfig();
        }

        /// <summary>
        /// Setups the business model.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="adapter">The MIDI out adapter.</param>
        /// <returns>
        /// The created model.
        /// </returns>
        private static AirCompModel SetupModel(CompConfig config, IMidiOutAdapter adapter)
        {
            var model = new AirCompModel(config, adapter.GetMidiOutDevices(), adapter.GetMidiOutChannels());
            try
            {
                // Without connected leap motion we recieve an exception here - move on
                model.InitializeControllerDevice();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            return model;
        }

        /// <summary>
        /// Setups the binding of the MIDI out adapter to the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="adapter">The adapter.</param>
        /// <returns>
        /// The created adapter.
        /// </returns>
        private static MidiOutModelBinding SetupMidiOutBinding(AirCompModel model, IMidiOutAdapter adapter)
        {
            return new MidiOutModelBinding(model, adapter);
        }

        /// <summary>
        /// Shows the UI.
        /// </summary>
        /// <param name="model">The model.</param>
        private void ShowUi(AirCompModel model)
        {
            // Wire up MVVM
            var viewModel = new AirCompViewModel();
            viewModel.Model = model;
            MainWindow = new MainWindow();
            var view = (MainWindow)MainWindow;
            view.ViewModel = viewModel;

            // Show up!
            MainWindow.Show();
        }
    }
}
