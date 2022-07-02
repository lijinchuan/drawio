using Autofac;
using AutoMapper.Configuration;
using Drawio.Net.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drawio.Net.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomAutofacModule : Module
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public CustomAutofacModule(ContainerBuilder builder, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// AutoFac注册类
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var accessImpleAndInterfaces = AssemblyHelper.GetImpleAndInterfaces("Drawio.Net.Data", "");
            accessImpleAndInterfaces.AddRange(AssemblyHelper.GetImpleAndInterfaces("Drawio.Net.Service", "Service"));
            foreach (var v in accessImpleAndInterfaces)
            {
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

            builder.RegisterInstance<IMongoClient>(new MongoClient(_configuration["MongoDB"]));

            //builder.RegisterGeneric(typeof(TopucHunterRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();//注册仓储泛型                                                                                             //builder.RegisterGeneric(typeof(MyRepositoryBase<，>)).As(typeof(IMyRepository<，>)).InstancePerDependency();//注册仓储泛型 2个以上的泛型参数
            //  builder.RegisterType<myAssembly>().As<ImyAssembly>();   //普通依赖注入
        }
    }
}
