using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Service.Base;
using TLS.ViewModels.Common;
using TLS.ViewModels.DataTable;
using TLS.ViewModels.News;

namespace TLS.Service.NewsService
{
    public interface INewsService : IService<News>
    {
        Task<ApiResponseDto> Create(CreateNewsInputDto input);
        Task<ApiResponseDto> Delete(int id);
        Task<ApiResponseDto> Edit(EditNewInputDto input);
        Task<DataTableResponse<NewsVm>> GetAllPaging(GetAllNewsPageRequest input);
        Task<NewsDetailVm> GetNewsDetailById(int newsId);
        Task<NewsPageVm> GetNewsPageVm(SiteNewsPageRequest input);
    }
}