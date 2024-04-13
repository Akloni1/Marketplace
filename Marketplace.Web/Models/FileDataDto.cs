namespace Marketplace.Web.Models
{
	public class FileDataDto
	{
        public string fileName { get; set; }
        public string contentType { get; set; }
        public byte[] content { get; set; }
    }
}
