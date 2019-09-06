using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDTOs.Contexts;
using ProjectDTOs.Entities;
using ProjectDTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectDTOs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController: ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //GET api/autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> Get()
        {
            var autores = await context.Autores.ToListAsync();
            var autoresDTO = mapper.Map<List<AutorDTO>>(autores);
            return autoresDTO;
        }

        //GET api/autores/5
        [HttpGet("{id}", Name ="ObtenerAutor")]
        public async Task<ActionResult<AutorDTO>> Get(int id, string param2)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if(autor == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorDTO>(autor);

            return autorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacion)
        {
            var autor = mapper.Map<Autor>(autorCreacion);
            context.Autores.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            //Debemos colocar el nombre de una ruta,
            //el parámetro que recibe el método de la ruta y en el cuerpo de la respuesta 
            //colocamos el autor.
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autorDTO);
        }

        //PUT api/autores/5
        //el método Put se utiliza cuando se realiza actualización completa,
        //es decir se deben enviar todos los campos para que la actualización se realice
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AutorCreacionDTO autorActualizacion)
        {
            var autor = mapper.Map<Autor>(autorActualizacion);
            autor.Id = id;

            context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
            //return Ok();
        }

        // el JsonPatchDocument es porque vamos a recibir un conjunto de operaciones
        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<AutorCreacionDTO> patchDocument)
        {            
            if(patchDocument == null)
            {
                return BadRequest();
            }

            var autorBD = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if(autorBD == null)
            {
                return NotFound();
            }

            var autorDTO = mapper.Map<AutorCreacionDTO>(autorBD);

            //Validamos que los campos a actualizar cumplan con el modelo (se valida con ModelState)
            patchDocument.ApplyTo(autorDTO, ModelState);

            var isValid = TryValidateModel(autorBD);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(autorDTO, autorBD);

            await context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE api/autores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Autor>> Delete(int id)
        {
            var autorId = await context.Autores.Select(x=>x.Id).FirstOrDefaultAsync(x => x == id);

            if(autorId == default(int))
            {
                return NotFound();
            }

            context.Remove(new Autor { Id = autorId });
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
