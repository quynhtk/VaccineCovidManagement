using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.DonViYTes
{
    public class GetDonViYTeInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
