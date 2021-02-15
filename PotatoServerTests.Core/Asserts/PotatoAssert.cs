using PotatoServerTestsCore.Exceptions;
using PotatoServerTestsCore.Models;
using System.Net;

namespace PotatoServerTestsCore.Asserts
{
    public class PotatoAssert
    {
        public static void EqualStatusCode<T>(HttpStatusCode statusCode, ApiResponse<T> apiResponse)
        {
            if (statusCode == apiResponse.StatusCode)
                return;

            throw new PotatoEqualException(statusCode, apiResponse.StatusCode, apiResponse.ValueString);
        }
    }
}
