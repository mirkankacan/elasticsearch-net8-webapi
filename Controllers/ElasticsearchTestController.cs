using Elastic.Clients.Elasticsearch;
using Elasticsearch.WebAPI.Dtos;
using Elasticsearch.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Elasticsearch.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticsearchTestController : ControllerBase
    {
        private readonly IProductElasticService _elasticService;

        public ElasticsearchTestController(IProductElasticService elasticService)
        {
            _elasticService = elasticService;
        }
        [HttpGet("GetByNameWildcard")]
        public async Task<IActionResult> GetByNameWildcard(string value, CancellationToken cancellationToken)
        {
            var response = await _elasticService.GetByNameWildcard(value, cancellationToken);
            switch (response.IsValidResponse && response.IsSuccess())
            {
                case true:
                    return Ok(response.Documents);
                    break;
                case false:
                    return BadRequest();
                    break;
                default:
                    break;
            }
        }
        [HttpGet("GetByNameMatch")]
        public async Task<IActionResult> GetByNameMatch(string query, CancellationToken cancellationToken)
        {
            var response = await _elasticService.GetByNameMatch(query, cancellationToken);
            switch (response.IsValidResponse && response.IsSuccess())
            {
                case true:
                    return Ok(response.Documents);
                    break;
                case false:
                    return BadRequest();
                    break;
                default:
                    break;
            }
        }
        [HttpGet("GetByNameFuzzy")]
        public async Task<IActionResult> GetByNameFuzzy(string value, CancellationToken cancellationToken)
        {
            var response = await _elasticService.GetByNameFuzzy(value, cancellationToken);
            switch (response.IsValidResponse && response.IsSuccess())
            {
                case true:
                    return Ok(response.Documents);
                    break;
                case false:
                    return BadRequest();
                    break;
                default:
                    break;
            }
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _elasticService.GetAll(cancellationToken);
            switch (response.IsValidResponse && response.IsSuccess())
            {
                case true:
                    return Ok(response.Documents);
                    break;
                case false:
                    return BadRequest();
                    break;
                default:
                    break;
            }
        }
        [HttpGet("SeedData")]
        public async Task<IActionResult> SeedData(CancellationToken cancellationToken)
        {
            await _elasticService.SeedData(cancellationToken);
            return Created();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            var response = await _elasticService.Create(createProductDto, cancellationToken);
            switch (response.IsValidResponse && response.IsSuccess())
            {
                case true:
                    return Ok(response.Result);
                    break;
                case false:
                    return BadRequest();
                    break;
                default:
                    break;
            }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var response = await _elasticService.Update(updateProductDto, cancellationToken);
            switch (response.IsValidResponse && response.IsSuccess())
            {
                case true:
                    return Ok(response.Result);
                    break;
                case false:
                    return BadRequest();
                    break;
                default:
                    break;
            }
        }
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _elasticService.DeleteById(id, cancellationToken);
            switch (response.IsValidResponse && response.IsSuccess())
            {
                case true:
                    return Ok(response.Result);
                    break;
                case false:
                    return BadRequest();
                    break;
                default:
                    break;
            }
        }
    }
}
