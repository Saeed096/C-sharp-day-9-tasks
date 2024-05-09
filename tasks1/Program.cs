using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace tasks1
{
    internal class Program
    {
        static bool isFound = false;
        static int startSearch(string text, int id)
        {
            if (!isFound)
            {
                if (text.Contains("target"))
                {
                    isFound = true;                   // right way to abort other tasks????? yes "not recommended to use abort"
                    return id; 
                }
            }
            return -1;
        }

        static async Task assThreadToString(string[]strArr)
        {
            LinkedList<Task<int>> tasks = new LinkedList<Task<int>>();
            for(int i = 0; i < strArr.Length; i++) 
            {
                int temp = i; 
                tasks.AddFirst(Task<int>.Run(() => startSearch(strArr[temp], temp+1)));
            }
           
            await Task.WhenAll(tasks);

            foreach(Task<int> task in tasks)
            { 
                int result = task.Result;
                if (result > 0)
                { 
                    Console.WriteLine($"task num {result} found the k.w");
                    break;
                }
            }

        }

        static async Task Main(string[] args)
        {
            string str = "The target of our journey lay hidden amidst the dense foliage, a place where dreams intertwine with reality, beckoning us forward with whispers of ancient secrets and untold wonders";
            string[] strArr = str.Split(',');

            await assThreadToString(strArr);  // implicitly return in the func whatever returned from await "so that u can when call this func also make await for it ...." if await return task func main here will return task >> does it mean in each func it is allowed only to use 1 await >> if 2 await return different task >> task<int> after await >> return int , task<string> after await >> return string >> so main here will return what???????

        }
    }
}
