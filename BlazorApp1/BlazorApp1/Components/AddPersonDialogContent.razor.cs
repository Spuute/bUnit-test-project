using BlazorApp1.Forms;
using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorApp1.Components;

public partial class AddPersonDialogContent
{
    [Inject] public IApiService ApiService { get; set; } = default!;
    [Inject] public IClientErrorHandlingService ErrorService { get; set; } = default!;
    [Inject] public AddPersonValidator Validator { get; set; } = default!;

    [Parameter] public EventCallback<Person> OnOk { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    private MudForm _form = default!;
    private PersonForm _person = new();
    private bool _isValid;

    private async Task Submit()
    {
        await _form.Validate();

        if (!_isValid)
        {
            await ErrorService.HandleError("Validation Error");
            return;
        }

        var person = new Person
        {
            FirstName = _person.FirstName!,
            LastName = _person.LastName!,
            BirthYear = _person.BirthYear
        };

        await ApiService.SaveAsync(person);
        await OnOk.InvokeAsync(person);
    }

    private async Task Cancel()
        => await OnCancel.InvokeAsync();
}