using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using RapidProtoCore.Interfaces;

namespace RapidProtoCore.Core.Architecture.ViewGeneration
{
    public class ElementGenerationConvention
    {
        /// <summary>
        /// Combines a Match, a Create and a Spawn function to evaluate input and create attribute based UIElements, bind them and push them into the UI
        /// </summary>
        /// <param name="match">The Matching function</param>
        /// <param name="create">The creation function</param>
        /// <param name="spawn">The UI pushing function</param>
        public ElementGenerationConvention(Func<string, MemberInfo, bool> match, Func<MemberInfo, object, Dictionary<string,object>, UIElement> create, Action<UIElement, UIElementCollection> spawn)
        {
            this.Match = match;
            this.Create = create;
            this.Spawn = spawn;
        }

        public void DefineOptions(Dictionary<string, object> options)
        {
            this.Options = options;
        }

        public void AddAmplifier(Func<UIElement, UIElement> amplify)
        {
            this.Amplify.Add(amplify);
        }

        /// <summary>
        /// The main function. If the match function of the current strategy matches the module element:
        /// It 1) Creates the UIElement
        /// 2) Runs all amplify functions upon the element
        /// 3) Does something (such as push the element into a stackpanel) depending on the strategy definition
        /// </summary>
        /// <param name="item"></param>
        /// <param name="memberGroupType"></param>
        /// <param name="module"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Execute(MemberInfo item, string memberGroupType, IModule module, UIElementCollection target)
        {
            if (this.Match(memberGroupType, item))
            {
                var e = Create(item, module, this.Options);
                Amplify.ForEach(a => e = a(e));
                this.Spawn(e, target);
                return true;
            }

            return false;
        }

        public Func<MemberInfo, object, Dictionary<string, object>, UIElement> Create { get; set; }

        public Action<UIElement, UIElementCollection> Spawn { get; set; }

        public Func<string, MemberInfo, bool> Match { get; set; }

        public List<Func<UIElement, UIElement>> Amplify { get; set; } = new List<Func<UIElement, UIElement>>();

        public Dictionary<string, object> Options { get; set; } = null;
    }
}
