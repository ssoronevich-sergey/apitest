using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauceDemo;

public class LoginPage
{
    private readonly IPage Page;
    private ILocator LoginTextBox => Page.Locator ("//input[@id='user-name']");
    private ILocator PassTextBox => Page.Locator ("//input[@id='password']");
    private ILocator LoginButton => Page.Locator("//input[@id='login-button']");
    
    public LoginPage(IPage page)
        {
        Page = page;
        }

    public async Task OpenLoginPageAsync()
    {
        await Page.GotoAsync("https://www.saucedemo.com/");
    }
    
    public async Task LoginUser(string username, string password)
    {
        await LoginTextBox.FillAsync(username);
        await PassTextBox.FillAsync(password);
        await LoginButton.ClickAsync();
        
    }
        
}


