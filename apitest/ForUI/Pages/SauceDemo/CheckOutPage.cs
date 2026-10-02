using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauceDemo;

public class CheckoutPage
{
    private readonly IPage Page;

    private ILocator FirstNameInput => Page.Locator("[data-test='firstName']");
    private ILocator LastNameInput => Page.Locator("[data-test='lastName']");
    private ILocator PostalCodeInput => Page.Locator("[data-test='postalCode']");
    private ILocator ContinueButton => Page.Locator("[data-test='continue']");
    private ILocator PageTitle => Page.Locator("[data-test='title']");

    public CheckoutPage(IPage page)
    {
        Page = page;
    }

    public async Task<bool> IsOpenedAsync()
    {
        await PageTitle.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });

        var title = (await PageTitle.TextContentAsync())?.Trim();
        return title == "Checkout: Your Information";
    }

    public async Task FillCustomerInfoAsync(string firstName, string lastName, string postalCode)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await PostalCodeInput.FillAsync(postalCode);
    }

    public async Task<CheckoutOverviewPage> ClickContinueAsync()
    {
        await ContinueButton.ClickAsync();
        await Page.WaitForURLAsync("**/checkout-step-two.html");
        return new CheckoutOverviewPage(Page);
    }
}