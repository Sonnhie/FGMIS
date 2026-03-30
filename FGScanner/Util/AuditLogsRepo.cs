using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Util
{
    public class AuditLogsRepo
    {
        private readonly db_connection _Connection;

        public AuditLogsRepo()
        {
            _Connection = new db_connection();
        }


    }
}
