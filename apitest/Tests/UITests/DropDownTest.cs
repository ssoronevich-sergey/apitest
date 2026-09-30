using FluentAssertions;
using Microsoft.Playwright;

namespace apitest.Tests.UITests;

public class DropdownTest : BaseTest
{
    [Test]
    public async Task DropdownHomeworkTest()
    {
        await Page.GotoAsync("https://demoqa.com/select-menu");
        var drop = Page.Locator("#selectOne");
        await drop.ClickAsync();
        
        var option = Page.GetByText("Prof.");
        await option.ClickAsync();

        var text = await drop.TextContentAsync();
        await Assertions.Expect(drop).ToContainTextAsync("Prof.");
    }

}