using MongoDB.Bson;
using MongoDB.Driver.Builders;

public static class Utility
{
    public const string EMAIL_PATTERN = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
    public const string USERNAME_AND_DISCRIMINATOR_PATTERN = @"^[a-zA-Z0-9]{4,20}#[0-9]{4}$";
    public const string USERNAME_PATTERN = @"^[a-zA-Z0-9]{4,20}$";
    public const string RANDOM_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";


}

public class Order
{
    public enum STATE : byte
    {
        NEW = 0,
        CANCELLED = 1,
        COMPLETE = 2
    }
    public ObjectId Id { get; set; }
    public string U { get; set; }
    public string E { get; set; }
    public string P { get; set; }
    public string S { get; set; }
    public string SC { get; set; }

}


