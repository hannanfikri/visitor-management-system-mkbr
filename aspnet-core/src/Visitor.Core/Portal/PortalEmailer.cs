using Abp.Configuration;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.UI;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Visitor.Appointment;
using Visitor.Configuration;
using Visitor.Net.Emailing;

namespace Visitor.core.Portal
{
    public class PortalEmailer: VisitorServiceBase , IPortalEmailer , ITransientDependency
    {
        private readonly ISettingManager _settingManager;
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly IEmailSender _emailSender;
        private readonly IAppConfigurationAccessor _configurationAccessor;

        public PortalEmailer(
            ISettingManager settingManager,
            IEmailTemplateProvider emailTemplateProvider,
            IEmailSender emailSender,
            IAppConfigurationAccessor configurationAccessor
        )
        {
            _settingManager = settingManager;
            _emailTemplateProvider = emailTemplateProvider;
            _emailSender = emailSender;
            _configurationAccessor = configurationAccessor;
        }

        public async Task SendEmailDetailBookingAsync(AppointmentEnt appointment)
        {
            await CheckMailSettingsEmptyOrNull();

            var emailTemplate = GetTitleAndSubTitle(null, appointment.AppRefNo, L("AppointmentDetail"));
            var mailMessage = new StringBuilder();

            //new
            var visitorInfo = new StringBuilder();
            var appointmentInfo = new StringBuilder();
            var dateTimeInfo = new StringBuilder();

            /*var serviceInfo = new StringBuilder();
            var timeInfo = new StringBuilder();
            var bookingCheck = new StringBuilder();
            var ServiceTranslationDto = service.Translations.FirstOrDefault();*/

            visitorInfo.AppendLine("<td style='border-bottom: 1px solid #dee2e6 ; color: #222; padding: 10px 0px;'>");
            visitorInfo.AppendLine(L("VisitorDetails"));
            visitorInfo.AppendLine("</td>");

            visitorInfo.AppendLine("<td style='border-bottom: 1px solid #dee2e6 ; color: #222; padding: 10px 0px;'>");
            visitorInfo.AppendLine(appointment.Title + " " + appointment.FullName + "<br>" + appointment.IdentityCard + "<br>" + appointment.PhoneNo + "<br>" + appointment.Email );
            visitorInfo.AppendLine("</td>");


            dateTimeInfo.AppendLine("<td style='border-bottom: 1px solid #dee2e6 ; color: #222; padding: 10px 0px;'>");
            dateTimeInfo.AppendLine(L("Date"));
            dateTimeInfo.AppendLine("</td>");

            dateTimeInfo.AppendLine("<td style='border-bottom: 1px solid #dee2e6 ; color: #222; padding: 10px 0px;'>");
            dateTimeInfo.AppendLine(appointment.AppDateTime.ToString("dddd, dd MMMM yyyy hh:mm tt"));
            dateTimeInfo.AppendLine("</td>");

            appointmentInfo.AppendLine("<td style='border-bottom: 1px solid #dee2e6 ; color: #222; padding: 10px 0px;'>");
            appointmentInfo.AppendLine(L("AppointmentDetails"));
            appointmentInfo.AppendLine("</td>");

            appointmentInfo.AppendLine("<td style='border-bottom: 1px solid #dee2e6 ; color: #222; padding: 10px 0px;'>");
            appointmentInfo.AppendLine(appointment.Tower + "<br>" + appointment.CompanyName + "<br>" + appointment.Department + "<br>"
            + appointment.Level + "<br>" + appointment.OfficerToMeet + "<br>" + appointment.PurposeOfVisit );
            appointmentInfo.AppendLine("</td>");


            

            

           /* bookingCheck.AppendLine("<td style='border-bottom: 1px solid #dee2e6 ; color: #222; padding: 10px 0px;'>");
            bookingCheck.AppendLine(L("CheckURL") + booking.BookingRefNo);
            bookingCheck.AppendLine("</td>");

            timeInfo.AppendLine(L("AppointmentTimeArrival"));*/


            //From here on, you can implement your platform-dependent byte[]-to-image code 

            //e.g. Windows 10 - Full .NET Framework


            await ReplaceBodyAndSend(appointment.Email, L("YourBookingDetail"), emailTemplate, mailMessage, visitorInfo,appointmentInfo, dateTimeInfo);

        }

        private async Task CheckMailSettingsEmptyOrNull()
        {
#if DEBUG
            return;
#endif
            if (
                (await _settingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress)).IsNullOrEmpty() ||
                (await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Host)).IsNullOrEmpty()
            )
            {
                throw new UserFriendlyException(L("SMTPSettingsNotProvidedWarningText"));
            }

            if ((await _settingManager.GetSettingValueAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)))
            {
                return;
            }

            if (
                (await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.UserName)).IsNullOrEmpty() ||
                (await _settingManager.GetSettingValueAsync(EmailSettingNames.Smtp.Password)).IsNullOrEmpty()
            )
            {
                throw new UserFriendlyException(L("SMTPSettingsNotProvidedWarningText"));
            }
        }
        private StringBuilder GetTitleAndSubTitle(int? tenantId, string title, string subTitle)
        {
            var emailTemplate = new StringBuilder(_emailTemplateProvider.GetAppointmentEmailTemplate(tenantId));
            emailTemplate.Replace("{EMAIL_TITLE}", title);
            emailTemplate.Replace("{EMAIL_SUB_TITLE}", subTitle);

            return emailTemplate;
        }
        private async Task ReplaceBodyAndSend(string emailAddress, string subject, StringBuilder emailTemplate, StringBuilder mailMessage,
            StringBuilder visitorInfo, StringBuilder appointmentInfo, StringBuilder dateTimeInfo)
        {
            emailTemplate.Replace("{EMAIL_BODY}", mailMessage.ToString());
            emailTemplate.Replace("{VISITOR_INFO}", visitorInfo.ToString());
            emailTemplate.Replace("{APPOINTMENT_INFO}", appointmentInfo.ToString());
            emailTemplate.Replace("{DATETIME_INFO}", dateTimeInfo.ToString());
           /* emailTemplate.Replace("{PERSONAL_INFO}", personalInfo.ToString());
            emailTemplate.Replace("{ARRIVAL_INFO}", timeInfo.ToString());
            emailTemplate.Replace("{CHECK_URL}", bookingCheck.ToString());*/


            await _emailSender.SendAsync(new MailMessage
            {
                To = { emailAddress },
                Subject = subject,
                Body = emailTemplate.ToString(),
                IsBodyHtml = true,
            });
        }
    }
}
