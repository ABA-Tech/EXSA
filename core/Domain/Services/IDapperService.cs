namespace Domain.Services
{
    public interface IDapperService
    {
        IEnumerable<T> Query<T>(string sql, object parameters = null);
        T QuerySingleOrDefault<T>(string sql, object parameters = null);
    }
}
