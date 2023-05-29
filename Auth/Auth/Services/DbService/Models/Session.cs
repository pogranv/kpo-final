namespace Auth.Services.PostgresDbService.Models;

public partial class Session
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string SessionToken { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
}
