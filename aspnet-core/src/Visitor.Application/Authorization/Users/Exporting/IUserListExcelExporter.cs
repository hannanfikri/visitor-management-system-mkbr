using System.Collections.Generic;
using Visitor.Authorization.Users.Dto;
using Visitor.Dto;

namespace Visitor.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}