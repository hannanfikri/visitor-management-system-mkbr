using System.ComponentModel.DataAnnotations;

namespace Visitor.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
