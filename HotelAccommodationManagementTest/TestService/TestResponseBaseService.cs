using HotelAccommodationManagementApplication.Services;
using HotelAccommodationManagementDomain.Entities;
using System;
using System.Threading.Tasks;

public class TestResponseBaseService : ResponseBaseService
{
    public Task<Response<T>> ExecuteRequest<T>(Func<Task<T>> action) => HandleRequest(action);
}
