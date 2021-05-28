using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interface.Repository;
using MISA.CukCuk.Core.Interface.Service;
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
using MISA.CukCuk.Core;

namespace MISA.CukCuk.Core.Services
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


        #endregion

        #region Methods
        /// <summary>
        /// Hàm đọc thông tin file excel.
        /// </summary>
        /// <param name="file">file excel.</param>
        /// <param name="cancellationToken">Token hủy</param>
        /// <returns>Danh sách các khách hàng và lỗi của từng khách hàng.</returns>
        public async Task<List<CustomerImport>> ReadFromExcel(IFormFile file, CancellationToken cancellationToken)
        {
            var customersImport = new List<CustomerImport>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int rowNumber = 3; rowNumber <= rowCount; rowNumber++)
                    {
                        var customer = new Customer()
                        {
                            CustomerCode = GetValue(worksheet.Cells[rowNumber, 1].Value),
                            FullName = GetValue(worksheet.Cells[rowNumber, 2].Value),
                            MemberCardCode = GetValue(worksheet.Cells[rowNumber, 3].Value),
                            PhoneNumber = GetValue(worksheet.Cells[rowNumber, 5].Value),
                            DateOfBirth = ParseDate(worksheet.Cells[rowNumber, 6].Value),
                            CompanyName = GetValue(worksheet.Cells[rowNumber, 7].Value),
                            CompanyTaxCode = GetValue(worksheet.Cells[rowNumber, 8].Value),
                            Email = GetValue(worksheet.Cells[rowNumber, 9].Value),
                            Address = GetValue(worksheet.Cells[rowNumber, 10].Value),
                            Note = GetValue(worksheet.Cells[rowNumber, 11].Value)
                        };

                        var customerImport = new CustomerImport();
                        customerImport.Data = customer;

                        // check trong file Excel import
                        if (customersImport.Any())
                        {
                            // Check mã khách hàng có trùng với khách hàng khác trong tệp nhập khẩu hay không.
                            for (int i = 0; i < customersImport.Count; i++)
                            {
                                if (customersImport[i].Data.CustomerCode == customer.CustomerCode)
                                {
                                    customerImport.Errors.Add("Mã khách hàng đã trùng với khách hàng khác trong tệp nhập khẩu.");
                                    break;
                                }

                            }

                            // check SĐT có trùng với SĐT đã tồn tại trong tệp nhập khẩu hay không.
                            for (int i = 0; i < customersImport.Count; i++)
                            {
                                if (customersImport[i].Data.PhoneNumber == customer.PhoneNumber)
                                {
                                    customerImport.Errors.Add("SĐT đã trùng với SĐT của khách hàng khác trong tệp nhập khẩu.");
                                    break;
                                }

                            }
                        }

                        // check mã khách hàng đã tồn tại trong hệ thống.
                        if (_customerRepository.CheckCustomerCodeExist(customer.CustomerCode))
                        {
                            customerImport.Errors.Add("Mã khách hàng đã tồn tại trong hệ thống.");
                        }

                        // check số điện thoại đã tồn tại trong hệ thống.
                        if (_customerRepository.CheckPhoneNumberExist(customer.PhoneNumber))
                        {
                            customerImport.Errors.Add("SĐT đã có trong hệ thống.");
                        }

                        // check nhóm khách hàng có tồn tại trên hệ thống không.
                        string customerGroupName = GetValue(worksheet.Cells[rowNumber, 4].Value);
                        if (_customerRepository.GetCustomerGroupByName(customerGroupName) == null)
                        {
                            customerImport.Errors.Add("Nhóm khách hàng không có trong hệ thống.");
                        }


                        customersImport.Add(customerImport);
                    }
                }
            }


            return customersImport;
        }

        /// <summary>
        /// Hàm chuyển giá trị object từ excel thành kiểu string.
        /// </summary>
        /// <param name="valueObj">Giá trị cần chuyển</param>
        /// <returns>Chuỗi string.</returns>
        private string GetValue(object valueObj)
        {
            if (valueObj is null)
            {
                return null;
            }
            return valueObj.ToString().Trim();
        }

        /// <summary>
        /// Hàm parse date string thành kiểu DateTime.
        /// </summary>
        /// <param name="valueObj">dateString</param>
        /// <returns>DateTime</returns>
        private DateTime? ParseDate(object valueObj)
        {
            string valueStr = GetValue(valueObj);
            if (valueObj is null)
            {
                return null;
            }
            return DateTime.ParseExact(valueStr, formats: new string[] { "dd/MM/yyyy", "MM/yyyy", "yyyy" }, CultureInfo.InvariantCulture);
        }



        #endregion
    }
}
