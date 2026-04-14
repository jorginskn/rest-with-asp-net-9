using RestWithASPNET9Jorge.Model;
using RestWithASPNET9Jorge.Model.Context;

namespace RestWithASPNET9Jorge.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly MSSQLContext _context;
    public PersonRepository(MSSQLContext context)
    {
        _context = context;
    }
    public List<Person> FindAll()
    {
        var persons = _context.Persons.ToList();
        return persons;
    }
    public Person FindById(long id)
    {
        var person = _context.Persons.Find(id);
        return person;
    }

    public Person Create(Person person)
    {
        _context.Add(person);
        _context.SaveChanges();
        return person;
    }

    public void Delete(long id)
    {
        try
        {
            var existingPerson = _context.Persons.Find(id);

            if (existingPerson == null)
            {
                return;
            }
            else
            {
                _context.Remove(existingPerson);
                _context.SaveChanges();
                return;
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
            var existingPerson = _context.Persons.Find(person.Id);
            if (existingPerson == null)
            {
                return null;
            }
            else
            {
                _context.Entry(existingPerson).CurrentValues.SetValues(person);
                _context.SaveChanges();
                return person;
            }

        }
        catch (Exception ex)
        {

            throw;
        }

    }

}
