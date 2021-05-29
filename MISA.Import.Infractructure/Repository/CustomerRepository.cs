using MISA.Import.Core.Entities;
using MISA.Import.Core.Interface.Repository;
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

namespace MISA.Import.Infrastructure.Repository
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
        /// Lấy toàn bộ dữ liệu khách hàng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: dqdat (28/05/2021)
        public IEnumerable<Customer> GetCustomers()
        {
            var customers = DbConnection.Query<Customer>("Proc_GetCustomers", commandType: CommandType.StoredProcedure);
            return customers;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu nhóm khách hàng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: dqdat (28/05/2021)
        public IEnumerable<CustomerGroup> GetCustomerGroups()
        {
            var customerGroups = DbConnection.Query<CustomerGroup>("Proc_GetCustomerGroups", commandType: CommandType.StoredProcedure);
            return customerGroups;
        }

        /// <summary>
        /// Hàm insert một khách hàng vào db.
        /// </summary>
        /// <param name="customer">Thông tin khách hàng.</param>
        /// <returns>Số khách hàng thêm thành công.</returns>
        /// CreatedBy: dqdat (28/05/2021)
        public int InsertCustomer(Customer customer)
        {
            MappingProcParametersValueWithObject(customer);
            var rowsAffect = DbConnection.Execute("Proc_InsertCustomer", Parameters, commandType: CommandType.StoredProcedure);
            return rowsAffect;
        }

        /// <summary>
        /// Thực hiện gán giá trị tham số đầu vào của store với các property của object
        /// </summary>
        /// <param name="customer"></param>
        /// CreatedBy: dqdat (28/05/2021)
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

        #endregion

    }
}
