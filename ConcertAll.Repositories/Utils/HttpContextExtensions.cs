using Microsoft.AspNetCore.Http;

namespace ConcertAll.Repositories.Utils
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationHeader<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));

            double totalRecords = queryable.ToList().Count;
            httpContext.Response.Headers.Add("TotalRecordsQuantity", totalRecords.ToString());

        }
    }
}
