using Autofac;
using Drawio.Net.Utils;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public CustomAutofacModule(ContainerBuilder builder)
        {

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

            //builder.RegisterGeneric(typeof(TopucHunterRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();//注册仓储泛型                                                                                             //builder.RegisterGeneric(typeof(MyRepositoryBase<，>)).As(typeof(IMyRepository<，>)).InstancePerDependency();//注册仓储泛型 2个以上的泛型参数
            //  builder.RegisterType<myAssembly>().As<ImyAssembly>();   //普通依赖注入
        }
    }
}
