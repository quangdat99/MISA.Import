using MISA.CukCuk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Interface.Repository
{
    /// <summary>
    /// Repository khách hàng
    /// </summary>
    /// CreatedBy: DQDAT (20/05/2021)
    public interface ICustomerRepository
    {

        /// <summary>
        /// Kiểm tra mã kahchs hàng đã tồn tại hay chưa
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns>true - đã tồn tại; false - không tồn tại</returns>
        /// CreatedBy: DQDAT (22/05/2021)
        bool CheckCustomerCodeExist(string customerCode, Guid? customerId = null);

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại trong hệ thống chưa
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        /// CreatedBy: DQDAT (22/05/2021)
        bool CheckPhoneNumberExist(string phoneNumber);
    }
}
