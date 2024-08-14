using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Repository.Base;
using TLS.Service.Base;
using TLS.ViewModels.Contact;
using TLS.ViewModels.DataTable;
using TLS.ViewModels.ServeyVm;
using System.Linq;
using TLS.Common.Enums.Extension;
using Microsoft.EntityFrameworkCore;

namespace TLS.Service.SurveyService
{
    public class SurveyService : BaseService<Survey>, ISurveyService
    {
        private IRepository<Survey> _surveyRepository;
        public SurveyService(IRepository<Survey> surveyRepository) : base(surveyRepository)
        { 
            _surveyRepository = surveyRepository;
        }
        public async Task<DataTableResponse<SurveyViewModel>> GetAllPaging(GetAllSurveyPageRequest r)
        {
            var surveys = _surveyRepository.GetAll();
            var searchKeyWord = r.Keyword != null ? r.Keyword.ToLower() : "";
            var surveyFilter = from n in surveys
                                where string.IsNullOrEmpty(r.Keyword) ||
                                n.CompanyName.ToLower().Contains(searchKeyWord) ||
                                n.CustomerName.ToLower().Contains(searchKeyWord)
                                orderby n.CreatedDate descending
                                select n;

            var surveyPaging = await surveyFilter.Skip(r.StartIndex)
                                         .Take(r.PageSize)
                                         .Select(x => new SurveyViewModel()
                                         {
                                             Id = x.Id,
                                             CustomerName = x.CustomerName,
                                             CompanyName = x.CompanyName,
                                             BusinessField = x.BusinessField != null ? x.BusinessField.DisplayName() : "",
                                             ConversionMediaChannel = x.ConversionMediaChannel != null ? x.ConversionMediaChannel.DisplayName() : "",
                                             ConversionMediaChannelLink = x.ConversionMediaChannelLink,
                                             CustomerPhone = x.CustomerPhone,
                                             MainMediaChannelLink = x.MainMediaChannelLink,
                                             MediaChannel = x.MediaChannel != null ? x.MediaChannel.DisplayName() : "",
                                             Title = x.Title != null ? x.Title.DisplayName() : "",
                                             CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                                             
                                         })
                                         .ToListAsync();

            for (int i = 0; i < surveyPaging.Count; i++)
            {
                surveyPaging[i].Index = i + 1 + r.StartIndex;
            }

            return new DataTableResponse<SurveyViewModel>()
            {
                data = surveyPaging,
                draw = r.Draw,
                recordsFiltered = await surveyFilter.CountAsync(),
                recordsTotal = await surveys.CountAsync()
            };
        }
    }
}
