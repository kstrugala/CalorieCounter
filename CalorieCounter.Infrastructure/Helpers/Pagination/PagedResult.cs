using System.Collections.Generic;

namespace CalorieCounter.Infrastructure.Helpers.Pagination
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Results { get; private set; }

        public PaginationInformation Pagination { get; private set; }

       
        public PagedResult(IEnumerable<T> results, int count, int currentPage, int pageSize, int totalPages)
        {
            Pagination =  new PaginationInformation(count, currentPage, pageSize, totalPages);
            Results = results;
        }

    }
}
