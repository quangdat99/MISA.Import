using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MISA.Import.Core.Entities;
using MISA.Import.Core.Interface.Repository;
using MISA.Import.Core.Interface.Service;
using MISA.Import.Core.Entitis;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MISA.Import.Core;

namespace MISA.Import.Core.Services
{
    /// <summary>
    /// Dịch vụ khách hàng
    /// </summary>
    /// CreatedBy: dqdat (27/05/2021)
    public class CustomerService :  ICustomerService
    {
        #region Declare
        /// <summary>
        /// Kho chứa kahchs hàng
        /// </summary>
        ICustomerRepository _customerRepository;
        #endregion


        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="customerRepository"></param>
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Hàm thêm nhiều khách hàng không có lỗi vào db.
        /// </summary>
        /// <param name="customersImport">Danh sách các khách hàng và lỗi của từng khách hàng</param>
        /// <returns>Số khách hàng thêm thành công.</returns>
        /// CreatedBy: dbhuan (06/05/2021)
        public int InsertCustomers(List<CustomerImport> customersImport)
        {
            int i = 0;
            foreach (var customerImport in customersImport)
            {
                if (customerImport.Errors.Count() == 0)
                {
                    i++;
                    _customerRepository.InsertCustomer(customerImport.Data);
                }
            }
            return i;
        }


        #endregion

        #region Methods
        /// <summary>
        /// Hàm đọc thông tin file excel.
        /// </summary>
        /// <param name="file">file excel.</param>
        /// <param name="cancellationToken">Token hủy</param>
        /// <returns>Danh sách các khách hàng và lỗi của từng khách hàng.</returns>
        public async Task<List<CustomerImport>> ReadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var customersImport = new List<CustomerImport>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        var customer = new Customer()
                        {
                            CustomerCode = ParseString(worksheet.Cells[row, 1].Value),
                            FullName = ParseString(worksheet.Cells[row, 2].Value),
                            MemberCardCode = ParseString(worksheet.Cells[row, 3].Value),
                            PhoneNumber = ParseString(worksheet.Cells[row, 5].Value),
                            DateOfBirth = ParseDate(worksheet.Cells[row, 6].Value),
                            CompanyName = ParseString(worksheet.Cells[row, 7].Value),
                            CompanyTaxCode = ParseString(worksheet.Cells[row, 8].Value),
                            Email = ParseString(worksheet.Cells[row, 9].Value),
                            Address = ParseString(worksheet.Cells[row, 10].Value),
                            Note = ParseString(worksheet.Cells[row, 11].Value)
                        };

                        var customerImport = new CustomerImport();
                        

                        // check trong file Excel import
                        if (customersImport.Any())
                        {
                            // Check mã khách hàng có trùng với khách hàng khác trong tệp nhập khẩu hay không.
                            for (int i = 0; i < customersImport.Count; i++)
                            {
                                if (customersImport[i].Data.CustomerCode == customer.CustomerCode)
                                {
                                    customerImport.Errors.Add(Properties.Resources.MsgDuplicateCustomerCodeImport);
                                    break;
                                }

                            }

                            // check SĐT có trùng với SĐT đã tồn tại trong tệp nhập khẩu hay không.
                            for (int i = 0; i < customersImport.Count; i++)
                            {
                                if (customersImport[i].Data.PhoneNumber == customer.PhoneNumber)
                                {
                                    customerImport.Errors.Add(Properties.Resources.MsgDuplicatePhoneNumberImport);
                                    break;
                                }

                            }
                        }

                        // check mã khách hàng đã tồn tại trong hệ thống.
                        if (_customerRepository.CheckCustomerCodeExist(customer.CustomerCode))
                        {
                            customerImport.Errors.Add(Properties.Resources.MsgDuplicateCustomerCodeExist);
                        }

                        // check số điện thoại đã tồn tại trong hệ thống.
                        if (_customerRepository.CheckPhoneNumberExist(customer.PhoneNumber))
                        {
                            customerImport.Errors.Add(Properties.Resources.MsgDuplicatePhoneNumberExist);
                        }

                        // check nhóm khách hàng có tồn tại trên hệ thống không.
                        string customerGroupName = ParseString(worksheet.Cells[row, 4].Value);
                        var customerGroup = _customerRepository.GetCustomerGroupByName(customerGroupName);
                        if (customerGroup == null)
                        {
                            customerImport.Errors.Add(Properties.Resources.MsgCustomerGroupIsExist);
                        }
                        else
                        {
                            customer.CustomerGroupId = customerGroup.CustomerGroupId;
                            customer.CustomerGroupName = customerGroupName;
                        }

                        customerImport.Data = customer;
                        customersImport.Add(customerImport);
                    }
                }
            }


            return customersImport;
        }

        /// <summary>
        /// Hàm chuyển giá trị object từ excel thành kiểu string.
        /// </summary>
        /// <param name="obj">Giá trị cần chuyển</param>
        /// <returns>Chuỗi string.</returns>
        private string ParseString(object obj)
        {
            if (obj is null)
            {
                return null;
            }
            return obj.ToString().Trim();
        }

        /// <summary>
        /// Hàm parse date string thành kiểu DateTime.
        /// </summary>
        /// <param name="obj">dateString</param>
        /// <returns>DateTime</returns>
        private DateTime? ParseDate(object obj)
        {
            string valueStr = ParseString(obj);
            if (obj is null)
            {
                return null;
            }
            return DateTime.ParseExact(valueStr, new string[] { "dd/MM/yyyy", "MM/yyyy", "yyyy", "d/M/yyyy", "dd/yyyy", "dd/M/yyyy", "d/MM/yyyy", "M/yyyy", "d/yyyy" }, new CultureInfo("en-US"),
                                            DateTimeStyles.None);
        }



        #endregion
    }
}
