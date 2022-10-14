namespace SnakesAndLaddersLibrary.AnimationMessage
{
    public class Message : IMessage
    {
        public string Sender { get; set; }
        public string Animation { get; set; }
        public List<KeyValuePair<string, string>> Values { get; set; }

        public Message()
        {
            Sender = string.Empty;
            Animation = string.Empty;
            Values = new List<KeyValuePair<string, string>>();

        }


    }
}
