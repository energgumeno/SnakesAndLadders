namespace SnakesAndLaddersLibrary.AnimationMessage;

public interface IMessage
{
    string Animation { get; set; }
    string Sender { get; set; }
    List<KeyValuePair<string, string>> Values { get; set; }
}