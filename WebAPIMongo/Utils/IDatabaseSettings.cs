namespace WebAPIMongo.Utils
{
    public interface IDatabaseSettings
    {
        string ClientCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AddressCollectionName { get; set; }
    }
}
