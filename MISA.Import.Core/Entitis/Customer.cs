using MISA.CukCuk.Core.Attributes;
using MISA.CukCuk.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Entities
{
    /// <summary>
    /// Thông tin khách hàng
    /// </summary>
    /// CreatedBy: DQDAT (20/5/2021)
    public class Customer :BaseEntity
    {
        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [PropertyRequired]
        [PropertyMaxLength(10)]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        [PropertyRequired]
        public string FullName { get; set; }

        /// <summary>
        /// Id nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Giới tính: 1 - Nam, 0 - Nữ, 2 - Khác
        /// </summary>
        public GenderEnum? Gender { get; set; } 

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email của khách hàng
        /// </summary>

        [PropertyEmailFormat]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số tiền ghi nợ
        /// </summary>
        public string DebitAmout { get; set; }

        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public bool? IsStopFollow { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string GenderName
        {
            get
            {
                return Gender switch
                {
                    GenderEnum.Male => Properties.Resources.Male,
                    GenderEnum.Female => Properties.Resources.Female,
                    GenderEnum.Other => Properties.Resources.Other,
                    _ => Properties.Resources.Unknown
                };
            }
        }

        #endregion

    }
}