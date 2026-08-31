using System.IO;
using System.Text.Json;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace apitest.DTO.UserDTOs;

[TestFixture]
public class UserTests
{
    private RootDTO _root;

    [SetUp]
    public void Setup()
    {
        var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "UsersData.json");
        string data = File.ReadAllText(path);
        _root = JsonSerializer.Deserialize<RootDTO>(data);
    }

    [Test]
    public void UserCount10()
    {
        var count = _root.Data.Count;
        count.Should().Be(10);
    }

    [Test]
    public void NameIsAliceJohnson()
    {
        var firstUser = _root.Data.First();
        firstUser.Profile.FullName.Should().Be("Alice Johnson");
    }
    
    [Test]
    public void UniqueIdTest()
    {
        var ids = _root.Data.Select(u => u.Id).ToList();
        ids.Should().OnlyHaveUniqueItems();
    }
    
    [Test]
    public void OnePremiumUser()
    {
        var hasPremium = _root.Data.Any(u => u.Profile.Tags.Contains("premium"));
        hasPremium.Should().BeTrue();
    }
    [Test]
    public void CityIsNotEmpty()
    {
        var allHaveCity = _root.Data.All(u => !string.IsNullOrEmpty(u.Profile.Address.City));
        allHaveCity.Should().BeTrue();
    }
    [Test]
    public void UserFromStockholm()
    {
        var hasStockholmUser = _root.Data.Any(u => u.Profile.Address.City == "Stockholm");
        hasStockholmUser.Should().BeTrue();
    }
    [Test]
    public void Age_18_60()
    {
        var allInRange = _root.Data.All(u => u.Profile.Age >= 18 && u.Profile.Age <= 60);
        allInRange.Should().BeTrue();
    }
    [Test]
    public void AdminUser()
    {
        var hasAdmin = _root.Data.Any(u => u.Roles.Contains("admin"));
        hasAdmin.Should().BeTrue();
    }
}