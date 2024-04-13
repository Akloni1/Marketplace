namespace Marketplace.Web.Services.IServices
{
    public interface IDocumentService
    {
        Task<T> GetDocumentByCode<T>(string code, string token);
    }
}
