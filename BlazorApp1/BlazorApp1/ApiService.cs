using BlazorApp1.Models;

namespace BlazorApp1;

public class ApiService : IApiService
{
    private readonly List<Person> _persons = [];
    
    public Task SaveAsync(Person person)
    {
        _persons.Add(person);
        return Task.CompletedTask;
    }

    public Task<List<Person>> GetAllAsync()
    {
        return Task.FromResult(_persons);
    }
}