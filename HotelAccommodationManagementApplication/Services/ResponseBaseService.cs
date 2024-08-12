using HotelAccommodationManagementDomain.Entities;

namespace HotelAccommodationManagementApplication.Services
{
    public abstract class ResponseBaseService
    {
        protected async Task<Response<T>> HandleRequest<T>(Func<Task<T>> action)
        {
            var resp = new Response<T>();

            try
            {
                resp.Data = await action();
                resp.Status = "200";
                resp.Message = "Petición exitosa";
            }
            catch (Exception e)
            {
                resp.Message = e.Message;
                resp.Status = "500";
            }

            return resp;
        }
    }
}
