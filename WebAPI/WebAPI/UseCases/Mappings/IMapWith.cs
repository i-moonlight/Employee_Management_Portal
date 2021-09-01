using AutoMapper;

namespace WebAPI.UseCases.Mappings
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
