using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Service.Base;
using TLS.ViewModels.Contact;
using TLS.ViewModels.DataTable;
using TLS.ViewModels.ServeyVm;

namespace TLS.Service.SurveyService
{
    public interface ISurveyService : IService<Survey>
    {
        public Task<DataTableResponse<SurveyViewModel>> GetAllPaging(GetAllSurveyPageRequest request);
    }
}
