using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public class GetChiTietNhapInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
