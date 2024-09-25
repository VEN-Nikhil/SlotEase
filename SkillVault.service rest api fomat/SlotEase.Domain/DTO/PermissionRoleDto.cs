namespace SlotEase.Domain.DTO;

public class PermissionRoleDto
{

    public long PermissionId { get; set; }

    public long RoleId { get; set; }
    public virtual PermissionDto ClientPermission { get; set; }
    public virtual RoleDto<long> IdentityRole { get; set; }

    public long PermissionRoleId { get; set; }
}
