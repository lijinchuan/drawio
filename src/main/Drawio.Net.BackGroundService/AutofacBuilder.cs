using Autofac;
using AutoMapper;
using Drawio.Net.Service;
using Drawio.Net.Service.Impl;
using Drawio.Net.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.BackGroundService
{
    /// <summary>
    /// Autofac注入类
    /// </summary>
    public class AutofacBuilder
    {
        private static IContainer _container;
        public static void init()
        {
            ContainerBuilder builder = new ContainerBuilder();

            var accessImpleAndInterfaces = AssemblyHelper.GetImpleAndInterfaces("Drawio.Net.Data", "");
            accessImpleAndInterfaces.AddRange(AssemblyHelper.GetImpleAndInterfaces("Drawio.Net.Service", "Service"));
            foreach (var v in accessImpleAndInterfaces)
            {
                if (v.interfaces.Any(p => p == typeof(ISaveDrawFileService)))
                {
                    continue;
                }
                if (v.impl.CustomAttributes.Any(p => p.AttributeType == typeof(ObsoleteAttribute)))
                {
                    continue;
                }
                
                if (v.interfaces.Length == 0)
                {
                    continue;
                }
                if (v.impl.IsGenericType)
                {
                    builder.RegisterGeneric(v.impl).As(v.interfaces[0]).InstancePerLifetimeScope();
                }
                else
                {
                    builder.RegisterType(v.impl).As(v.interfaces[0]).InstancePerLifetimeScope();
                }
            }

            builder.RegisterType<MongoDBSaveDrawFileService>().As<ISaveDrawFileService>();

            _ = builder.Register<IConfigurationProvider>(ctx => new MapperConfiguration(cfg => cfg.AddMaps("Drawio.Net.Service"))).SingleInstance();
            _ = builder.Register<IMapper>(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>(), ctx.Resolve)).InstancePerDependency();

            var mongoDB = System.Configuration.ConfigurationManager.AppSettings["MongoDB"];

            builder.RegisterInstance<IMongoClient>(new MongoClient(mongoDB));

            _container = builder.Build();

            //只有在Build之后，才能调用GetFromFac
        }

        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
