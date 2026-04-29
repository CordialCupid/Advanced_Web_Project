using System.Text.Json.Serialization;

namespace Musichord.Models.Entities;

public class Friendship
{
    public int Id {get;set;}
    public string Status {get;set;} = String.Empty;
    public string SenderId {get;set;} = String.Empty;
    public string SenderHandle {get;set;} = String.Empty;
    [JsonIgnore]
    public ApplicationUser? Sender {get;set;}
    public string ReceiverId {get;set;} = String.Empty;
    public string ReceiverHandle {get;set;} = String.Empty;
    [JsonIgnore]
    public ApplicationUser? Receiver {get;set;}
}