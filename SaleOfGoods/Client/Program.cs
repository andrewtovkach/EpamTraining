namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
           Service service = new Service();
            service.OnStart();
        }
    }
}
