using AutoMapper;

namespace WebAPI.UserCases.Common.Mappings
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}