﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace VaccineCovidManagement.ChiTietNhaps
{
    public class GetVaccineTonKhoLookup : EntityDto<Guid>
    {
        public string TenVaccineTonKho { get; set; }
    }
}
