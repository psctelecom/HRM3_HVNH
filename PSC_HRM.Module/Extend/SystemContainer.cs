using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace PSC_HRM.Module
{
    public sealed class SystemContainer
    {
        private static volatile UnityContainer instance;
        private static object syncRoot = new Object();

        private SystemContainer() { }

        public static UnityContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new UnityContainer();
                    }
                }
                return instance;
            }
        }

        public static void Install()
        {
            var type = typeof(IRegister);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var item in types)
            {
                if (item == typeof(IRegister))
                    continue;
                IRegister instance = Activator.CreateInstance(item) as IRegister;
                if (instance != null)
                    instance.Register();
            }
        }

        public static T Resolver<T>() where T : class
        {
            return Instance.Resolve<T>();
        }

        public static T Resolver<T>(string name) where T : class
        {
            return Instance.Resolve<T>(name);
        }
    }
}
