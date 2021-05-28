using Microsoft.AspNetCore.Http;
using MISA.Import.Core.Entities;
using MISA.Import.Core.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Import.Core.Interface.Service
{
    /// <summary>
    /// Service khách hàng
    /// </summary>
    /// CreatedBy: dqdat (28/05/2021)
    public interface ICustomerService 
    {
        /// <summary>
        /// Hàm đọc thông tin file excel.
        /// </summary>
        /// <param name="file">file excel.</param>
        /// <param name="cancellationToken">Token hủy</param>
        /// <returns>Danh sách các khách hàng và lỗi của từng khách hàng.</returns>
        /// CreatedBy: dqdat (28/05/2021)
        public Task<List<CustomerImport>> ReadFile(IFormFile file, CancellationToken cancellationToken);

        /// <summary>
        /// Hàm thêm nhiều khách hàng không có lỗi vào db.
        /// </summary>
        /// <param name="customersImport">Danh sách các khách hàng và lỗi của từng khách hàng</param>
        /// <returns>Số khách hàng thêm thành công.</returns>
        /// CreatedBy: dqdat (28/05/2021)
        public int InsertCustomers(List<CustomerImport> customersImport);
    }
}
