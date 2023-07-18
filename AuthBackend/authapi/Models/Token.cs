
namespace authapi.Models;

public class Token
{
    public string access_token { get; set; } = string.Empty;
    public int expires_in { get; set; }
    public string refresh_token { get; set;} = string.Empty;
    public int refresh_expires_in { get; set; }
    public string token_type { get; set;} = string.Empty;
    public string session_state { get; set; } = string.Empty;

}
