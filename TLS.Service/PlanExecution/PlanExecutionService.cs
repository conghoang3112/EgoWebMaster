using System;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Repository.Base;
using TLS.Service.Base;
using TLS.ViewModels.DataTable;
using TLS.ViewModels.PlanExecutionVm;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TLS.Service.PlanExecution
{
    public class PlanExecutionService : BaseService<PlanExecutionInfo>, IPlanExecutionService
    {
        private IRepository<PlanExecutionInfo> _planExecutionRepository;
        public PlanExecutionService(IRepository<PlanExecutionInfo> planExecutionRepository) : base(planExecutionRepository)
        {
            _planExecutionRepository = planExecutionRepository;
        }
        public async Task<DataTableResponse<PlanExecutionModel>> GetAllPaging(GetAllPlanExecutionPageRequest r)
        {
            var planExecutions = _planExecutionRepository.GetAll();
            var searchKeyWord = r.Keyword != null ? r.Keyword.ToLower() : "";
            var planExecutionFilter = from n in planExecutions
                               where string.IsNullOrEmpty(r.Keyword) ||
                               n.CompanyName.ToLower().Contains(searchKeyWord) ||
                               n.PhoneNumber.ToLower().Contains(searchKeyWord)
                               orderby n.Createddate descending
                               select n;

            var planExecutionPaging = await planExecutionFilter.Skip(r.StartIndex)
                                         .Take(r.PageSize)
                                         .Select(x => new PlanExecutionModel()
                                         {
                                             Id = x.Id,
                                             CompanyName = x.CompanyName,
                                             PhoneNumber = x.PhoneNumber,
                                             Budget = x.Budget,
                                             Createddate = x.Createddate.ToString("dd/MM/yyyy")

                                         })
                                         .ToListAsync();

            for (int i = 0; i < planExecutionPaging.Count; i++)
            {
                planExecutionPaging[i].Index = i + 1 + r.StartIndex;
            }

            return new DataTableResponse<PlanExecutionModel>()
            {
                data = planExecutionPaging,
                draw = r.Draw,
                recordsFiltered = await planExecutionFilter.CountAsync(),
                recordsTotal = await planExecutions.CountAsync()
            };
        }
    }
}
