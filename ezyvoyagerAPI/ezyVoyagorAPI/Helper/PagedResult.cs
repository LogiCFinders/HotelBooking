using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ezyVoyagorAPI.Helper
{
    public class PagedResult<T>
    {
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PagedResult(IEnumerable<T> data, int totalRecords, int pageNumber, int pageSize)
        {
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            Data = data;
        }
    }
}