namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[3];
            arr[1] = 1;

            MyDynamicArray<int> da = new MyDynamicArray<int>();
            da[1] = 1;

            da.Find(x => x > 3);
        }
    }
}