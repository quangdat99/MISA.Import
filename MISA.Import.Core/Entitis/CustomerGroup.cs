using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    /// <summary>
    /// Thông tin nhóm khách hàng
    /// </summary>
    /// CreatedBy: dqdat(27/05/2021)
    public class CustomerGroup: BaseEntity
    {
        /// <summary>
        /// Id của nhóm khách hàng
        /// </summary>
        public Guid CustomerGroupId { get; set; }

        /// <summary>
        /// Tên của nhóm khách hàng
        /// </summary>
        public string CustomerGroupName { get; set; }

        /// <summary>
        /// Mô tả nhóm khách hàng
        /// </summary>
        public string Description { get; set; }
    }
}
