using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginationResulte<TEntity>
    {
        public PaginationResulte(int _PageIndex, int _PageSize, int _TotalCount, IEnumerable<TEntity> _Data)
        {
            PageSize = _PageSize;
            TotalCount = _TotalCount;
            PageIndex = _PageIndex;
            Data = _Data;   
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public IEnumerable<TEntity> Data { get; set; }



    }
}
