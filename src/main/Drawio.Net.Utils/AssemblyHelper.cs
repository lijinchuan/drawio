using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Utils
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// 获取类和其实现的接口列表
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static List<(Type impl, Type[] interfaces)> GetImpleAndInterfaces(string assemblyName, string nameSearch)
        {
            var result = new List<(Type impl, Type[] interfaces)>();
            if (!string.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> types = assembly
                    .GetTypes()
                    .Where(x => !x.IsInterface && x.Name.Contains(nameSearch))
                    .ToList();

                foreach (var item in types)
                {
                    var interfaceType = item.GetInterfaces();
                    result.Add((item, interfaceType));
                }
                return result;
            }
            return result;
        }

        /// <summary>
        /// 搜索类型
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="suffix">全类名的前缀</param>
        /// <returns></returns>
        public static List<Type> GetTypes(string assemblyName, string suffix)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> types = assembly
                    .GetTypes()
                    .Where(x => !x.IsInterface && x.FullName.StartsWith(suffix))
                    .ToList();

                return types;
            }

            return new List<Type>();
        }
    }
}
