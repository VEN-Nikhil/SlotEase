using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlotEase.Domain.DTO;

public class PermissionDto
{

    [Key]
    public long Id { get; set; }

    public string ClientId { get; set; }

    [ForeignKey("Parent")]
    public long? ParentId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public virtual PermissionDto Parent { get; set; }


    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool IsDeleted { get; set; }
}
