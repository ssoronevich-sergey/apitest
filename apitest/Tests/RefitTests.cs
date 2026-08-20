using Refit;
using apitest.DTO;
using apitest.Interfaces;
using FxResources.Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace apitest;

public class RefitTests
{
    private IUserApiClient _userApiClient;

    [OneTimeSetUp]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddRefitClient<IUserApiClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://reqres.in/api"));
        var provider = services.BuildServiceProvider();
        _userApiClient = provider.GetRequiredService<IUserApiClient>();   
    }

    [Test]
    public async Task Test1()
    {
        var result = await _userApiClient.getUserAsync(2);
        Assert.Multiple(() =>
            {
                Assert.That(result.Data.Id, Is.EqualTo (2));
                Assert.That(result.Data.Email, Is.Not.Null);
            }


        );
    }
    [Test]
    public async Task Test2()
    {
        var newUser = new CreateUserRequestDTO("Sergo Pozollini", "ООО Тепленькая пошла");
        var responce = await _userApiClient.PostUserAsync(newUser);
        Assert.That (responce.Name, Is.EqualTo("Sergo Pozollini"));
    }
}