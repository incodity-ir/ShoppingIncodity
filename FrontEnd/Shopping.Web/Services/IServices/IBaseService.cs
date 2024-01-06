

namespace Shopping.Web.Services.IServices
{
    public interface IBaseService:IDisposable
    {
         ResponseDto responseModel {get; set;}
         Task<T> SendAync<T>(ApiRequest  apiRequest);
    }
}