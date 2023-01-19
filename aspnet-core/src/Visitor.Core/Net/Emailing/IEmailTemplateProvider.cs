namespace Visitor.Net.Emailing
{
    public interface IEmailTemplateProvider
    {
        string GetDefaultTemplate(int? tenantId);
        string GetAppointmentEmailTemplate(int? tenantId);
        string GetCancelAppointmentEmailTemplate(int? tenantId);
        string GetThanksEmailTemplate(int? tenantId);
    }
}
