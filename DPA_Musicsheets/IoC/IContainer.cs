using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.IoC
{
    public interface IContainer : IDisposable
    {
        void RegisterTransient<TImplementation>(Func<IContainer, TImplementation> factory)
            where TImplementation : class;

        void RegisterTransient<TAbstract, TImplementation>(Func<IContainer, TImplementation> factory)
            where TImplementation : class, TAbstract
            where TAbstract : class;

        TAny ResolveTransient<TAny>()
            where TAny : class;

        void RegisterSingleton<TImplementation>(TImplementation instance, bool disposable = false)
            where TImplementation : class;

        void RegisterSingleton<TImplementation>(Func<IContainer, TImplementation> factory, bool disposable = false)
            where TImplementation : class;

        void RegisterSingleton<TAbstract, TImplementation>(TImplementation instance, bool disposable = false)
            where TImplementation : class, TAbstract
            where TAbstract : class;

        void RegisterSingleton<TAbstract, TImplementation>(Func<IContainer, TImplementation> factory, bool disposable = false)
            where TImplementation : class, TAbstract
            where TAbstract : class;

        TAny ResolveSingleton<TAny>()
            where TAny : class;

        TAny Resolve<TAny>()
            where TAny : class;
    }
}
