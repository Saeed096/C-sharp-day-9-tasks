namespace task2
{
    internal class Program
    {
        static object obj = new object();
        static bool isFound = false;
        static void findProduct(int id , List<string> l)
        {
            if (id == 1)
              //  Console.WriteLine("sadasdasdas");
            //for (int i = 0; i < 500; i++) { Console.WriteLine(id); }
            lock(obj)         // without lock >> both of tasks can enter >> remove fanta and chipsy , using lock >> sequential and 1st one most of times will remove first "unless i make delay for it using console.writeline...">> benefit of tasks ?????? in other case >> when many actions should be done parallel >> benefit will be shown >> but here we have only one action so >> benefit isnot shown
            {
            if(!isFound) 
            {
              if(l.Contains("fanta"))
                {
                    isFound = true;
                    l.RemoveAt(l.IndexOf("fanta"));
                    Console.WriteLine($"task num {id} removed the product");
                }
            }
            }
        }
        static async Task createThreads(List<string> l) 
        {
            int threadsNum = l.Count / 2;
            LinkedList<Task> tasks = new LinkedList<Task>();
            for(int i = 0; i < threadsNum; i++) 
            {
                int temp = i;                      // to avoid var capture 
                tasks.AddLast(Task.Run(() =>findProduct(temp+1 , l))); 
            }

           await Task.WhenAll(tasks);
        }
        static async Task Main(string[] args)
        {

            List<string> list = new List<string>() {"pepsi" , "cola" , "fanta" , "chipsy" , "fayrouz" };
            
           await createThreads(list); 

            foreach(string s in list)
            {  Console.WriteLine(s); }
        }
    }
}
