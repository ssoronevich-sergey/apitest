using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauceDemo;

public class CheckoutCompletePage
{
    private readonly IPage Page;
    private ILocator CompleteHeader => Page.Locator("[data-test='complete-header']");
    public CheckoutCompletePage(IPage page)
    {
        Page = page;
    }

    public async Task<bool> IsOpenedAsync()
    {
        await CompleteHeader.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });

        return true;
    }
    public async Task<string> GetCompleteHeaderTextAsync() => (await CompleteHeader.TextContentAsync())?.Trim() ?? string.Empty;
}