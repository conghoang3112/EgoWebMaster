using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Service.Base;
using TLS.ViewModels.Contact;
using TLS.ViewModels.DataTable;

namespace TLS.Service.Catalog
{
    public interface IContactService : IService<Contact>
    {
        public Task<DataTableResponse<ContactVm>> GetAllPaging(GetAllContactPageRequest input);
    }
}
