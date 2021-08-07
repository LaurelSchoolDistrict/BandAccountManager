using BandAccountManager.BlazorApp.Shared.Accounts;

using CsvHelper.Configuration;

namespace BandAccountManager.BlazorApp.Server.Mapping
{
    public class NewAccountDtoCsvMap : ClassMap<NewAccountDto>
    {
        public NewAccountDtoCsvMap()
        {
            Map(a => a.StudentEmail).Name(nameof(NewAccountDto.StudentEmail));
            Map(a => a.StudentName).Name(nameof(NewAccountDto.StudentName));
            Map(a => a.StartingBalance).Name(nameof(NewAccountDto.StartingBalance)).Default(0, useOnConversionFailure: true);
        }
    }
}
