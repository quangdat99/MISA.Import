using MISA.Import.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Import.Core.Interface.Repository
{
    /// <summary>
    /// Repository khách hàng
    /// </summary>
    /// CreatedBy: dqdat (28/05/2021)
    public interface ICustomerRepository
    {

        /// <summary>
        /// Hàm insert một khách hàng vào db.
        /// </summary>
        /// <param name="customer">Thông tin khách hàng.</param>
        /// <returns>Số khách hàng thêm thành công.</returns>
        /// CreatedBy: dqdat (28/05/2021)
        public int InsertCustomer(Customer customer);

        /// <summary>
        /// Lấy toàn bộ dữ liệu khách hàng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: dqdat (28/05/2021)
        public IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Lấy toàn bộ dữ liệu nhóm khách hàng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: dqdat (28/05/2021)
        public IEnumerable<CustomerGroup> GetCustomerGroups();

    }
}
