using System;
using System.IO;
using System.Configuration;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace HHOnline.Framework
{
    /// <summary>
    /// 对IUnityContainer进行封装
    /// </summary>
    public class HHContainer
    {
        #region Cntor
        private static IUnityContainer container;

        static HHContainer()
        {
            container = new UnityContainer();

            ExeConfigurationFileMap map = new ExeConfigurationFileMap();


            string path = GlobalSettings.MapPath("~/Unity.config");

            map.ExeConfigFilename = path;

            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            UnityConfigurationSection section = (UnityConfigurationSection)config.GetSection("unity");

            section.Containers.Default.Configure(container);
        }

        private HHContainer()
        {
        }

        public static HHContainer Create()
        {
            container.RegisterInstance<HHContainer>(new HHContainer(), new ContainerControlledLifetimeManager());
            return container.Resolve<HHContainer>();
        }

        public IUnityContainer InnerContainer
        {
            get
            {
                return container;
            }
        }
        #endregion

        #region Register
        public HHContainer RegisterInstance(Type t, object instance)
        {
            container.RegisterInstance(t, instance);
            return HHContainer.Create();
        }

        public HHContainer RegisterType(Type t)
        {
            container.RegisterType(t);
            return HHContainer.Create();
        }

        public HHContainer RegisterTypeSingleton(Type t)
        {
            container.RegisterType(t, new ContainerControlledLifetimeManager());
            return HHContainer.Create();
        }

        public HHContainer RegisterInstanceSingleton(Type t, object instance)
        {
            container.RegisterInstance(t, instance, new ContainerControlledLifetimeManager());
            return HHContainer.Create();
        }

        public HHContainer RegisterInstance<T>(T instance)
        {
            container.RegisterInstance<T>(instance);
            return HHContainer.Create();
        }

        public HHContainer RegisterType<T>()
        {
            container.RegisterType<T>();
            return HHContainer.Create();
        }

        public HHContainer RegisterTypeSingleton<T>()
        {
            container.RegisterType<T>(new ContainerControlledLifetimeManager());
            return HHContainer.Create();
        }

        public HHContainer RegisterInstanceSingleton<T>(T instance)
        {
            container.RegisterInstance<T>(instance, new ContainerControlledLifetimeManager());
            return HHContainer.Create();
        }
        #endregion

        #region Resolve
        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return container.Resolve<T>(name);
        }

        public object Resolve(Type t)
        {
            return container.Resolve(t);
        }

        public object Resolve(Type t, string name)
        {
            return container.Resolve(t, name);
        }
        #endregion

    }
}
