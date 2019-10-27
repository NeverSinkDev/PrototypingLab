using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using RapidProtoCore.Interfaces;

namespace RapidProtoCore.Core.Architecture.ViewGeneration
{
    /// <summary>
    /// Describes the loading strategy for a single modue
    /// This basically binds a Module to a set of functional rules
    /// and renders those unto a window
    /// </summary>
    public abstract class AbstractGenerationStrategy
    {
        protected AbstractGenerationStrategy(IModule module, Window window)
        {
            this.Screen = window;
            this.Module = module;
            this.Init();
        }

        public void Init()
        {
            this.CreateMemberInfoCatalog();
            this.CreateConventionCatalog();
        }

        public Window Screen { get; set; }

        public List<Dictionary<string, List<MemberInfo>>> MemberInfoCatalog = new List<Dictionary<string, List<MemberInfo>>>();

        public List<ElementGenerationConvention> Conventions = new List<ElementGenerationConvention>();

        public IModule Module { get; set; }

        public void Execute()
        {
            // For each TYPE of MemberInfo (property, method...) that we gather
            foreach (var elementDictionary in this.MemberInfoCatalog)
            {
                // For each Primary Attribute TYPE
                foreach (var elementList in elementDictionary)
                {
                    // For each Attribute
                    foreach (var element in elementList.Value)
                    {
                        // Try out conventions
                        foreach (var convention in this.Conventions)
                        {
                            // Until one matches
                            if (convention.Execute(element, elementList.Key, this.Module, null))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        public abstract void CreateMemberInfoCatalog();

        public abstract void CreateConventionCatalog();
    }
}
