using AutoMapper;
using Drawio.Net.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Drawio.Net.Service
{
    public class EntityMappingProfile: Profile
    {
        public EntityMappingProfile()
        {
            //domain里面的entity和model对应默认互相注册
            var sourceTypes = AssemblyHelper.GetTypes("Drawio.Net.Domain", "Drawio.Net.Domain.Entity");
            var destTypes = AssemblyHelper.GetTypes("Drawio.Net.Domain", "Drawio.Net.Domain.Model");

            foreach (var source in sourceTypes)
            {
                var m = Regex.Match(source.Name, @"([\w]+)Entity$");
                if (m.Success)
                {
                    var dest = destTypes.FirstOrDefault(p => p.Name == m.Groups[1].Value + "Model");

                    if (dest != null)
                    {
                        CreateMap(source, dest).ReverseMap();
                    }
                }
            }

        }
    }
}
