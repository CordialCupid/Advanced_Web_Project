namespace Musichord.Models.Entities;

public static class GlobalFriendGraph
{
    public static Dictionary<string, List<string>> graph {get;set;} = new();

    public static async Task GraphInit(ICollection<ApplicationUser> users, ICollection<Friendship> relationships)
    {
        graph = new();
        foreach(var user in users)
        {
            graph.Add(user.Id, new List<string>());  
        }
        foreach (Friendship relations in relationships)
        {
            AddEdge(relations.SenderId, relations.ReceiverId);
        }
    }

    public static void AddEdge(string sender, string receiver)
    {
        if (String.IsNullOrEmpty(sender) || String.IsNullOrEmpty(receiver))
        {
            throw new Exception("Sender and Receiver cannot be null or empty.");
        }
        graph[sender].Add(receiver);
        graph[receiver].Add(sender);
    }

    public static List<string> GetFriends(string userId)
    {
        if (String.IsNullOrEmpty(userId))
        {
            throw new Exception("Please provide a valid user id.");
        }
        return graph[userId];
    }

    public static async Task<List<string?>> GetNonFriends(string userId, ICollection<ApplicationUser> exceptUsers)
    {
        if (String.IsNullOrEmpty(userId))
        {
            throw new Exception("Please ensure the user id and name are valid before retrieving those who are not friends.");
        }
        var nons = exceptUsers.Select(u => u.Id).Except(graph[userId]).ToList() ?? new List<string?>();
        return nons;
    }
}