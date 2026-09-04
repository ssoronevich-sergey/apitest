namespace apitest.Dependency;

public interface IEmailSender
{
    void Send(string to, string text);
}