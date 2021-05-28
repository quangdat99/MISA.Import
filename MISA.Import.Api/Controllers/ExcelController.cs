using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MISA.CukCuk.Core.Entities;
using MISA.CukCuk.Core.Interface.Service;
using MISA.Import.Core.Entitis;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Import.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        ICustomerService _customerService;

        public ExcelController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost("ReadFileExcel")]
        public async Task<IActionResult> ReadFileExcel(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                throw new Exception("File rỗng");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("File không đúng định dạng");
            }


            var res = await _customerService.ReadFromExcel(formFile, cancellationToken);
            return Ok(res);
        }

        [HttpPost("InsertCustomers")]
        public IActionResult InsertCustomers(List<CustomerImport> customerImport)
        {

            return Ok(1);
        }

    }
}
