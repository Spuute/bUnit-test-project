using BlazorApp1.Pages;
using Bunit;
using MudBlazor.Services;

namespace TestProject1;

public class index_tests : TestContext
{
    public index_tests()
    {
        Services.AddMudServices();
    }
    [Fact]
    public void Index_Should_Render_Hello_World()
    {
        var cut = RenderComponent<Home>();
        cut.Find("h1").MarkupMatches("<h1>Hello, world!</h1>");
    }
}