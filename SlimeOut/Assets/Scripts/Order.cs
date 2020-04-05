using MongoDB.Bson;
using MongoDB.Driver.Builders;

/// <summary>
/// defines the order in which info is stored in the database
/// </summary>
public class Order
{
    public enum STATE : byte
    {
        NEW = 0,
        CANCELLED = 1,
        COMPLETE = 2
    }
    public ObjectId Id { get; set; }
    public string U { get; set; } // for username
    public string E { get; set; } // for email
    public string P { get; set; } // for encrypted password
    public string S { get; set; } // for slime name
    public string SC { get; set; } // for slime color

}



