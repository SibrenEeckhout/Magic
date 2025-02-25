﻿
namespace Howest.MagicCards.Shared.Filters
{
    public class PaginationFilter
    {
        const int _maxPageSize = 120;
        
        private int _pageSize = _maxPageSize;
        private int _pageNumber = 1;

        public PaginationFilter(int pageSize)
        {
            _pageSize = pageSize;
        }

        public PaginationFilter()
        {
            _pageSize = 120;
        }
        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = (value < 1) ? 1 : value; }
        }

        public int PageSize 
        { 
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize || value < 1) ? _maxPageSize : value; } 
        } 
    }
}
