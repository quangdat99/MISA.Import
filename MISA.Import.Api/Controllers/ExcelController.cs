using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MISA.Import.Core.Entities;
using MISA.Import.Core.Interface.Service;
using MISA.Import.Core.Entitis;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MISA.Import.Core.Interface.Repository;

namespace MISA.Import.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        /// <summary>
        /// Khai báo
        /// </summary>
        ICustomerService _customerService;
        ICustomerRepository _customerRepository;


        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="customerService"></param>
        public ExcelController(ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _customerService = customerService;
            _customerRepository = customerRepository;

        }


        /// <summary>
        /// Hàm đọc thông tin file excel.
        /// </summary>
        /// <param name="formFile">file excel.</param>
        /// <param name="cancellationToken">Token hủy</param>
        /// <returns>Danh sách các khách hàng và lỗi của từng khách hàng.</returns>
        /// CreatedBy: dqdat (28/05/2021)
        [HttpPost("ReadFile")]
        public async Task<IActionResult> ReadFile(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                throw new Exception(Core.Properties.Resources.MsgFileNull);
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception(Core.Properties.Resources.MsgMalformedFile);
            }


            var customersImport = await _customerService.ReadFile(formFile, cancellationToken);
            return Ok(customersImport);
        }

        /// <summary>
        /// Hàm thêm nhiều khách hàng không có lỗi vào db.
        /// </summary>
        /// <param name="customersImport">Danh sách các khách hàng và lỗi của từng khách hàng</param>
        /// <returns>Số khách hàng thêm thành công.</returns>
        /// CreatedBy: dqdat (28/05/2021)
        [HttpPost("InsertCustomers")]
        public IActionResult InsertCustomers(List<CustomerImport> customersImport)
        {
            int numberRecordAddSuccess = _customerService.InsertCustomers(customersImport);
            return Ok(new
            {
                totalRecord = customersImport.Count(),
                numberRecordAddSuccess = numberRecordAddSuccess
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_customerRepository.GetCustomers());
        }

    }
}
