using Marketplace.Web.Models;
using Marketplace.Web.Services.IServices;
using Newtonsoft.Json.Linq;

namespace Marketplace.Web.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        public DocumentService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> GetDocumentByCode<T>(string code, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.DocumentAPIBase + "/api/document/" + code,
                AccessToken = token
            });
        }
    }
}
