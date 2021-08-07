using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using AutoMapper;

using CsvHelper;
using CsvHelper.Configuration;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BandAccountManager.BlazorApp.Shared.Accounts;
using BandAccountManager.BlazorApp.Shared.Authorization;
using BandAccountManager.Core.Accounts;
using BandAccountManager.Core.Common;
using BandAccountManager.BlazorApp.Server.Mapping;

namespace BandAccountManager.BlazorApp.Server.Api.Accounts.Batch
{
    public class ImportCsvRequest
    {
        [FromForm(Name = "file")]
        public IFormFile? File { get; set; }
    }

    public class ImportCsv : BaseAsyncEndpoint
        .WithRequest<ImportCsvRequest>
        .WithoutResponse
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public ImportCsv(IRepository<Account> accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        [Authorize(Roles.Administrator)]
        [HttpPost("api/accounts/_batch/$importCsv")]
        public override async Task<ActionResult> HandleAsync([FromRoute] ImportCsvRequest request, CancellationToken cancellationToken = default)
        {
            if (request.File is null)
            {
                return BadRequest();
            }

            var csvConfiguration = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                Quote = '"'
            };

            

            using (var fileStream = request.File.OpenReadStream())
            using (var fileReader = new StreamReader(fileStream))
            using (var csvReader = new CsvReader(fileReader, csvConfiguration))
            {
                csvReader.Context.RegisterClassMap<NewAccountDtoCsvMap>();

                await foreach (var accountDto in csvReader.GetRecordsAsync<NewAccountDto>())
                {
                    if (!await _accountRepository.Exists(accountDto.Id))
                    {
                        var account = _mapper.Map<Account>(accountDto);

                        await _accountRepository.Save(account);
                    }
                }
            }

            return Ok();
        }
    }
}
