﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Specification
{
    public interface IPagination
    {
        public bool IsPaginationEnabled { get; set; }
        public int? PageSize { get; set; }
        public int? PageCount { get; set; }
    }
}