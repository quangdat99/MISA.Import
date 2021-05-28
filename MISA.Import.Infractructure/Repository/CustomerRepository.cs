using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace MISA.CukCuk.Infrastructure.Repository
{
   /// <summary>
   /// Kho chứa khách hàng
   /// </summary>
   /// CreatedBy: dqdat (27/05/2021)
    public class CustomerRepository : ICustomerRepository
    {

        #region Declare
        public DbConnection DbConnection { get => new MySqlConnection(_connectionString); }
        string _connectionString;
        IConfiguration _configuration;
        DynamicParameters Parameters;
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="configuration"></param>
        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            Parameters = new DynamicParameters();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm check mã khách hàng đã tồn tại trong hệ thống chưa ?
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// CreatedBy: dqdat (27/05/2021)
        public bool CheckCustomerCodeExist(string customerCode)
        {
            Parameters.Add("@m_CustomerCode", customerCode);
            var isExist = DbConnection.ExecuteScalar<bool>("Proc_CheckCustomerCodeExist", param: Parameters, commandType: CommandType.StoredProcedure);
            return isExist;
        }

        /// <summary>
        /// Hàm check số điện thoại đã tồn tại trong hệ thống chưa?
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public bool CheckPhoneNumberExist(string phoneNumber)
        {
            Parameters.Add("@m_PhoneNumber", phoneNumber);
            var isExist = DbConnection.ExecuteScalar<bool>("Proc_CheckPhoneNumberExist", param: Parameters, commandType: CommandType.StoredProcedure);
            return isExist;
        }



        /// <summary>
        /// Thực hiện gán giá trị tham số đầu vào của store với các property của object
        /// </summary>
        /// <param name="customer"></param>
        /// CreatedBy: DQDAT (22/5/2021)
        void MappingProcParametersValueWithObject(Customer customer)
        {
            // Lấy ra các properties của đối tượng:
            var properties = typeof(Customer).GetProperties();

            // Duyệt từng Property:
            foreach (var property in properties)
            {
                // Lấy ra value:
                var value = property.GetValue(customer);

                // Lấy ra tên của Property:
                var propertyName = property.Name;

                // Đặt tên cho tham số đầu vào:
                Parameters.Add($"@m_{propertyName}", value);
            }
        }

        public bool CheckCustomerGroupExist(Guid customerGroupId)
        {
            Parameters.Add("@m_CustomerGroupId", customerGroupId);
            var isExist = DbConnection.ExecuteScalar<bool>("Proc_CheckCustomerGroupExist", param: Parameters, commandType: CommandType.StoredProcedure);
            return isExist;
        }

        public CustomerGroup GetCustomerGroupByName(string customerGroupName)
        {
            Parameters.Add("@m_CustomerGroupName", customerGroupName);
            var customerGroup = DbConnection.QueryFirstOrDefault<CustomerGroup>("Proc_GetCustomerGroupByName", param: Parameters, commandType: CommandType.StoredProcedure);
            return customerGroup;
        }
        #endregion

    }
}
