using SendMessageLogger;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var logger = new SMLogger("C:\\Users\\karth\\Desktop\\1.txt - Notepad++");
            var logger = new SMLogger("*1.txt - Notepad");
            for (var i = 0; i < 10; i++)
            {
                logger.Log($"hello: {i}\n");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
