using API.Dtos;
using API.Models;
using AutoMapper;
using System.Text.RegularExpressions;
using API.Dtos.EmpleadosDtos.ConId;
using API.Dtos.EmpleadosDtos.SinId;
using API.Services.Commands.CreateEmpleadoCommand;
using API.Dtos.SucursalesDtos;

namespace ApiFinal.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Empleado, EmpleadoDto>().ReverseMap();
            //CreateMap<Empleado, EmpleadoListaDto>().ReverseMap();


            CreateMap<Empleado, EmpleadoListaSinIdDto>()
                .ForMember(dest => dest.CargoNombre, opt => opt.MapFrom(src => src.Cargo.Nombre))
                .ForMember(dest => dest.SucursalNombre, opt => opt.MapFrom(src => src.Sucursal.Nombre))
                .ForMember(dest => dest.CiudadNombre, opt => opt.MapFrom(src => src.Sucursal.Ciudad.Nombre))
                .ForMember(dest => dest.JefeNombre, opt => opt.MapFrom(src => src.Jefe.Nombre))
                .ReverseMap();


            CreateMap<Empleado, EmpleadoSinIdDto>()
                .ForMember(dest => dest.CargoNombre, opt => opt.MapFrom(src => src.Cargo.Nombre))
                .ForMember(dest => dest.SucursalNombre, opt => opt.MapFrom(src => src.Sucursal.Nombre))
                .ForMember(dest => dest.CiudadNombre, opt => opt.MapFrom(src => src.Sucursal.Ciudad.Nombre))
                .ForMember(dest => dest.JefeNombre, opt => opt.MapFrom(src => src.Jefe.Nombre))
                .ReverseMap();


            CreateMap<Cargo, CargoDto>().ReverseMap();
            CreateMap<Ciudad, CiudadDto>().ReverseMap();
            CreateMap<Sucursal, SucursalDto>().ReverseMap();


            CreateMap<Empleado, CreateEmpleadoCommand>().ReverseMap();
        }
    }
}
