﻿using AutoMapper;
using To_doListApiApp.Dtos.ItemDto;
using To_doListApiApp.Dtos.UserDto;
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

            CreateMap<Workspace, WorkspaceCreateDto>().ReverseMap();
            CreateMap<Workspace, WorkspaceEditDto>().ReverseMap();
            CreateMap<Workspace, WorkspaceGetDto>().ReverseMap();

            CreateMap<Item, ItemCreateDto>().ReverseMap();
            CreateMap<Item, ItemEditDto>().ReverseMap();
            CreateMap<Item, ItemGetDto>().ReverseMap();
        }
    }
}