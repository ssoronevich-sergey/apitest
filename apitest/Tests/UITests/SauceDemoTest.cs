using apitest.ForUI.Pages.SauceDemo;
using FluentAssertions;
using Microsoft.Playwright;

namespace apitest.Tests.UITests;

public class SauceDemoTest : BaseTest
{
    [Test]
    public async Task LoginAndCheckoutTwoItems()
    {
        var loginPage = new LoginPage(Page);
        await loginPage.OpenLoginPageAsync();
        await loginPage.LoginUser("standard_user", "secret_sauce");
        
        var productsPage = new ProductsPage(Page);
        var isTitleVisible = await productsPage.IsProductsTitleVisibleAsync();
        isTitleVisible.Should().BeTrue();
        var titleText = await productsPage.GetProductsTitleTextAsync();
        titleText.Should().Be("Products");

        //ищем по названиям товаров и добавляем в корзину
        const string firstProduct = "Sauce Labs Backpack";
        const string secondProduct = "Sauce Labs Bike Light";
        await productsPage.AddToCartByNameAsync(firstProduct);
        await productsPage.AddToCartByNameAsync(secondProduct);

        // Проверка кол-ва товаров в корзине
        var cartCount = await productsPage.GetCartCountAsync();
        cartCount.Should().Be(2);
        var cartPage = await productsPage.GoToCartAsync();

        //переходим на страницу корзины, проверяем кол-во товаров и их имена
        var isCartOpened = await cartPage.IsOpenedAsync();
        isCartOpened.Should().BeTrue();
        var itemsCount = await cartPage.GetItemsCountAsync();
        itemsCount.Should().Be(2);
        var cartItems = await cartPage.GetItemNamesAsync();
        cartItems.Should().BeEquivalentTo(new[] { firstProduct, secondProduct });

        // форма заказа
        var checkoutPage = await cartPage.ClickCheckoutAsync();
        var isCheckoutOpened = await checkoutPage.IsOpenedAsync();
        isCheckoutOpened.Should().BeTrue();
        await checkoutPage.FillCustomerInfoAsync(
            firstName: "Sergo",
            lastName: "Pozzolini",
            postalCode: "88005553535");
        var overviewPage = await checkoutPage.ClickContinueAsync();

        // Обзор заказа и проверка что в заказе
        var isOverviewOpened = await overviewPage.IsOpenedAsync();
        isOverviewOpened.Should().BeTrue();
        var overviewItemsCount = await overviewPage.GetItemsCountAsync();
        overviewItemsCount.Should().Be(2);
        var overviewItems = await overviewPage.GetItemNamesAsync();
        overviewItems.Should().BeEquivalentTo(new[] { firstProduct, secondProduct });

        // Страница подтверждения
        var completePage = await overviewPage.ClickFinishAsync();
        var isCompleteOpened = await completePage.IsOpenedAsync();
        isCompleteOpened.Should().BeTrue();

        //"Thank you for your order!"
        var headerText = await completePage.GetCompleteHeaderTextAsync();
        headerText.Should().Be("Thank you for your order!");
    }
}