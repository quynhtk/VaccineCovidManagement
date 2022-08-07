using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.NhaSanXuats
{
    public class GetNhaSanXuatInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
