namespace Marketplace.Services.DocumentsAPI.Models
{
	public class FileData
	{
		public string fileName { get; set; }
        public string contentType { get; set; }
        public byte[] content { get; set; }

    }
}
