

namespace MauiApp1.Platforms.Android
{
    internal class MessageTest : IMessage
    {
        //string IMessage.myString => throw new NotImplementedException();

        string IMessage.myString => "hello";
    }
}
