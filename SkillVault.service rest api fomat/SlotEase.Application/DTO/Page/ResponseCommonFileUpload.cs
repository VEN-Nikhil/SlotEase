namespace SlotEase.Application.DTO.Page;

public class ResponseCommonFileUpload
{
    public string docName { get; set; }
    public string fileName { get; set; }
    public string refFileName { get; set; }
    public string FileType { get; set; }
    public string docPath { get; set; }
    public byte[] docByte { get; set; }
}
