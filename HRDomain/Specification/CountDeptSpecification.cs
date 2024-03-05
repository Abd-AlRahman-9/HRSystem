﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class CountDeptSpecification : GenericSpecification<Department>
    {
        public CountDeptSpecification(GetAllDeptsParams _params)
            :base
            (
                D =>(!_params.MngId.HasValue || D.ManagerId == _params.MngId.Value)
            )
        { }
    }
} 