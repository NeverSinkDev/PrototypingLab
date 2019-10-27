using System;
using System.Windows;
using System.Windows.Controls;

namespace CommonModules.ViewGeneration
{
    public static class ElementSpawnLibrary
    {
        public static Action<UIElement, UIElementCollection> PushTo(UIElementCollection target)
        {
            return new Action<UIElement, UIElementCollection>((s, v) => target.Add(s));
        }
    }
}
