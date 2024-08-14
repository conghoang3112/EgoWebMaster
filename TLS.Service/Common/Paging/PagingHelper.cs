using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLS.ViewModels.Common;
using TLS.ViewModels.DataTable;

namespace TLS.Service.Common.Paging
{

    public static class PagingHelper<T, U> where U : DataTableRow
    {
        public static async Task<PagedResultModel<U>> PageList(IQueryable<T> itemsInput, int pageIndex, int pageSize, IMapper _mapper)
        {
            return new PagedResultModel<U>()
            {
                Items = itemsInput.Skip((pageIndex - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList()
                                        .Select(b => _mapper.Map<U>(b))
                                        .ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRecords = await itemsInput.CountAsync()
            };
        }

        public static async Task<DataTableResponse<U>> PageListDatatable(
           IQueryable<T> listItem,
           IQueryable<T> listItemFiltered,
           int startIndex,
           int pageSize,
           int draw,
           IMapper _mapper)
        {
            var listItemPaging = listItemFiltered.Skip(startIndex)
                                        .Take(pageSize)
                                        .Select(b => _mapper.Map<U>(b))
                                        .ToList();

            for (int i = 0; i < listItemPaging.Count; i++)
            {
                listItemPaging[i].Index = i + 1 + startIndex;
            }

            return new DataTableResponse<U>()
            {
                data = listItemPaging,
                draw = draw,
                recordsFiltered = await listItemFiltered.CountAsync(),
                recordsTotal = await listItem.CountAsync()
            };
        }
    }
}
