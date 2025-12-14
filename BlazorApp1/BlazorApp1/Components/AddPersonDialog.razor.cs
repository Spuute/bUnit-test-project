using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorApp1.Components;

public partial class AddPersonDialog
{
    [CascadingParameter] public IMudDialogInstance MudDialog { get; set; } = default!;
    
    private void HandleOk(Person person)
        => MudDialog.Close(DialogResult.Ok(person));

    private void HandleCancel()
        => MudDialog.Cancel();
}