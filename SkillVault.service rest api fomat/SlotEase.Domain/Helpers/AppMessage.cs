using SlotEase.Domain.Enums;

namespace SlotEase.Domain.Helpers;

public class AppMessage
{
    public string Code { get; set; }
    public MessageType Type { get; set; }
    public string Text { get; set; }
    public string Action { get; set; }
}
