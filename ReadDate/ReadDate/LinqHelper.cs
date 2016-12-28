using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDate
{
    public class LinqHelper
    {
        public object GetSingleValue(string SelectType,DataTable Table,string selectSQL,string selectTitle)
        {
            DataRow[] T1 = Table.Select(selectSQL);
            switch (SelectType)
            {
                case "MRows":
                    return T1;
                    break;
                case "SRow":
                    return T1[0];
                    break;
                case "SValue":
                    return T1[0][selectTitle];
                    break;
                default:
                    return "";
            }
        }
    }
}
