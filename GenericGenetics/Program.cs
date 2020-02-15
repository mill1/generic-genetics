using System;

namespace GenericGenetics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Program p = new Program();

            try
            {
                p.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.Read();
            }
        }

        public void Run()
        {

            TextEvolution evolution = new TextEvolution();

            evolution.Run();
        }
    }
}
