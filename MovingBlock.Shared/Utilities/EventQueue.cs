namespace MovingBlock.Shared.Utilities
{
    public class EventQueue<T>
    {
        private static readonly EventQueue<T> instance = new EventQueue<T>();
        public static EventQueue<T> Instance { get { return instance; } }

        private readonly Queue<T> queue = new Queue<T>();
        public event EventHandler Enqueued;
        protected virtual void OnEnqueued()
        {
            if (Enqueued != null)
                Enqueued(this, EventArgs.Empty);
        }

        public virtual void Enqueue(T item)
        {
            queue.Enqueue(item);
            OnEnqueued();
        }

        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        public virtual T Dequeue()
        {
            T item = queue.Dequeue();
            return item;
        }
    }
}
