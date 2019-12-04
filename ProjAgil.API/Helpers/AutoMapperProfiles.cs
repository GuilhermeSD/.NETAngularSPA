using System.Linq;
using AutoMapper;
using Domain;
using ProjAgil.API.Dtos;

namespace ProjAgil.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>().
            ForMember(dto => dto.Palestrantes, opt => {
                opt.MapFrom(domain => domain.PalestrantesEventos.Select(x=>x.Palestrante).ToList());
            }).ReverseMap();
            
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            
            CreateMap<Palestrante, PalestranteDto>().
            ForMember(dto => dto.Eventos, opt => {
                opt.MapFrom(domain => domain.PalestrantesEventos.Select(x=>x.Evento).ToList());
            }).ReverseMap();
        }
    }
}