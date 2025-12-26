namespace Shared.Mailing;

public interface IEmailService : IBaseEmailService
{
    void Send(IEmailTemplate emailTemplate, bool sendSeparately = false);

    void SendToAdministrators(IEmailTemplate emailTemplate, bool sendSeperately = false);

    void Send(string to, string from, string subject, string message, bool isHtml = true);
}
