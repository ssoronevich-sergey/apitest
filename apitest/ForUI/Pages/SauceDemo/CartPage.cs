using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauceDemo;

public class CartPage
{
    private readonly IPage Page;

    private ILocator CartItems => Page.Locator(".cart_item");
    private ILocator CartItemNames => Page.Locator(".cart_item .inventory_item_name");
    private ILocator CartTitle => Page.Locator("[data-test='title']");
    private ILocator CheckoutButton => Page.Locator("[data-test='checkout']");

    public CartPage(IPage page)
    {
        Page = page;
    }

    public async Task<bool> IsOpenedAsync()
    {
        await CartTitle.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });

        var title = (await CartTitle.TextContentAsync())?.Trim();
        return title == "Your Cart";
    }

    public async Task<IReadOnlyList<string>> GetItemNamesAsync()
    {
        var names = await CartItemNames.AllTextContentsAsync();
        return names.Select(n => n.Trim()).ToList();
    }

    public async Task<int> GetItemsCountAsync()
        => await CartItems.CountAsync();

    public async Task<CheckoutPage> ClickCheckoutAsync()
    {
        await CheckoutButton.ClickAsync();
        await Page.WaitForURLAsync("**/checkout-step-one.html");
        return new CheckoutPage(Page);
    }
}