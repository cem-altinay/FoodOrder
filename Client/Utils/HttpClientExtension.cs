
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoodOrder.Shared.CustomException;
using FoodOrder.Shared.ResponseModel;

namespace FoodOrder.Client.Utils
{
    public static class HttpClientExtension
    {
        public async static Task<T> GetServiceResponseAsync<T>(this HttpClient Client, string Url, bool ThrowSuccessException = false)
        {
            var httpRes = await Client.GetFromJsonAsync<ServiceResponse<T>>(Url);

            return !httpRes.Success && ThrowSuccessException ? throw new ApiException(httpRes.Message) : httpRes.Value;
        }

             public async static Task<TResult> PostGetServiceResponseAsync<TResult, TValue>(this HttpClient Client, string Url, TValue Value, bool ThrowSuccessException = false)
        {
            var httpRes = await Client.PostAsJsonAsync(Url, Value);

            if (httpRes.IsSuccessStatusCode)
            {
                var res = await httpRes.Content.ReadFromJsonAsync<ServiceResponse<TResult>>();

                return !res.Success && ThrowSuccessException ? throw new ApiException(res.Message) : res.Value;
            }

            throw new HttpException(httpRes.StatusCode.ToString());
        }

        public async static Task<BaseResponse> PostGetBaseResponseAsync<TValue>(this HttpClient Client, string Url, TValue Value, bool ThrowSuccessException = false)
        {
            var httpRes = await Client.PostAsJsonAsync(Url, Value);

            if (httpRes.IsSuccessStatusCode)
            {
                var res = await httpRes.Content.ReadFromJsonAsync<BaseResponse>();

                return !res.Success && ThrowSuccessException ? throw new ApiException(res.Message) : res;
            }

            throw new HttpException(httpRes.StatusCode.ToString());
        }
    }
}