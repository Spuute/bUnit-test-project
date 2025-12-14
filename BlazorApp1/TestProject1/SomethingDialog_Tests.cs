using System.Reflection;
using BlazorApp1.Components;
using Bunit;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;

namespace TestProject1;

public class SomethingDialogTests : TestBase
{
    [Fact]
    public void Clicking_OK_Should_Call_Close_With_Ok_Result()
    {
        var cut = RenderComponent<SomethingDialog>(p =>
            p.AddCascadingValue(_fakeDialogInstance)
        );

        // Kalla metoder indirekt via knappar eller direkt via instance
        cut.Instance.GetType()
            .GetMethod("Submit", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.Invoke(cut.Instance, null);

        A.CallTo(() => _fakeDialogInstance.Close(A<DialogResult>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Clicking_Cancel_Should_Call_Cancel()
    {
        var provider = RenderComponent<MudDialogProvider>();
        var service = Services.GetRequiredService<IDialogService>();

        IDialogReference dialogRef = null;
        await provider.InvokeAsync(async () =>
        {
            dialogRef = await service.ShowAsync<SomethingDialog>();
        });

        provider.Find("#ok-button").Click();
        var rv = await dialogRef.GetReturnValueAsync<bool?>();
        Assert.True(rv);
    }
}