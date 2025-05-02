using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductParams
    {
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;

        public int?BrandId {  get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
        public string? SearchValue { get; set; }
        public int PageIndex { get; set; } = 1;

        //public int PageSize { get; set; }

        private int pageSize = DefaultPageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
