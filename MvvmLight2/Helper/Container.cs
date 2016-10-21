using System;
using Microsoft.Practices.Unity;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Контейнер внедрения зависимотсей на базе Unity
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public sealed class Container: IContainer
    {
        private readonly UnityContainer unity = new UnityContainer();

        static Container() { }

        private static Container instance;
        public static Container Instance
        {
            get
            {
                return instance ?? (instance = new Container());
            }
        }

        public T Resolve<T>()
        {
            return this.unity.Resolve<T>();
        }

        public void RegisterAsSingleton<TInterface, TImplementor>()
            where TImplementor : TInterface
        {
            this.unity.RegisterType<TInterface, TImplementor>(
               new ContainerControlledLifetimeManager());
        }

       
        public void RegisterInstance(Type type, object instance)
        {
            this.unity.RegisterInstance(type, instance);
        }

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool p)
        {
            throw new NotImplementedException();
        }

    }
}
