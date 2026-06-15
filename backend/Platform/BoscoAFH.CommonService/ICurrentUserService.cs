namespace BoscoAFH.CommonService
{
    /// <summary>
    /// Defines the interface for the current user service.
    /// </summary>
    public interface ICurrentUserService
    {
        long UserId { get; set; }
        string UserName { get; set; }
        int RoleId { get; set; }
        string ClientIPAddress { get; set; }
        Guid CorrelationId { get; set; }
        string DeviceType { get; set; }
        string BrowserName { get; set; }
        bool CanSeeAll { get; set; }
        CreateBaseDTO CreateBaseDTO { get; set; }
        ModifyBaseDTO ModifyBaseDTO { get; set; }
        DeleteBaseDTO DeleteBaseDTO { get; set; }
    }
    public class CurrentUserService : ICurrentUserService
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public Guid CorrelationId { get; set; }
        public int RoleId { get; set; }
        public bool CanSeeAll { get; set; }
        public string ClientIPAddress { get; set; } = "Unknown";
        public string DeviceType { get; set; } = "Unknown";
        public string BrowserName { get; set; } = "Unknown";
        public CreateBaseDTO CreateBaseDTO { get; set; } = new();
        public ModifyBaseDTO ModifyBaseDTO { get; set; } = new();
        public DeleteBaseDTO DeleteBaseDTO { get; set; } = new();
    }

    public class CreateBaseDTO
    {
        public long CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }
    }

    public class ModifyBaseDTO
    {
        public long ModifiedById { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }

    public class DeleteBaseDTO
    {
        public long ModifiedById { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = true;
    }

}
