using BlazorApp1.Models;

namespace BlazorApp1;

public interface IApiService
{
    Task SaveAsync(Person person);
    Task<List<Person>> GetAllAsync();
}