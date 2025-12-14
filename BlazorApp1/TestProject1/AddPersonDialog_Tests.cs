using System.Reflection;
using AutoFixture;
using BlazorApp1.Components;
using BlazorApp1.Models;
using Bunit;
using FakeItEasy;
using MudBlazor;

namespace TestProject1;

public class AddPersonDialog_Tests : TestBase
{
    [Fact]
    public void GIVEN_add_person_dialog_WHEN_input_is_valid_THEN_a_call_to_save_async_should_have_happened_once()
    {
        // Arrange
        var personToAdd = Fixture.Create<Person>();
        var cut = RenderComponent<AddPersonDialogContent>();

        cut.Find("#first-name-input").Input(personToAdd.FirstName);
        cut.Find("#last-name-input").Input(personToAdd.LastName);
        cut.Find("#birth-year-input").Input(personToAdd.BirthYear);

        // Act
        cut.Find("#ok-button").Click();

        // Assert 
        A.CallTo(() => FakeApiService.SaveAsync(
                A<Person>.That.Matches(p =>
                    p.FirstName == personToAdd.FirstName &&
                    p.LastName == personToAdd.LastName &&
                    p.BirthYear == personToAdd.BirthYear)))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public void GIVEN_add_person_dialog_WHEN_not_adding_first_name_THEN_a_call_to_client_error_handling_service_with_correct_message_should_have_happened()
    {
        var cut = RenderComponent<AddPersonDialogContent>();
        var person = Fixture.Create<Person>();
        
        // cut.Find("#first-name-input").Input("Anna");
        cut.Find("#last-name-input").Input(person.LastName);
        cut.Find("#birth-year-input").Input(person.BirthYear);
        cut.Find("#ok-button").Click();
        
        var form = cut.Instance.GetType()
            .GetField("_form", BindingFlags.NonPublic | BindingFlags.Instance)!
            .GetValue(cut.Instance) as MudForm;

        // Dialog stängs inte
        Assert.NotNull(form);
        Assert.Contains(form.Errors, e => e.Contains("\"First Name\" måste anges."));
        
        A.CallTo(() => FakeClientErrorHandlingService.HandleError("Validation Error", false)).MustHaveHappened();
        A.CallTo(() => FakeApiService.SaveAsync(person)).MustNotHaveHappened();
        A.CallTo(() => FakeDialogInstance.Close(A<DialogResult>.Ignored)).MustNotHaveHappened();
    }
    
    [Fact]
    public void GIVEN_add_person_dialog_WHEN_not_adding_first_name_THEN_correct_error_message_should_be_displayed_in_dialog()
    {
        // Arrange
        var cut = RenderComponent<AddPersonDialogContent>();
        
        // Act – sätt tomt värde
        cut.Find("#first-name-input").Input("a");
        
        // Assert – MudForm har errors
        Assert.Contains("mellan 2 och 150", cut.Markup);
        A.CallTo(() => FakeDialogInstance.Close(A<DialogResult>.Ignored)).MustNotHaveHappened();
    }

    [Fact]
    public void GIVEN_add_person_dialog_WHEN_not_adding_last_name_THEN_correct_error_message_should_be_displayed_in_dialog()
    {
        // Arrange
        var cut = RenderComponent<AddPersonDialogContent>();
        
        // Act
        cut.Find("#last-name-input").Input("Adam");
        cut.Find("#birth-year-input").Input("1990");
        
        cut.Find("#ok-button").Click();
        
        // Assert
        Assert.Contains("Last name must be between 2 and 150 characters long.", cut.Markup);
        A.CallTo(() => FakeDialogInstance.Close(A<DialogResult>.Ignored)).MustNotHaveHappened();
    }
}