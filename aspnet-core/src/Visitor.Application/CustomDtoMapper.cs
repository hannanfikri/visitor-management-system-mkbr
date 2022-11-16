using Visitor.Company.Dtos;
using Visitor.Company;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using IdentityServer4.Extensions;
using Visitor.Auditing.Dto;
using Visitor.Authorization.Accounts.Dto;
using Visitor.Authorization.Delegation;
using Visitor.Authorization.Permissions.Dto;
using Visitor.Authorization.Roles;
using Visitor.Authorization.Roles.Dto;
using Visitor.Authorization.Users;
using Visitor.Authorization.Users.Delegation.Dto;
using Visitor.Authorization.Users.Dto;
using Visitor.Authorization.Users.Importing.Dto;
using Visitor.Authorization.Users.Profile.Dto;
using Visitor.Blacklist.Dtos;
using Visitor.Chat;
using Visitor.Chat.Dto;
using Visitor.DynamicEntityProperties.Dto;
using Visitor.Editions;
using Visitor.Editions.Dto;
using Visitor.Friendships;
using Visitor.Friendships.Cache;
using Visitor.Friendships.Dto;
using Visitor.Localization.Dto;
using Visitor.MultiTenancy;
using Visitor.MultiTenancy.Dto;
using Visitor.MultiTenancy.HostDashboard.Dto;
using Visitor.MultiTenancy.Payments;
using Visitor.MultiTenancy.Payments.Dto;
using Visitor.Notifications.Dto;
using Visitor.Organizations.Dto;
using Visitor.Sessions.Dto;
using Visitor.WebHooks.Dto;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Visitor.Appointment;
using Visitor.Tower.Dtos;
using Visitor.PurposeOfVisit.Dtos;
using Visitor.Status.Dtos;
using Visitor.Title.Dtos;
using Visitor.Level.Dtos;
using Visitor.Departments.Dtos;

namespace Visitor
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {

            configuration.CreateMap<CreateOrEditAppointmentDto, Appointment.AppointmentEnt>().ReverseMap();
            configuration.CreateMap<AppointmentDto, Appointment.AppointmentEnt>().ReverseMap();

            //Tower
            configuration.CreateMap<CreateOrEditTowerDto, Tower.TowerEnt>().ReverseMap();
            configuration.CreateMap<TowerDto, Tower.TowerEnt>().ReverseMap();
            //PurposeOfVisit
            configuration.CreateMap<CreateOrEditPurposeOfVisitDto, PurposeOfVisit.PurposeOfVisitEnt>().ReverseMap();
            configuration.CreateMap<PurposeOfVisitDto, PurposeOfVisit.PurposeOfVisitEnt>().ReverseMap();
            //Status
            configuration.CreateMap<CreateOrEditStatusDto, Status.StatusEnt>().ReverseMap();
            configuration.CreateMap<StatusDto, Status.StatusEnt>().ReverseMap();
            //Title
            configuration.CreateMap<CreateOrEditTitleDto, Title.TitleEnt>().ReverseMap();
            configuration.CreateMap<TitleDto, Title.TitleEnt>().ReverseMap();



            //Level
            configuration.CreateMap<CreateOrEditLevelDto, Level.LevelEnt>().ReverseMap();
            configuration.CreateMap<LevelDto, Level.LevelEnt>().ReverseMap();

            //Blacklist
            configuration.CreateMap<CreateOrEditBlacklistDto, Blacklist.BlacklistEnt>().ReverseMap();
            configuration.CreateMap<BlacklistDto, Blacklist.BlacklistEnt>().ReverseMap();

            configuration.CreateMap<CreateOrEditCompanyDto, Company.CompanyEnt>().ReverseMap();
            configuration.CreateMap<CompanyDto, Company.CompanyEnt>().ReverseMap();

            //Appointment
            configuration.CreateMap<CreateOrEditAppointmentDto, Appointment.AppointmentEnt>().ReverseMap();
            configuration.CreateMap<AppointmentDto, Appointment.AppointmentEnt>().ReverseMap();

            //Department
            configuration.CreateMap<CreateOrEditDepartmentDto, Departments.Department>().ReverseMap();
            configuration.CreateMap<DepartmentDto, Departments.Department>().ReverseMap();

            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.DisplayName.IsNullOrEmpty() ? entity.DynamicProperty.PropertyName : entity.DynamicProperty.DisplayName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();

            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}