using System.Collections.Generic;
using MvvmHelpers;
using Visitor.Models.NavigationMenu;

namespace Visitor.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}