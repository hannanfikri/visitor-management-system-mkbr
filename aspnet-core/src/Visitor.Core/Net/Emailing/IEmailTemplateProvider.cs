namespace Visitor.Net.Emailing
{
    public interface IEmailTemplateProvider
    {
        string GetDefaultTemplate(int? tenantId);
        string GetAppointmentEmailTemplate(int? tenantId);
    }
}
