namespace NBUCareers.Infrastructure.Mapping
{
    using AutoMapper;

    public interface ICustomMapping
    {
        void CreateMapping(IProfileExpression configuration);
    }
}
