using AutoMapper;
using ToDoAPI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTodoRequest, Todo>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
          .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());


        CreateMap<UpdateTodoRequest, Todo>()
          .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
          .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}