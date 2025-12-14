using MudBlazor;

namespace BlazorApp1;

public class ClientErrorHandlingService : IClientErrorHandlingService
{
    private readonly ISnackbar _snackbar;

    public ClientErrorHandlingService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }
    
    public Task HandleError(Exception exception, bool isOnlyWarning = false)
    {
        OnError(exception.Message, isOnlyWarning);
        return Task.CompletedTask;
    }

    public Task HandleError(string customErrorMessage, bool isOnlyWarning = false)
    {
        OnError(customErrorMessage, isOnlyWarning);
        return Task.CompletedTask;
    }

    private void OnError(string errorMessage, bool isOnlyWarning)
    {
        var severityLevel = Severity.Error;

        if (isOnlyWarning)
        {
            severityLevel = Severity.Warning;
        }
        
        _snackbar.Add($"{errorMessage}", severityLevel);
    }
}