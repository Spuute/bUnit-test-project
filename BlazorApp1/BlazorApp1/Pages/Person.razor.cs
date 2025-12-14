using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages;

public partial class Person
{
    [Inject] public IApiService ApiService { get; set; }
    
    private List<Models.Person> _persons = [];
    protected override async Task OnInitializedAsync()
    {
        _persons = await ApiService.GetAllAsync();
        foreach (var person in _persons)
        {
            Console.WriteLine(person.FirstName + " " + person.LastName + " " + person.BirthYear);
        }
    }
}