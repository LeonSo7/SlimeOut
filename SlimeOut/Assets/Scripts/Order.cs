using MongoDB.Bson;

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
    public string O_username { get; set; } // for username
    public string O_email { get; set; } // for email
    public string O_password { get; set; } // for encrypted password
    public string O_slimename { get; set; } // for slime name
    public string O_slime_color { get; set; } // for slime color
    public int O_balance { get; set; } // for currency
    public int O_health { get; set; } // for health
    public int O_slime_level { get; set; } // for slime level
    public int O_hunger_level { get; set; } // for hunger level
    public int O_exp_level { get; set; } // for exp level
    public string[] O_item_strings { get; set; }
} 



