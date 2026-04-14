using Microsoft.AspNetCore.Mvc;
using RestWithASPNET9Jorge.Interfaces;
using RestWithASPNET9Jorge.Model;

namespace RestWithASPNET9Jorge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonController> _logger;
    public PersonController(IPersonService personService, ILogger<PersonController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Buscando todas as pessoas!");
        var persons = _personService.FindAll();
        if(persons == null)
        {
            _logger.LogInformation("Nenhuma pessoa foi encontrada!");
            NotFound();
        }
        _logger.LogInformation("Pessoas foram encontradas!");

        return Ok(persons);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id)
    {
        _logger.LogInformation("Buscando pessoa por ID {ID}",id);

        var person = _personService.FindById(id);
        if (person == null)
        {
            _logger.LogInformation("Nenhuma pessoa foi encontrada para o ID: {id}!",id);

            return NotFound();
        }
        _logger.LogInformation("Pessoa com ID: {id} foi encontrada!",id);

        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        if (person == null)
        {
            _logger.LogWarning("Tentativa de criar pessoa com dados nulos");
            return BadRequest("Person data is required");
        }

        _logger.LogInformation("Criando nova pessoa: {@person}", person);

        var createdPerson = _personService.Create(person);
        _logger.LogInformation("Nova pessoa: {@person} criada com sucesso!", person);

        return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut]
     public IActionResult Put([FromBody] Person person)
    {
        _logger.LogInformation("Atualizando dados da pessoa: {@person}", person);
        var updatePerson = _personService.Update(person);
        if (person == null)
        {
            return BadRequest();
        }
        _logger.LogInformation("dados da pessoa: {person} atualizados com sucesso!", person);

        return Ok();
    }

     [HttpDelete("{id}")]
     public IActionResult Delete(long id)
    {
        _logger.LogInformation("Deletando pessoa com ID: {id}!",id);
        _personService.Delete(id);
        _logger.LogInformation("pessoa com ID: {id} deletada com sucesso!", id);

        return NoContent();
    }
}
