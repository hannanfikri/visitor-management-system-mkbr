using System.Collections.Generic;
using Visitor.Authorization.Users.Importing.Dto;
using Visitor.Dto;

namespace Visitor.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
