namespace SlotEase.Application.DTO.Attachment;

public class DownloadAsZipDto
{
    public byte[] FileBytes { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public string DocPath { get; set; }

}
