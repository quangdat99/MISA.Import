using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Enum
{
    /// <summary>
    /// Trạng thái của object
    /// </summary>
    /// CreatedBy: DQDAT (23/05/2021)
    public enum EntityState
    {
        /// <summary>
        /// Thêm
        /// </summary>
        Add = 1,

        /// <summary>
        /// Sửa
        /// </summary>
        Update = 2,

        /// <summary>
        /// Xóa
        /// </summary>
        Delete = 3
    }

    /// <summary>
    /// Giới tính
    /// </summary>
    ///  CreatedBy: DQDAT (23/05/2021)
    public enum GenderEnum
    {
        /// <summary>
        /// Nam
        /// </summary>
        Male = 1,

        /// <summary>
        /// Nữ
        /// </summary>
        Female = 0,

        /// <summary>
        /// Khác
        /// </summary>
        Other = 2
    }
}
