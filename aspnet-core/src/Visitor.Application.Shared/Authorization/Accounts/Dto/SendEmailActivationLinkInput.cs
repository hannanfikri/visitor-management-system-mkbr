using System.ComponentModel.DataAnnotations;

namespace Visitor.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}