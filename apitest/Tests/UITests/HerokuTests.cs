using FluentAssertions;
namespace apitest.Tests.UITests;

public class HerokuTests : BaseTest
{
    [Test]
    public async Task CheckBoxTest()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
        var first = Page.Locator ("input[type=checkbox]").Nth(0);
        await first.CheckAsync();
        (await first.IsCheckedAsync()).Should().BeTrue();
    }

    [Test]
    public async Task FormAuthenticationTest()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/login");
        var loginTextBox = Page.Locator ("//input[@id='username']");
        await loginTextBox.FillAsync("wrong");
        var passTextBox = Page.Locator ("//input[@id='password']");
        await passTextBox.FillAsync("wrong");
        var loginButton = Page.Locator("button[type='submit']");
        await loginButton.ClickAsync();
        var errorMessage = await Page.QuerySelectorAsync("#flash");
        var textErrorMessage = await errorMessage.InnerTextAsync();
        textErrorMessage.Should().Contain("Your username is invalid!");
    }
    [Test]
    public async Task LoginTest()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
        var loginTextBox = Page.Locator ("//input[@id='user-name']");
        await loginTextBox.FillAsync("standard_user");
        var passTextBox = Page.Locator ("//input[@id='password']");
        await passTextBox.FillAsync("secret_sauce");
        var loginButton = Page.Locator("//input[@id='login-button']");
        await loginButton.ClickAsync();
        var products = Page.Locator("span.title", new() {HasTextString = "Products"});;
        var textProductMessage = await products.InnerTextAsync();
        textProductMessage.Should().Contain("Products");
    }
    
}