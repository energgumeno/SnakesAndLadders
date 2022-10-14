using System.Text;

namespace SnakesAndLaddersLibrary.AnimationMessage
{
    public class AnimationLogger : IAnimationLogger
    {

        public async Task AnimationMessage(Message message)
        {
            StringBuilder valuesToString = new StringBuilder();
            valuesToString.AppendLine();
            message.Values.ForEach(value => valuesToString.AppendLine($" <-- Key:'{value.Key}' Value:'{value.Value}' -->"));
            Console.WriteLine($"Object: '{message.Sender}' Performed: '{message.Animation}' With values: {valuesToString.ToString()}");

        }


    }
}
