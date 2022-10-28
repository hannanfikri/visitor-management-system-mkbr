using System.Collections.Generic;
using Abp;
using Visitor.Chat.Dto;
using Visitor.Dto;

namespace Visitor.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
