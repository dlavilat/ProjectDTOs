using AutoMapper;
using ProjectDTOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDTOs.Models.Auxiliares
{
    public class SimpleMappings: Profile
    {
        public SimpleMappings()
        {
            CreateMap<Autor, AutorDTO>();
            CreateMap<Libro, LibroDTO>();
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorCreacionDTO>();
        }
    }
}
