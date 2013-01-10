using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webf.SvcDependencies
{
    public interface IDbServices<DBModel>
    {
        DBModel DataBase { get; } 
    
        bool saveModeltoDB<TableType>(Type tableType, TableType obj);
    }
}