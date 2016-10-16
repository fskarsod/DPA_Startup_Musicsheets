using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.IoC
{
    public class IoCContainer : IContainer
    {
        private static readonly IDictionary<Type, Func<IContainer, object>> AbstractTransientRegister = new Dictionary<Type, Func<IContainer, object>>();
        private static readonly IDictionary<Type, Func<IContainer, object>> ConcreteTransientRegister = new Dictionary<Type, Func<IContainer, object>>();

        private static readonly IDictionary<Type, object> AbstractSingletonRegister = new Dictionary<Type, object>();
        private static readonly IDictionary<Type, object> ConcreteSingletonRegister = new Dictionary<Type, object>();

        private static readonly IList<IDisposable> Disposables = new List<IDisposable>();

        private static IContainer _instance;
        public static IContainer Instance => _instance ?? (_instance = new IoCContainer());

        private IoCContainer()
        { }

        public void RegisterTransient<TImplementation>(Func<IContainer, TImplementation> factory)
            where TImplementation : class
        {
            ConcreteTransientRegister.Add(typeof(TImplementation), factory);
        }

        public void RegisterTransient<TAbstract, TImplementation>(Func<IContainer, TImplementation> factory)
            where TImplementation : class, TAbstract
            where TAbstract : class
        {
            RegisterTransient(factory);
            AbstractTransientRegister.Add(typeof(TAbstract), factory);
        }

        public TAny ResolveTransient<TAny>()
            where TAny : class
        {
            var factory = ResolveFromDictionary<TAny, Func<IContainer, object>>(ConcreteTransientRegister)
                ?? ResolveFromDictionary<TAny, Func<IContainer, object>>(AbstractTransientRegister);
            return factory?.Invoke(this) as TAny;
        }

        public void RegisterSingleton<TImplementation>(TImplementation instance, bool disposable = false)
            where TImplementation : class
        {
            if (disposable && instance is IDisposable)
            {
                Disposables.Add((IDisposable)instance);
            }
            ConcreteSingletonRegister.Add(typeof(TImplementation), instance);
        }

        public void RegisterSingleton<TImplementation>(Func<IContainer, TImplementation> factory, bool disposable = false)
            where TImplementation : class
        {
            RegisterSingleton(factory(this), disposable);
        }

        public void RegisterSingleton<TAbstract, TImplementation>(TImplementation instance, bool disposable = false)
            where TImplementation : class, TAbstract
            where TAbstract : class
        {
            RegisterSingleton(instance, disposable);
            AbstractSingletonRegister.Add(typeof(TAbstract), instance);
        }

        public void RegisterSingleton<TAbstract, TImplementation>(Func<IContainer, TImplementation> factory, bool disposable = false)
            where TImplementation : class, TAbstract
            where TAbstract : class
        {
            RegisterSingleton<TAbstract, TImplementation>(factory(this));
        }

        public TAny ResolveSingleton<TAny>()
            where TAny : class
        {
            var result = ResolveFromDictionary<TAny, object>(ConcreteSingletonRegister) 
                ?? ResolveFromDictionary<TAny, object>(AbstractSingletonRegister);
            return result as TAny;
        }

        public TAny Resolve<TAny>()
            where TAny : class
        {
            return ResolveSingleton<TAny>() ?? ResolveTransient<TAny>();
        }

        public void Dispose()
        {
            _instance = null;
            foreach (var disposable in Disposables)
            {
                disposable.Dispose();
            }
        }

        private static TResult ResolveFromDictionary<TRequest, TResult>(IDictionary<Type, TResult> dictionary)
            where TRequest : class
            where TResult : class
        {
            var type = typeof(TRequest);
            return dictionary.ContainsKey(type)
                ? dictionary[type]
                : null;
        }
    }
}
