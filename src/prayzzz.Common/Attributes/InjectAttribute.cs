using prayzzz.Common.Enums;
using System;

namespace prayzzz.Common.Attributes
{
    /// <summary>
    ///     Indicates that the marked class should be injected into the DependencyInjection-Container.
    ///     Uses <see cref="DependencyLifetime.Scoped" /> as default lifetime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAttribute : Attribute
    {
        /// <summary>
        ///     Uses <see cref="DependencyLifetime.Scoped" /> as lifetime.
        /// </summary>
        public InjectAttribute(params Type[] serviceTypes)
        {
            AutoActivate = false;
            Lifetime = DependencyLifetime.Scoped;
            ServiceTypes = serviceTypes;
        }

        public InjectAttribute(DependencyLifetime lifetime, params Type[] serviceTypes)
        {
            AutoActivate = false;
            Lifetime = lifetime;
            ServiceTypes = serviceTypes;
        }

        public bool AutoActivate { get; set; }

        public DependencyLifetime Lifetime { get; }

        public Type[] ServiceTypes { get; }
    }
}