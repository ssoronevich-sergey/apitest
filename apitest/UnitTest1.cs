using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using apitest.DTO;
using NUnit.Framework;
namespace apitest;
public class Tests
{
    private static HttpClient client;
    //работает только для тестов в классе Test
    [OneTimeSetUp]
    public void Setup()
    {
        client = new HttpClient()
        {
            BaseAddress = new Uri("https://reqres.in/api/")
        };
        client.DefaultRequestHeaders.Add("x-api-key", "free_user_3HpELMQtbGQTO17ItccFNl2nZss");
    }

    [Test]
    public async Task Test1()
    { 
        //Get запрос
        using HttpResponseMessage response = await client.GetAsync("users/2");
        //проверка статускода
        response.EnsureSuccessStatusCode();
    }

    [Test]
    public async Task Test2()
    {
        using HttpResponseMessage response = await client.GetAsync("users/2");
        string jsonGet = await response.Content.ReadAsStringAsync();
        UserResponceDTO userResponce = JsonSerializer.Deserialize<UserResponceDTO>(jsonGet);
        UserDataDTO user = userResponce.Data;
        if (user.Id == 2)
        {
        
        }
        else
        {
            throw new Exception();
        }
        
    }

    [Test]
    public async Task Test3()
    {
        var payload = new CreateUserRequestDTO
        {
            Name = "Sergo Pozollini",
            Job = "ООО Тепленькая пошла"
        };

        using HttpResponseMessage postResponce = await client.PostAsJsonAsync("users", payload);
    
        string jsonResponce = await postResponce.Content.ReadAsStringAsync();
        CreatedUserDTO createdUser = JsonSerializer.Deserialize<CreatedUserDTO>(jsonResponce);
        
        //дополнительные проверки что что-то пришло и записалось верно.
        Assert.That(createdUser, Is.Not.Null);
        Assert.That(createdUser.Id, Is.Not.Null);
        Assert.That(createdUser.CreatedAt, Is.Not.Null);
        Assert.That(createdUser.Name, Is.EqualTo("Sergo Pozollini"));
        Assert.That(createdUser.Job, Is.EqualTo("ООО Тепленькая пошла"));

        // визуальный отчет в консоль
        Console.WriteLine($"Created user: Id={createdUser.Id}, CreatedAt= {createdUser.CreatedAt}");
    }

    [Test]
    public async Task Test4()
    {
        var putPayload = new CreateUserRequestDTO
        {
            Name = "Sergo Pozollini",
            Job = "ООО Холодненькая пришла"
        };
        //Put запрос
        using HttpResponseMessage putResponce = await client.PutAsJsonAsync("users/2", putPayload);
        //проверка статус кода
        putResponce.EnsureSuccessStatusCode();
        
        //десериализация и проверка дополнительно что обновилось верно.
        string jsonResponce = await putResponce.Content.ReadAsStringAsync();
        CreatedUserDTO updatedUser = JsonSerializer.Deserialize<CreatedUserDTO>(jsonResponce);
        Assert.That(updatedUser, Is.Not.Null);
        Assert.That(updatedUser.Name, Is.Not.Null);
        Assert.That(updatedUser.Name, Is.EqualTo("Sergo Pozollini"));
        Assert.That(updatedUser.Job,Is.Not.Null);
        Assert.That(updatedUser.Job,Is.EqualTo("ООО Холодненькая пришла"));
        
        // визуальный отчет в консоль
        Console.WriteLine($"Updated user: Name={updatedUser.Name}, Job= {updatedUser.Job}");
    }

    [Test]
    public async Task Test5()
    {
        using HttpResponseMessage deleteResponse = await client.DeleteAsync("users/2");
        deleteResponse.EnsureSuccessStatusCode();
        
        //визуальный отчет в консоль
        Console.WriteLine($"Статус код запроса Delete:\n {deleteResponse}");
    }
    
    [OneTimeTearDown]
    public void TearDown()
    {
        client.Dispose();
    }
}

//free_user_3HpELMQtbGQTO17ItccFNl2nZss