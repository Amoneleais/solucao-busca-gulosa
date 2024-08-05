public static class FilaPrioridadeExtensions
{
    public static void Enfileirar<T>(this Queue<T> queue, T item)
    {
        queue.Enqueue(item);
    }

    public static T Desenfileirar<T>(this Queue<T> queue)
    {
        return queue.Dequeue();
    }
}
