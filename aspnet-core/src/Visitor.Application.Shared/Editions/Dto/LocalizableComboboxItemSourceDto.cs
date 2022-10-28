using System.Collections.ObjectModel;

namespace Visitor.Editions.Dto
{
	//Mapped in CustomDtoMapper
	public class LocalizableComboboxItemSourceDto
	{
		public Collection<LocalizableComboboxItemDto> Items { get; set; }
	}
}