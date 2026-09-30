using FluentAssertions;
using Microsoft.Playwright;

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
    }

    [Test]
    public async Task DropDownTestFromLesson()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/dropdown");
        await Assertions.Expect(Page).ToHaveTitleAsync("The Internet");
        await Assertions.Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/dropdown");    
        var dropdown = Page.Locator("#dropdown");
        await Assertions.Expect(dropdown).ToBeVisibleAsync();
        await dropdown.SelectOptionAsync("1");
        await Assertions.Expect(dropdown).ToHaveValueAsync("1");
        //проверка для стандартного дропдауна
        var selected = dropdown.Locator("option:checked");
        await Assertions.Expect(selected).ToHaveTextAsync("Option 1");
        //универсальная проверка
        var text = await dropdown.InnerTextAsync();
        text.Should().Contain("Option 1");
        
        //проверка по option[@selected='selected]
        var opt1 = dropdown.Locator("//option[@selected='selected']");
        var textOpt1 =await opt1.InnerTextAsync();
        textOpt1.Should().Be("Option 1");
        
        await dropdown.SelectOptionAsync("2");
        await Assertions.Expect(dropdown).ToHaveValueAsync("2");
        //проверка для стандартного дропдауна
        
        await Assertions.Expect(selected).ToHaveTextAsync("Option 2");
        //универсальная проверка
        var text2 = await dropdown.InnerTextAsync();
        text.Should().Contain("Option 2");
    }
    
    [Test]
    //нестандартный дропдаун
    public async Task Should_Select_Sub_Item()
    {
        await Page.GotoAsync("https://demoqa.com/select-menu");
        var dropdown = Page.Locator("#withOptGroup");
        await dropdown.ClickAsync();

        var option = Page.GetByText("Group 1, option 1");
        await option.ClickAsync();

        var text = await dropdown.TextContentAsync();
        await Assertions.Expect(dropdown).ToContainTextAsync("Group 1, option 1");
    }
    
}