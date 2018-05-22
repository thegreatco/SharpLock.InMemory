using System;

namespace SharpLock.InMemory.Tests
{
    public class InnerLock : ISharpLockable<string>
    {
        private static readonly Random Random = new Random();
        public InnerLock()
        {
            Id = Random.Next(0, int.MaxValue - 1).ToString();
            SomeVal = "abcd1234";
        }
        public string SomeVal { get; set;}
        public string Id { get; set; }
        public DateTime? UpdateLock { get; set; }
        public Guid? LockId { get; set; }
    }
}