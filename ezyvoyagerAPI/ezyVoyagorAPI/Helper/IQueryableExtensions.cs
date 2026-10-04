using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ezyvoyagerAPI.Helper
{
    //public static class IQueryableExtensions
    //{
    //    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
    //        this IQueryable<T> query,
    //        int pageNumber,
    //        int pageSize,
    //        string sortBy = null,
    //        bool sortDescending = false,
    //        Expression<Func<T, bool>> filter = null)
    //    {
    //        if (pageNumber <= 0) pageNumber = 1;
    //        if (pageSize <= 0) pageSize = 10;

    //        // Apply filtering
    //        if (filter != null)
    //        {
    //            query = query.Where(filter);
    //        }

    //        // Apply sorting
    //        //if (!string.IsNullOrEmpty(sortBy))
    //        //{
    //        //    query = sortDescending
    //        //        ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
    //        //        : query.OrderBy(e => EF.Property<object>(e, sortBy));
    //        //}

    //        var totalRecords = await query.CountAsync();
    //        var data = await query
    //            .Skip((pageNumber - 1) * pageSize)
    //            .Take(pageSize)
    //            .ToListAsync();

    //        return new PagedResult<T>(data, totalRecords, pageNumber, pageSize);
    //    }
    //}

    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert to hex string
                StringBuilder builder = new StringBuilder();
                foreach (var b in hash)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
    }
}