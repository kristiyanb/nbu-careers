using AutoMapper;

namespace NBUCareers.Infrastructure.Mapping
{
    public interface ICustomMapping
    {
        void CreateMapping(IProfileExpression configuration);
    }
}
