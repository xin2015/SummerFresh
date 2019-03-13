
namespace SummerFresh.Data.Mapping
{
    public interface IMappingProvider
    {
        bool Supports(string dbProviderName);

        Tables ReadTables(Dao dao);

        string CreateTableCommand(Table mapping);

        string GetDropTableCommand(Table table);

        ISqlCommand CreateSelectCommand(TableMapping mapping, object parameters);

        ISqlCommand CreateSelectCountCommand(TableMapping mapping, object parameters);

        ISqlCommand CreateSelectAllCommand(TableMapping mapping, object parameter = null);

        ISqlCommand CreateInsertCommand(TableMapping mapping, object parameters);

        ISqlCommand CreateUpdateCommand(TableMapping mapping, object parameters);

        ISqlCommand CreateUpdateCommand(TableMapping mapping, object parameters, string[] fields, bool inclusive);

        ISqlCommand CreateDeleteCommand(TableMapping mapping, object parameters);

        ISqlCommand CreateBatchUpdateCommand(TableMapping mapping, object parameters, object whereParameter);

        ISqlCommand CreateBatchDeleteCommand(TableMapping mapping, object whereParameter);
    }
}