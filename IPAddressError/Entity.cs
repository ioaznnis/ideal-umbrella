using System.Net;

namespace IPAddressError;

public class Entity
{
    public Guid Id { get; set; }
    
    public IPAddress IpAddress { get; set; } = null!;
}