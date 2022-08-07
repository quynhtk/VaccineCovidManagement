﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace VaccineCovidManagement.Web.Pages;

public class IndexModel : VaccineCovidManagementPageModel
{
    private readonly IdentityUserAppService _identityUserAppService;

    public IndexModel(IdentityUserAppService identityUserAppService)
    {
        _identityUserAppService = identityUserAppService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var currentUser = CurrentUser.Id;
        if (currentUser == null)
        {
            return Redirect("/Account/Login");
        }
        else
        {
            return Redirect("/NhaSanXuats/Index");
        }
            
    }
}
