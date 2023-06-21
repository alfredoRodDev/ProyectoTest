using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.WrappersModels
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T>? Data { get; set; }
        public int? PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri? FirstPage { get; set; }
        public Uri? PreviousPage { get; set; }
        public Uri? NextPage { get; set; }
        public Uri? LastPage { get; set; }
    }

    public class PaginationHelper
    {
        public static PaginationResponse<T> CreatePaginatedResponse<T>(string baseUrl, int totalRecords, int pageSize, int pageNumber, IEnumerable<T> response)
        {

            //total pages
            var totalPages = totalRecords == 0 ? 1 : (int)Math.Ceiling(totalRecords / (double)pageSize);

            var firstPage = (totalPages > 1) ? UriPagService.GetPaginationUri(baseUrl, 1) : null;
            var nextPage = (totalPages > 1 && pageNumber < totalPages) ? UriPagService.GetPaginationUri(baseUrl, pageNumber + 1) : null;
            var previousPage = (pageNumber - 1 >= 1 && pageNumber <= totalPages) ? UriPagService.GetPaginationUri(baseUrl, pageNumber - 1) : null;
            var lastPage = (totalPages > 1) ? UriPagService.GetPaginationUri(baseUrl, totalPages) : null;

            var paginationResponse = new PaginationResponse<T>();
            paginationResponse.Data = response;
            paginationResponse.PageNumber = pageNumber;
            paginationResponse.TotalPages = totalPages;
            paginationResponse.TotalRecords = totalRecords;
            paginationResponse.FirstPage = firstPage;
            paginationResponse.PreviousPage = previousPage;
            paginationResponse.NextPage = nextPage;
            paginationResponse.LastPage = lastPage;

            return paginationResponse;

        }
    }

    public class UriPagService
    {
        public static Uri GetPaginationUri(string baseUrl, int pageNumber = 1)
        {

            var modifiedUri = QueryHelpers.AddQueryString(baseUrl, "pageNumber", pageNumber.ToString());

            return new Uri(modifiedUri);
        }



    }
}
