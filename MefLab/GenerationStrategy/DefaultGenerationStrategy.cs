using System.Collections.Generic;
using System.Windows.Media;
using RapidProtoCore.Core.Architecture.ViewGeneration;
using RapidProtoCore.Core.Utility;
using RapidProtoCore.Interfaces;
using RapidProtoCore.Interfaces.Attributes;
using static CommonModules.ViewGeneration.ElementCreationLibrary;
using static CommonModules.ViewGeneration.ElementMatchingLibrary;
using static CommonModules.ViewGeneration.ElementSpawnLibrary;

namespace MefLab.GenerationStrategy
{
    /// <summary>
    /// Concrete Default Generation Strategy Implementation.
    /// Used to Fill 3 groups: Input, Output and Buttons
    /// </summary>
    public class DefaultGenerationStrategy : AbstractGenerationStrategy
    {
        public DefaultGenerationStrategy(IModule module, MainWindow window) : base(module, window)
        {
        }

        public override void CreateMemberInfoCatalog()
        {
            this.MemberInfoCatalog.Add(AttributeUtility.CreateAttributedPropertyDictionary<IPrimaryCategoryAttribute>(this.Module));
            this.MemberInfoCatalog.Add(AttributeUtility.CreateAttributedMethodDictionary<IPrimaryCategoryAttribute>(this.Module));
        }

        public override void CreateConventionCatalog()
        {
            // The "Timed Buttons" require the reference to the current module. We pass it in the options

            this.Conventions.Add(new ElementGenerationConvention(CreateSimpleStringAndSyncMatchFunction("ActionPerformerAttribute", true), CreateTimedButtonAsync, PushTo((Screen as MainWindow).ButtonStackPanel.Children))
            { Options = new Dictionary<string, object>() { { "currentModule", this.Module } } });

            this.Conventions.Add(new ElementGenerationConvention(CreateSimpleStringAndSyncMatchFunction("ActionPerformerAttribute", false), CreateTimedButton, PushTo((Screen as MainWindow).ButtonStackPanel.Children))
            { Options = new Dictionary<string, object>() {{ "currentModule", this.Module }}});

            // Normal conventions
            this.Conventions.Add(new ElementGenerationConvention(CreateSimpleStringMatchFunction("ConfigFieldAttribute"), CreateEditTextField, PushTo((Screen as MainWindow).ConfigurationStackPanel.Children)));

            this.Conventions.Add(new ElementGenerationConvention(CreateSimpleStringMatchFunction("ListViewFieldAttribute"), CreateListViewer, PushTo((Screen as MainWindow).OutputFieldStackPanel.Children)));

            this.Conventions.Add(new ElementGenerationConvention(CreateSimpleStringMatchFunction("InputFieldAttribute"), CreateEditTextField, PushTo((Screen as MainWindow).InputFieldStackPanel.Children))
            { Options = new Dictionary<string, object>() {{ "color", new SolidColorBrush(Colors.DarkGoldenrod) }}});

            this.Conventions.Add(new ElementGenerationConvention(CreateSimpleStringMatchFunction("OutputFieldAttribute"), CreateEditTextField, PushTo((Screen as MainWindow).OutputFieldStackPanel.Children))
            { Options = new Dictionary<string, object>() {{ "color", new SolidColorBrush(Colors.Coral) } }});

            this.Conventions.Add(new ElementGenerationConvention(CreateSimpleStringMatchFunction("MultiLineTextFieldAttribute"), CreateTallEditTextField, PushTo((Screen as MainWindow).ButtonStackPanel.Children)));
        }



    }
}
