namespace DataAccess.DataAccess
{
    public interface ISqlDataAccess
    {
        public Task<IEnumerable<T>> LoadWithJoin<T, T2, U>(
            string query,
            U parameters,
            Func<T, T2, T> splitFunc,
            string splitOn,
            string connectionId = "Default");

        Task<IEnumerable<T>> LoadWithQuery<T, U>(string query, U parameters, string connectionId = "Default");
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
    }
}