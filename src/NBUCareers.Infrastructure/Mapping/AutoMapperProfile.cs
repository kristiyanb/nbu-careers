using AutoMapper;
using System;
using System.Linq;

namespace NBUCareers.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var modelRegistrations = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.StartsWith("NBUCareers"))
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Type = t,
                    MapFrom = this.GetMappingProfile(t, typeof(IMapFrom<>)),
                    MapTo = this.GetMappingProfile(t, typeof(IMapTo<>)),
                    ExplicitMap = t.GetInterfaces()
                        .Where(i => i == typeof(ICustomMapping))
                        .Select(i => (ICustomMapping)Activator.CreateInstance(t))
                        .FirstOrDefault()
                });

            foreach (var modelRegistration in modelRegistrations)
            {
                if (modelRegistration.MapFrom != null)
                {
                    this.CreateMap(modelRegistration.MapFrom, modelRegistration.Type);
                }

                if (modelRegistration.MapTo != null)
                {
                    this.CreateMap(modelRegistration.Type, modelRegistration.MapTo);
                }

                modelRegistration.ExplicitMap?.CreateMapping(this);
            }
        }

        private Type GetMappingProfile(Type type, Type mappingInterface)
            => type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == mappingInterface)
                ?.GetGenericArguments()
                .First();
    }
}
