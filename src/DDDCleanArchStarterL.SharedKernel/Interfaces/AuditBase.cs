using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DDDInvoicingCleanL.SharedKernel.Interfaces
{
    public class AuditBase
    {
      public  string AuditId { get; set; }
      public string AuditDataBeforeChanged { get; set; }
      public string AuditDataAfterChanged { get; set; }
      public string AuditEntityId { get; set; }
      public DateTime AuditTimeStamp { get; set; }
      public string AuditUserId { get; set; }
      public string AuditTenantId { get; set; }
      public string AuditAction { get; set; }
      public string AuditEntityType { get; set; }
      public string AuditEntityName { get; set; }
    }
}