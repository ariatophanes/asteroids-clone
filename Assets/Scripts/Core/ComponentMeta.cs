namespace Core
{
    public static class ComponentMeta<T>
    {
        public static int Id { get; }

        static ComponentMeta() => Id = Counter.Count++;
    }
}