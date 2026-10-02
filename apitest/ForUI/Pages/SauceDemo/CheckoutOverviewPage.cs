using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauceDemo;

public class CheckoutOverviewPage
{
    private readonly IPage Page;

    private ILocator PageTitle => Page.Locator("[data-test='title']");
    private ILocator OverviewItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator OverviewItems => Page.Locator(".cart_item");
    private ILocator FinishButton => Page.Locator("[data-test='finish']");

    public CheckoutOverviewPage(IPage page)
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
        return title == "Checkout: Overview";
    }

    public async Task<IReadOnlyList<string>> GetItemNamesAsync()
    {
        var names = await OverviewItemNames.AllTextContentsAsync();
        return names.Select(n => n.Trim()).ToList();
    }

    public async Task<int> GetItemsCountAsync()
        => await OverviewItems.CountAsync();

    public async Task<CheckoutCompletePage> ClickFinishAsync()
    {
        await FinishButton.ClickAsync();
        await Page.WaitForURLAsync("**/checkout-complete.html");
        return new CheckoutCompletePage(Page);
    }
}