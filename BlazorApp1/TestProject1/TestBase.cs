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
    protected readonly IFixture Fixture;
    protected readonly IApiService FakeApiService;
    protected readonly IMudDialogInstance FakeDialogInstance;
    protected readonly IClientErrorHandlingService FakeClientErrorHandlingService;

    public TestBase()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;

        Fixture = new Fixture()
            .Customize(new CompositeCustomization(new AutoFakeItEasyCustomization()));

        FakeApiService = Fixture.Freeze<IApiService>();
        FakeDialogInstance = Fixture.Freeze<IMudDialogInstance>();
        FakeClientErrorHandlingService = Fixture.Freeze<IClientErrorHandlingService>();
        
        Services.AddSingleton(FakeClientErrorHandlingService);
        Services.AddSingleton<AddPersonValidator>();
        Services.AddSingleton(FakeApiService);
        Services.AddMudServices();
    }
}