using Microsoft.Playwright;

namespace apitest.ForUI.Pages.SauceDemo;

public class ProductsPage
{
    private readonly IPage Page;
    private ILocator ProductsTitle => Page.Locator("[data-test='title']");
    private ILocator InventoryItems => Page.Locator(".inventory_item");
    private ILocator CartBadge => Page.Locator(".shopping_cart_badge");
    private ILocator CartLink => Page.Locator(".shopping_cart_link");

    public ProductsPage(IPage page)
    {
        Page = page;
    }

    public ILocator GetProductsTitle() => ProductsTitle;
    public async Task<bool> IsProductsTitleVisibleAsync() => await ProductsTitle.IsVisibleAsync();
    public async Task<string> GetProductsTitleTextAsync() => (await ProductsTitle.TextContentAsync())?.Trim() ?? string.Empty;

    // поиск товаров по названию
    public async Task AddToCartByNameAsync(string productName)
    {
        var item = InventoryItems.Filter(new LocatorFilterOptions
        {
            Has = Page.Locator(".inventory_item_name", new PageLocatorOptions
            {
                HasText = productName
            })
        });

        await item.Locator("button:has-text('Add to cart')").ClickAsync();
    }

    public async Task<int> GetCartCountAsync()
    {
        if (await CartBadge.CountAsync() == 0)
            return 0;

        var text = await CartBadge.TextContentAsync();
        return int.TryParse(text, out var count) ? count : 0;
    }

    public async Task<CartPage> GoToCartAsync()
    {
        await CartLink.ClickAsync();
        await Page.WaitForURLAsync("**/cart.html");
        return new CartPage(Page);
    }
}