﻿using System.Collections.Generic;

namespace KillBill.Client.Net.Model
{
    public class KillBillObject : object
    {
        public KillBillObject()
        {
        }

        public KillBillObject(List<AuditLog> auditLogs)
        {
            AuditLogs = auditLogs;
        }

        public List<AuditLog> AuditLogs { get; set; }
    }
}