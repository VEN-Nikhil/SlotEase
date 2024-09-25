namespace SlotEase.Domain.Message;

public class NotificationMessage<T>
{
    public string MetaMethodName { get; set; }
    public T PayLoad { get; set; }
}
