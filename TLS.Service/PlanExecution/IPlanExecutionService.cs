using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Service.Base;
using TLS.ViewModels.DataTable;
using TLS.ViewModels.PlanExecutionVm;
using TLS.ViewModels.ServeyVm;

namespace TLS.Service.PlanExecution
{
    public interface IPlanExecutionService : IService<PlanExecutionInfo>
    {
        public Task<DataTableResponse<PlanExecutionModel>> GetAllPaging(GetAllPlanExecutionPageRequest request);
    }
}
