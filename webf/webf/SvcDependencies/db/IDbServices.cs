﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webf.SvcDependencies
{
    public interface IDbServices
    {
        bool saveModeltoDB<TableType>(TableType obj);
    }
}