﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.WebUI.Client.Pages
{
    [Authorize]
    public partial class Index : ComponentBase
    {
    }
}
