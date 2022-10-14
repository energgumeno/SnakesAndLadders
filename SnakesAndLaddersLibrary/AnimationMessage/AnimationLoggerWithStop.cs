using System.Text;

namespace SnakesAndLaddersLibrary.AnimationMessage
{
    public class AnimationLoggerWithStop : IAnimationLogger
    {

        public async Task AnimationMessage(IMessage message)
        {
            StringBuilder valuesToString = new StringBuilder();
            valuesToString.AppendLine();
            message.Values.ForEach(value => valuesToString.AppendLine($" <-- Key:'{value.Key}' Value:'{value.Value}' -->"));
            Console.WriteLine($"Object: '{message.Sender}' Performed: '{message.Animation}' With values: {valuesToString.ToString()}");
            await Task.Delay(10);
            if (message.Animation == "SetNextPlayer") 
            {
                Console.ReadLine();
            
            }

        }


    }
}
