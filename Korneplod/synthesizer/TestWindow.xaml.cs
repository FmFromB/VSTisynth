using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace synthesizer
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : UserControl
    {
        private readonly MainWindowViewModel _viewModel;
        private static List<Slider> Sliders = new List<Slider> { };
        private List<CheckBox> Checks = new List<CheckBox> { };
        private List<RadioButton> RBs = new List<RadioButton> { };
        private ControlsEnumerator controlFinder;


        public TestWindow()
        {
            InitializeComponent();

            foreach (Slider slider in ControlsEnumerator.FindVisualChildren<Slider>(MainGrid))
            {
                Sliders.Add(slider);
            }
            foreach (CheckBox checkbox in ControlsEnumerator.FindVisualChildren<CheckBox>(MainGrid))
            {
                Checks.Add(checkbox);
            }
            foreach (RadioButton rb in ControlsEnumerator.FindVisualChildren<RadioButton>(MainGrid))
            {
                RBs.Add(rb);
            }

            _viewModel = new MainWindowViewModel(Dispatcher);
            DataContext = _viewModel;
            _viewModel.EditorView = this;

        }

        private void SetOctave(object sender, RoutedEventArgs e)
        {
            var octaveSel = sender as RadioButton;

            if (_viewModel != null)
            {
                _viewModel.BaseFrequency = 110 * Math.Pow(2, Convert.ToDouble(octaveSel.Name.Substring(1, 1)) - 1);
            }
        }

        private void SetWaveForm(object sender, RoutedEventArgs e)
        {
            void SetWave(SignalGeneratorType type, string name)
            {
                switch (name.Substring(name.Length - 1, 1))
                {
                    default:
                        _viewModel.Wavetype = type;
                        break;
                    case "2":
                        _viewModel.Wavetype2 = type;
                        break;
                    case "3":
                        _viewModel.Wavetype3 = type;
                        break;
                }
            }
            RadioButton WaveSel = sender as RadioButton;
            string wavename = WaveSel.Name.Substring(0, 2);

            if (_viewModel != null)
            {
                foreach (SignalGeneratorType stype in Enum.GetValues(typeof(SignalGeneratorType)))
                {
                    if (wavename == stype.ToString().Substring(0, 2)) SetWave(stype, WaveSel.Name);
                }


            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            _viewModel.KeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            _viewModel.KeyUp(e);
        }

        private void LowPassFilter_checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableLPF = true;
            }
        }

        private void LowPassFilter_unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableLPF = false;
            }
        }
        private void SubOscillator_checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableSubOsc = true;
            }
        }

        private void SubOscillator_unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableSubOsc = false;
            }
        }
        private void Vibrato_checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableVibrato = true;
            }
        }

        private void Vibrato_unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.EnableVibrato = false;
            }
        }
        private void Tremolo_checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.TremoloGain = 0.2f;
            }
        }

        private void Tremolo_unchecked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.TremoloGain = 0.0f;
            }
        }

        private void Octave2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selected = comboBox.SelectedIndex;

            if (_viewModel != null)
            {
                _viewModel.Voice2Octave = selected * 12;
            }
        }

        /// <summary>
        /// Sets all values chosen sound preset uses
        /// </summary>
        /// <param name="type1">Osc 1 wave type</param>
        /// <param name="type2">Osc 2 wave type</param>
        /// <param name="type3">Osc 3 wave type</param>
        /// <param name="A">Octave osc 1</param>
        /// <param name="octaveIndex1">Octave osc 2</param>
        /// <param name="octaveIndex2">Octave osc 3</param>
        /// <param name="semitone1">Semi osc 2</param>
        /// <param name="semitone2">Semi osc 3</param>
        /// <param name="ConfigParams">Sliders parameters</param>
        /// <param name="ConfigRBs">Turned on RadioButtons</param>
       /* void SetPreset(RadioButton type1, RadioButton type2, RadioButton type3, RadioButton A,
                byte octaveIndex1, byte octaveIndex2, byte semitone1, byte semitone2, double[] ConfigParams, bool[] ConfigRBs)
        {
            A.IsChecked = true;
            type1.IsChecked = type2.IsChecked = type3.IsChecked = A.IsChecked = true;
            Octave.SelectedIndex = octaveIndex1;
            Octave1.SelectedIndex = octaveIndex2;
            Semitone.SelectedIndex = semitone1;
            Semitone1.SelectedIndex = semitone2;
            if (Sliders != null)
                for (byte i = 0; i < Sliders.Count; i++)
                {
                    Sliders[i].Value = ConfigParams[i];
                };

            if (Checks != null)
                for (byte i = 0; i < Checks.Count; i++)
                {
                    Checks[i].IsChecked = ConfigRBs[i];
                }
        }*/

        /// <summary>
        /// List of available sound presets to settle up
        /// </summary>
        /// <param name="sender">Number of preset in the list</param>
        /// <param name="e"></param>
      /*  private void Presets(object sender, SelectionChangedEventArgs e)
        {
            double[] Config = new double[] { };
            bool[] Conf = new bool[] { };
            var comboBox = sender as ComboBox;
            var selected = comboBox.SelectedIndex;
            switch (selected)
            {
                case 0:
                    Config = new double[] { 0.25, 0.01, 0.01, 1.0, 0.02, 20000, 0.7, 0.5, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.7, // 1
                                            0, 0.01, 0.01, 1.0, 0.02, // 2
                                            0, 0.01, 0.01, 1.0, 0.02  // 3
                                           };
                    Conf = new bool[] { false, false, false, false };
                    SetPreset(Sine, Sine2, Sine3, A4, 0, 0, 0, 0, Config, Conf);
                    break;
                case 1:
                    Config = new double[] { 0.25, 0.01, 0.0, 1.0, 5.0, 6235, 0.18, 0.65, 0.88, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.7, // 1
                                            0, 0.01, 0.01, 1.0, 0.02, // 2
                                            0, 0.01, 0.01, 1.0, 0.02  // 3
                                           };
                    Conf = new bool[] { true, false, false, true };
                    SetPreset(Sine, Sine2, Sine3, A4, 0, 0, 0, 0, Config, Conf);
                    break;
                case 2:
                    Config = new double[] { 0.65, 5.0, 0.01, 1.0, 5.0, 2500, 0.3, 0.5, 0.0, 1.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.7, // 1
                                            0.05, 5.0, 0.01, 1.0, 5.0, // 2
                                            0, 0.01, 0.01, 1.0, 0.02   // 3
                                           };
                    Conf = new bool[] { true, false, false, false };
                    SetPreset(Saw, Whitenoise2, Sine3, A3, 0, 0, 0, 0, Config, Conf);
                    break;
                case 3:
                    Config = new double[] { 0.43, 0.01, 1.6, 0.56, 1.0, 20000, 0.32, 10, 1.0, 0.0, 0.0, 0.0, 0.77, 0.55, 0.42, 0.71, 0.58, 0.3, 0.7, // 1
                                            0, 0.01, 0.01, 1.0, 0.02, // 2
                                            0, 0.01, 0.01, 1.0, 0.02  // 3
                                           };
                    Conf = new bool[] { false, false, true, true };
                    SetPreset(Square, Sine2, Sine3, A3, 0, 0, 0, 0, Config, Conf);
                    break;
                case 4:
                    Config = new double[] { 0.62, 0.01, 0.01, 1.0, 3.4, 20000, 1, 0.5, 0.0, 0.15, 0.0, 0.0, 1, 0.0, 0.0, 0.0, 0.0, 0.0, 0.7, // 1
                                            0, 0.01, 0.01, 1.0, 0.02, // 2
                                            0, 0.01, 0.01, 1.0, 0.02  // 3
                                           };
                    Conf = new bool[] { false, true, false, false };
                    SetPreset(Saw, Sine2, Sine3, A3, 0, 0, 0, 0, Config, Conf);
                    break;
                case 5:
                    Config = new double[] { 0.57, 0.01, 1.288, 0.43, 0.74, 20000, 0.7, 0.5, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.7, // 1
                                            0.42, 0.01, 1.288, 0.43, 0.74, // 2
                                            0.42, 0.01, 1.288, 0.43, 0.74  // 3
                                           };
                    Conf = new bool[] { false, false, false, false };
                    SetPreset(Sine, Sine2, Sine3, A2, 1, 4, 0, 7, Config, Conf);
                    break;
                case 6:
                    Config = new double[] { 0.25, 0.01, 1.65, 0.45, 0.553, 20000, 0.7, 0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.7, // 1
                                            0, 0.01, 0.01, 1.0, 0.02, // 2
                                            0, 0.01, 0.01, 1.0, 0.02  // 3
                                           };
                    Conf = new bool[] { false, false, false, false };
                    SetPreset(Pinknoise, Sine2, Sine3, A4, 1, 4, 0, 7, Config, Conf);
                    break;
            }

        }*/

        private void Octave3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selected = comboBox.SelectedIndex;

            if (_viewModel != null)
            {
                _viewModel.Voice3Octave = selected * 12;
            }
        }

        private void Semitone2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selected = comboBox.SelectedIndex;

            if (_viewModel != null)
            {
                _viewModel.Voice2Semi = selected;
            }
        }

        private void Semitone3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selected = comboBox.SelectedIndex;

            if (_viewModel != null)
            {
                _viewModel.Voice3Semi = selected;
            }
        }
    }
}

