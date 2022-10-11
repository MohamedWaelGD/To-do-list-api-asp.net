using AutoMapper;
using To_doListApiApp.Dtos.ItemDto;
using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Dtos.UserWorkspaceDto;
using To_doListApiApp.Dtos.WorkspaceDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserGetDto>().ReverseMap();
            CreateMap<User, UserEditDto>().ReverseMap();

            CreateMap<Workspace, WorkspaceCreateDto>().ReverseMap();
            CreateMap<Workspace, WorkspaceEditDto>().ReverseMap();
            CreateMap<Workspace, WorkspaceGetDto>().ReverseMap();

            CreateMap<Item, ItemCreateDto>().ReverseMap();
            CreateMap<Item, ItemEditDto>().ReverseMap();
            CreateMap<Item, ItemGetDto>().ReverseMap();

            CreateMap<UserWorkspace, UserWorkspaceCreateDto>().ReverseMap();
            CreateMap<UserWorkspace, UserWorkspaceEditDto>().ReverseMap();
            CreateMap<UserWorkspace, UserWorkspaceGetDto>().ReverseMap();
        }
    }
}
