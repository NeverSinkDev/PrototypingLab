using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using RapidProtoCore.Core.Architecture.Modularity;
using RapidProtoCore.Core.Utility.Extensions;

namespace CommonModules.ViewGeneration
{
    public static class ElementCreationLibrary
    {
        public static UIElement CreateSimpleButton(MemberInfo methodToLink, object bindingObject, Dictionary<string, object> options = null)
        {
            var e = new System.Windows.Controls.Button();

            Action del = (Action)Delegate.CreateDelegate(typeof(Action), bindingObject, methodToLink as MethodInfo);

            e.Click += (o, i) => del.Invoke();
            e.Content = methodToLink.Name;
            return e;
        }

        public static UIElement CreateTimedButton(MemberInfo methodToLink, object bindingObject, Dictionary<string, object> options = null)
        {
            var e = new System.Windows.Controls.Button();
            AbstractModule m = options["currentModule"] as AbstractModule;
            Action del = (Action)Delegate.CreateDelegate(typeof(Action), bindingObject, methodToLink as MethodInfo);

            e.Click += (o, i) => TimedAction(del, m, m.Iterations).Invoke();
            e.Content = methodToLink.Name;
            return e;

            // Lokale Methode: "Wrappt" die zu ausführende Methode mit einer Stopwatch und erlaubt es X iterationen durchzuführen
            Action TimedAction(Action a, AbstractModule target, object iter)
            {
                var s = new Stopwatch();
                return new Action(() => s.Time(() => a(), iter, target));
            }
        }

        public static UIElement CreateTimedButtonAsync(MemberInfo methodToLink, object bindingObject, Dictionary<string, object> options = null)
        {
            var e = new System.Windows.Controls.Button();
            AbstractModule m = options["currentModule"] as AbstractModule;
            var del = (Func<Task>)Delegate.CreateDelegate(typeof(Func<Task>), bindingObject, methodToLink as MethodInfo);

            e.Click += async (o, i) => await TimedAction(del, m, m.Iterations).Invoke();
            e.Content = methodToLink.Name;
            return e;

            // Lokale Methode: "Wrappt" die zu ausführende Methode mit einer Stopwatch und erlaubt es X iterationen durchzuführen
            Func<Task> TimedAction(Func<Task> a, AbstractModule target, object iter)
            {
                var s = new Stopwatch();
                return new Func<Task>(async () => await s.TimeAsync(() => a(), iter, target));
            }
        }

        public static UIElement CreateEditTextField(MemberInfo propToLink, object bindingObject, Dictionary<string, object> options = null)
        {
            // Erstelle Grid für Label + Textbox
            var stack = new Grid();
            stack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });
            stack.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(500) });

            // Erstelle Label
            var l = new Label() { Content = $"{propToLink.Name}" };
            l.BorderBrush = new SolidColorBrush(Colors.Black);

            // Erstelle Textbox
            var e = new System.Windows.Controls.TextBox();

            // Setze Binding
            Binding myBinding = new Binding() { Path = new PropertyPath(propToLink.Name), Source = bindingObject, Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(e, System.Windows.Controls.TextBox.TextProperty, myBinding);

            stack.Children.Add(l);
            stack.Children.Add(e);
            Grid.SetColumn(e, 1);

            var color = options?["color"] as SolidColorBrush ?? new SolidColorBrush(Colors.Gray);

            stack.Background = color;
            return stack;
        }

        public static UIElement CreateTallEditTextField(MemberInfo propToLink, object bindingObject, Dictionary<string, object> options = null)
        {
            var e = new System.Windows.Controls.TextBox {MaxHeight = 300 };
            ScrollViewer.SetVerticalScrollBarVisibility(e, ScrollBarVisibility.Visible);
            Binding myBinding = new Binding() { Path = new PropertyPath(propToLink.Name), Source = bindingObject, Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(e, System.Windows.Controls.TextBox.TextProperty, myBinding);
            return e;
        }

        public static UIElement CreateListViewer(MemberInfo propToLink, object bindingObject, Dictionary<string, object> options = null)
        {
            var e = new System.Windows.Controls.ListBox() { MaxHeight = 300 };
            ScrollViewer.SetVerticalScrollBarVisibility(e, ScrollBarVisibility.Visible);
            Binding myBinding = new Binding() { Path = new PropertyPath(propToLink.Name), Source = bindingObject, Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(e, System.Windows.Controls.ListBox.ItemsSourceProperty, myBinding);
            return e;
        }
    }
}
