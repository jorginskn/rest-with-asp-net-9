using RestWithASPNET9Jorge.Interfaces;
using RestWithASPNET9Jorge.Model;
using RestWithASPNET9Jorge.Repositories;

namespace RestWithASPNET9Jorge.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    public PersonService(IPersonRepository repository)
    {
        _repository = repository;
    }
    public List<Person> FindAll()
    {
        var persons = _repository.FindAll();
        return persons;
    }
    public Person FindById(long id)
    {
        var person = _repository.FindById(id);
        return person;
    }

    public Person Create(Person person)
    {
       var createdPerson = _repository.Create(person);
        return createdPerson;
    }

    public void Delete(long id)
    {
        try
        {
            var existingPerson = _repository.FindById(id);

            if (existingPerson == null)
            {
                return;
            }
            else
            {
                _repository.Delete(id);
                return ;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    public Person Update(Person person)
    {
        try
        {
            var existingPerson = _repository.FindById(person.Id);
            if (existingPerson == null)
            {
                return null;
            }
            else
            {
                 _repository.Update(person);
                return person;
            }

        }
        catch (Exception ex)
        {

            throw;
        }
        
     }

 
}
