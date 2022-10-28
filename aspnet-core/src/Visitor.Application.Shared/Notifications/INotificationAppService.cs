using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Notifications;
using Visitor.Notifications.Dto;

namespace Visitor.Notifications
{
    public interface INotificationAppService : IApplicationService
    {
        Task<GetNotificationsOutput> GetUserNotifications(GetUserNotificationsInput input);
        
        Task SetAllNotificationsAsRead();

        Task<SetNotificationAsReadOutput> SetNotificationAsRead(EntityDto<Guid> input);

        Task<GetNotificationSettingsOutput> GetNotificationSettings();

        Task UpdateNotificationSettings(UpdateNotificationSettingsInput input);

        Task DeleteNotification(EntityDto<Guid> input);

        Task DeleteAllUserNotifications(DeleteAllUserNotificationsInput input);

        Task CreateMassNotification(CreateMassNotificationInput input);

        List<string> GetAllNotifiers();

        Task<GetPublishedNotificationsOutput> GetNotificationsPublishedByUser(GetPublishedNotificationsInput input);
    }
}
