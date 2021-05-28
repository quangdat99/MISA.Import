using Microsoft.AspNetCore.Http;
using MISA.CukCuk.Core.Entities;
using MISA.Import.Core.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interface.Service
{
    /// <summary>
    /// Service khách hàng
    /// </summary>
    /// CreatedBy: DQDAT (20/05/2021)
    public interface ICustomerService 
    {
        /// <summary>
        /// Hàm đọc thông tin file excel.
        /// </summary>
        /// <param name="file">file excel.</param>
        /// <param name="cancellationToken">Token hủy</param>
        /// <returns>Danh sách các khách hàng và lỗi của từng khách hàng.</returns>
        public Task<List<CustomerImport>> ReadFromExcel(IFormFile file, CancellationToken cancellationToken);

    }
}
