using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MefLab.GenerationStrategy;
using RapidProtoCore.Interfaces;

namespace MefLab
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private IModule selectedMod;

        [ImportMany(typeof(IModule))]
        public IEnumerable<IModule> AvailableModules { get; set; }

        public IEnumerable<IModule> ModuleSelectSubSet
        {
            get
            {
                if (Keyboard.IsKeyDown(Key.D0))
                {
                    return this.AvailableModules.Where(x => x.ToString().Contains("PE00")).OrderBy(x => x.ToString()).ToList();
                }
                else if (Keyboard.IsKeyDown(Key.D1))
                {
                    return this.AvailableModules.Where(x => x.ToString().Contains("PE01")).OrderBy(x => x.ToString()).ToList();
                }
                else if (Keyboard.IsKeyDown(Key.D2))
                {
                    return this.AvailableModules.Where(x => x.ToString().Contains("PE02")).OrderBy(x => x.ToString()).ToList();
                }

                return this.AvailableModules.Where(x => !x.ToString().Contains("PE")).ToList();
            }
        }

        /// <summary>
        /// Das zur Zeit geladene Modul
        /// </summary>
        public IModule SelectedMod
        {
            get
            {
                return this.selectedMod;
            }

            set
            {
                if (this.selectedMod != value)
                {
                    this.selectedMod = value;

                    if (value != null)
                    {
                        this.ClearValue();
                        DefaultGenerationStrategy generationStrategy = new DefaultGenerationStrategy(value, this);
                        generationStrategy.Execute();
                    }
                }
            }
        }

        public MainWindow()
        {
            // WPF
            InitializeComponent();
            this.DataContext = this;
            
            // MEF
            this.MefInit();
        }

        private void MefInit()
        {
            //var catalog = new DirectoryCatalog(".");
            var catalog = new AssemblyCatalog(System.Reflection.Assembly.GetCallingAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        private void ClearValue()
        {
            this.ButtonStackPanel.Children.Clear();
            this.InputFieldStackPanel.Children.Clear();
            this.OutputFieldStackPanel.Children.Clear();
            this.ConfigurationStackPanel.Children.Clear();
        }

        private void ProjectSource_KeyDown(object sender, KeyEventArgs e)
        {
            this.ProjectSource.ItemsSource = ModuleSelectSubSet;
            e.Handled = true;
        }
    }
}
