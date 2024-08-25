using Elastic.Clients.Elasticsearch;
using Elasticsearch.WebAPI.Dtos;
using Elasticsearch.WebAPI.Models;

namespace Elasticsearch.WebAPI.Services
{
    public interface IProductElasticService
    {
        Task<CreateResponse> Create(CreateProductDto createProductDto, CancellationToken cancellationToken);
        Task<UpdateResponse<Product>> Update(UpdateProductDto updateProductDto, CancellationToken cancellationToken);
        Task SeedData(CancellationToken cancellationToken);
        Task<SearchResponse<Product>> GetAll(CancellationToken cancellationToken);
        Task<DeleteResponse> DeleteById(Guid id, CancellationToken cancellationToken);
        Task<SearchResponse<Product>> GetByNameWildcard(string value, CancellationToken cancellationToken);
        Task<SearchResponse<Product>> GetByNameMatch(string query, CancellationToken cancellationToken);
        Task<SearchResponse<Product>> GetByNameFuzzy(string value, CancellationToken cancellationToken);
    }
}
