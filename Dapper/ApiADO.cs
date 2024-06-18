
using DataAccess.ADO.Data;
using DataAccess.ADO.Models;

namespace WebApi
{
    public static class ApiADO
    {
        public static void ConfigureApiADO(this WebApplication app)
        {
            app.MapGet("/Users", GetUsers);
            app.MapGet("/Users/{id:int}", GetUser);
            app.MapGet("/Users/{filter}", FindUsers).WithName("FindUsers");
            app.MapPost("Users", InsertUser);
            app.MapPut("/Users", UpdateUser);
            app.MapDelete("/Users", DeleteUser);
        }

        private static async Task<IResult> GetUsers(IUserData data)
        {
            try
            {
                return Results.Ok( await data.GetUsers());
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetUser(int id, IUserData data)
        {
            try
            {
                var user = await data.GetUser(id);
                if (user == null) { return Results.NotFound(id); }
                return Results.Ok(user);
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> FindUsers(string filter, IUserData data)
        {
            try
            {
                var user = await data.GetUsersByFilter(filter);
                if (user == null) { return Results.NotFound(filter); }
                return Results.Ok(user);
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }
        private static async Task<IResult> InsertUser(UserModel user, IUserData data)
        {
            try
            {
                await data.InsertUser(user);
                
                return Results.Ok();
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
        {
            try
            {
                await data.UpdateUser(user);

                return Results.Ok();
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> DeleteUser(int id, IUserData data)
        {
            try
            {
                await data.DeleteUser(id);

                return Results.Ok();
            }
            catch (Exception ex)
            {

                return Results.Problem(ex.Message);
            }
        }


    }
}
