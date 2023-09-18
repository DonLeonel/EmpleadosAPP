using AutoMapper;
using DB.Models;
using Application.Dtos;
using static Application.CQRS.Commands.Post.PostCargo;
using static Application.CQRS.Commands.Post.PostCiudad;
using static Application.CQRS.Commands.Post.PostCredencial;
using static Application.CQRS.Commands.Post.PostEmpleado;
using static Application.CQRS.Commands.Post.PostSucursal;
using Application.Dtos.Inteface;

namespace Application.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PostEmpleadoCommand, Empleado>();
            CreateMap<PostCiudadCommand, Ciudad>();
            CreateMap<PostCargoCommand, Cargo>();
            CreateMap<PostSucursalCommand, Sucursal>();
            CreateMap<PostCredencialCommand, Credencial>();
            
            CreateMap<Cargo, CargoResp>();  //el orden de lectura es importante!
            CreateMap<Ciudad, CiudadResp>();
            CreateMap<Sucursal, SucursalResp>();   
            CreateMap<Empleado, EmpleadoResp>();  
            CreateMap<Credencial, CredencialResp>();
                        
        }
    }
}
