using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using RapidProtoCore.Core.Architecture.Attributes;
using RapidProtoCore.Core.Utility;
using RapidProtoCore.Interfaces;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility;

namespace RapidProtoCore.Core.Architecture.Modularity
{
    [InheritedExport(typeof(IModule))]
    public abstract class AbstractModule : IModule, INotifyPropertyChanged
    {
        public abstract string Description { get; }

        [ConfigField]
        public int Iterations { get; set; } = 1;

        [ConfigField]
        public string Timing { get; set; } = "0";

        [ListViewField]
        public ObservableCollection<string> ObservableLog { get; set; } = new ObservableCollection<string>();

        public void LogMessage(string s)
        {
            Action<string> addMethod = ObservableLog.Add;
            Application.Current.Dispatcher.BeginInvoke(addMethod, s);
        }

        public void LogList<T>(IEnumerable<T> s)
        {
            Action<string> addMethod = ObservableLog.Add;
            Application.Current.Dispatcher.BeginInvoke(addMethod, string.Join(" ",s.Select(x => x.ToString())));
        }

        public void ClearMessage()
        {
            ObservableLog.Clear();
        }

        public override string ToString()
        {
            return this.Description + " (" + this.GetShortTypeString()+ ")";
        }

        // Fody Magic Happens Here
        public event PropertyChangedEventHandler PropertyChanged;

        // [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
