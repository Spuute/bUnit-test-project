using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using BlazorApp1;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace TestProject1;

public abstract class TestBase : TestContext
{
    protected readonly IFixture _fixture;
    protected readonly IApiService _fakeApiService;
    protected readonly IMudDialogInstance _fakeDialogInstance;
    protected readonly IClientErrorHandlingService _fakeClientErrorHandlingService;

    public TestBase()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        _fixture = new Fixture()
            .Customize(new CompositeCustomization(new AutoFakeItEasyCustomization()));

        _fakeApiService = _fixture.Freeze<IApiService>();
        _fakeDialogInstance = _fixture.Freeze<IMudDialogInstance>();
        _fakeClientErrorHandlingService = _fixture.Freeze<IClientErrorHandlingService>();
        
        Services.AddSingleton(_fakeClientErrorHandlingService);
        Services.AddSingleton<AddPersonValidator>();
        Services.AddSingleton(_fakeApiService);
        Services.AddMudServices();
    }
}