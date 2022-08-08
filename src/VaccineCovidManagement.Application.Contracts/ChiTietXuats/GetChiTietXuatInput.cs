using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.ChiTietXuats
{
    public class GetChiTietXuatInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
