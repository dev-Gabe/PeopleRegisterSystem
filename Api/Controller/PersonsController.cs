using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;

namespace Api.Controller {
    // Controlador responsável por gerenciar as operações CRUD para a entidade Person.
    [ApiController]
    [Route("Api/[Controller]")]

    public class PersonsController : ControllerBase{
        private readonly AppDbContext _context;
        public PersonsController(AppDbContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<List<Person>> Get() => await _context.Persons.ToListAsync();

        [HttpPost]
        public async Task<IActionResult> Post(Person person){
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Person updated){
            var person = await _context.Persons.FindAsync(id);
            
            if (person == null){
                return NotFound(); // Retorna 404 Not Found se a pessoa com o ID especificado não for encontrada.
            }

            person.Nome = updated.Nome;
            person.Sobrenome = updated.Sobrenome;
            person.Telefone = updated.Telefone;

            await _context.SaveChangesAsync();
            return Ok(person);
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var person = await _context.Persons.FindAsync(id);
            
            if (person == null){
                return NotFound();  // Retorna 404 Not Found se a pessoa com o ID especificado não for encontrada.
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return Ok(); 
        }
    }

}