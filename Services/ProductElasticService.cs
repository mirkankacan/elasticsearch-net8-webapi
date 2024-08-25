using Bogus;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elasticsearch.WebAPI.Dtos;
using Elasticsearch.WebAPI.Models;

namespace Elasticsearch.WebAPI.Services
{
    public class ProductElasticService : IProductElasticService
    {
        private readonly ElasticsearchClient _client;
        public ProductElasticService()
        {
            var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
                  .DefaultIndex("products");
            _client = new ElasticsearchClient(settings);
        }
        public async Task<CreateResponse> Create(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            Product product = new Product()
            {
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                Stock = createProductDto.Stock,
                Description = createProductDto.Description,
            };
            CreateRequest<Product> createRequest = new CreateRequest<Product>(product.Id.ToString())
            {
                Document = product
            };
            CreateResponse createResponse = await _client.CreateAsync(createRequest, cancellationToken);
            return createResponse;
        }

        public async Task<DeleteResponse> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            DeleteResponse deleteResponse = await _client.DeleteAsync("products", id, cancellationToken);
            return deleteResponse;
        }

        public async Task<SearchResponse<Product>> GetAll(CancellationToken cancellationToken)
        {
            SearchRequest searchRequest = new SearchRequest("products")
            {
                Size = 1000,
                Sort = new List<SortOptions>
                {
                    SortOptions.Field(new Field("name.keyword"), new FieldSort()
                    {
                        Order=SortOrder.Desc,
                    })
                },
            };
            SearchResponse<Product> searchResponse = await _client.SearchAsync<Product>(searchRequest, cancellationToken);
            return searchResponse;
        }

        public async Task<SearchResponse<Product>> GetByNameFuzzy(string value, CancellationToken cancellationToken)
        {
            SearchRequest searchRequest = new SearchRequest("products")
            {
                Size = 1000,
                Sort = new List<SortOptions>
                {
                    SortOptions.Field(new Field("name.keyword"), new FieldSort()
                    {
                        Order=SortOrder.Desc,
                    })
                },
                Query = new FuzzyQuery(new Field("name"))
                {
                    Value = value
                }
            };
            SearchResponse<Product> searchResponse = await _client.SearchAsync<Product>(searchRequest, cancellationToken);
            return searchResponse;
        }

        public async Task<SearchResponse<Product>> GetByNameMatch(string query, CancellationToken cancellationToken)
        {
            SearchRequest searchRequest = new SearchRequest("products")
            {
                Size = 1000,
                Sort = new List<SortOptions>
                {
                    SortOptions.Field(new Field("name.keyword"), new FieldSort()
                    {
                        Order=SortOrder.Desc,
                    })
                },
                Query = new MatchQuery(new Field("name"))
                {
                    Query = query
                }
            };
            SearchResponse<Product> searchResponse = await _client.SearchAsync<Product>(searchRequest, cancellationToken);
            return searchResponse;
        }

        public async Task<SearchResponse<Product>> GetByNameWildcard(string value, CancellationToken cancellationToken)
        {
            SearchRequest searchRequest = new SearchRequest("products")
            {
                Size = 1000,
                Sort = new List<SortOptions>
                {
                    SortOptions.Field(new Field("name.keyword"), new FieldSort()
                    {
                        Order=SortOrder.Desc,
                    })
                },
                Query = new WildcardQuery(new Field("name"))
                {
                    Value = $"*{value}*"
                }
            };
            SearchResponse<Product> searchResponse = await _client.SearchAsync<Product>(searchRequest, cancellationToken);
            return searchResponse;
        }

        public async Task SeedData(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                Faker faker = new Faker();
                Product product = new Product()
                {
                    Name = faker.Commerce.ProductName(),
                    Price = Convert.ToDecimal(faker.Commerce.Price()),
                    Stock = faker.Commerce.Random.Int(1, 20),
                    Description = faker.Commerce.ProductDescription(),
                };
                CreateRequest<Product> createRequest = new CreateRequest<Product>(product.Id.ToString())
                {
                    Document = product
                };
                await _client.CreateAsync(createRequest, cancellationToken);
            }
        }

        public async Task<UpdateResponse<Product>> Update(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            UpdateRequest<Product, UpdateProductDto> updateRequest = new UpdateRequest<Product, UpdateProductDto>("products", updateProductDto.Id.ToString())
            {
                Doc = updateProductDto
            };
            UpdateResponse<Product> updateResponse = await _client.UpdateAsync(updateRequest, cancellationToken);
            return updateResponse;
        }
    }
}
