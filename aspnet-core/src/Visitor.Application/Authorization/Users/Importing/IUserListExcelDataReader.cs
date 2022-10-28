using System.Collections.Generic;
using Visitor.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Visitor.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
