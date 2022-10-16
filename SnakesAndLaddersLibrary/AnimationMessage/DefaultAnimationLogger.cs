using System.Text;

namespace SnakesAndLaddersLibrary.AnimationMessage;

public class DefaultAnimationLogger : IAnimationLogger
{
    public async Task AnimationMessage(IMessage message)
    {
        var valuesToString = new StringBuilder();
        valuesToString.AppendLine();
        message.Values.ForEach(value => valuesToString.AppendLine($" <-- Key:'{value.Key}' Value:'{value.Value}' -->"));
        Console.WriteLine(
            $"Object: '{message.Sender}' Performed: '{message.Animation}' With values: {valuesToString.ToString()}");
        await Task.Delay(10);
    }
}