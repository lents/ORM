﻿namespace DataAccess.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadWithQuery<T, U>(string query, U parameters, string connectionId = "Default");
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
    }
}