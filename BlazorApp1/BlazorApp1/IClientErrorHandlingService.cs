namespace BlazorApp1;

public interface IClientErrorHandlingService
{
    Task HandleError(Exception exception, bool isOnlyWarning = false);
    Task HandleError(string customErrorMessage, bool isOnlyWarning = false);
}