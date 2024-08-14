using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Repository.Base;
using TLS.Service.Base;
using TLS.ViewModels.Contact;
using TLS.ViewModels.DataTable;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TLS.Service.Catalog
{
    public class ContactService : BaseService<Contact>, IContactService
    {
        public IRepository<Contact> _repository { get; set; }
        public ContactService(IRepository<Contact> baseReponsitory) : base(baseReponsitory)
        {
            _repository = baseReponsitory;
        }

        public async Task<DataTableResponse<ContactVm>> GetAllPaging(GetAllContactPageRequest input)
        {
            var contact = _repository.GetAll();
            var searchKeyWord = input.Keyword != null ? input.Keyword.ToLower() : "";
            var contactFilter = from n in contact
                                where string.IsNullOrEmpty(input.Keyword) ||
                                n.Name.ToLower().Contains(searchKeyWord) ||
                                n.PhoneNumber.ToLower().Contains(searchKeyWord)
                                orderby n.CreatedDate descending
                                select n;

            var contactPaging = await contactFilter.Skip(input.StartIndex)
                                         .Take(input.PageSize)
                                         .Select(x => new ContactVm()
                                         {
                                             Id = x.Id,
                                             CreatedDate = x.CreatedDate.ToString("dd/MM/yyyy"),
                                             Name = x.Name,
                                             BusinessAreas = x.BusinessAreas,
                                             PhoneNumber = x.PhoneNumber,
                                             SolutionOfInterest = x.SolutionOfInterest,
                                             Position = x.Position,
                                             Email = x.Email,
                                             Company = x.Company,
                                             Purpose = x.Purpose,
                                         })
                                         .ToListAsync();

            for (int i = 0; i < contactPaging.Count; i++)
            {
                contactPaging[i].Index = i + 1 + input.StartIndex;
            }

            return new DataTableResponse<ContactVm>()
            {
                data = contactPaging,
                draw = input.Draw,
                recordsFiltered = await contactFilter.CountAsync(),
                recordsTotal = await contact.CountAsync()
            };
        }

    }
}