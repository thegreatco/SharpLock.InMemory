using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpLock.InMemory
{
    public class SharpLockInMemoryClassIdDataStore<TLockableObject, TId> : ISharpLockDataStore<TLockableObject, TId>
        where TLockableObject : class, ISharpLockable<TId> where TId : class
    {
        private readonly SharpLockInMemoryClassIdDataStore<TLockableObject, TLockableObject, TId> _baseDataStore;

        public SharpLockInMemoryClassIdDataStore(IEnumerable<TLockableObject> rawStore, ISharpLockLogger sharpLockLogger,
            TimeSpan lockTime)
        {
            _baseDataStore =
                new SharpLockInMemoryClassIdDataStore<TLockableObject, TLockableObject, TId>(rawStore, sharpLockLogger, lockTime);
        }

        public ISharpLockLogger GetLogger() => _baseDataStore.GetLogger();
        public TimeSpan GetLockTime() => _baseDataStore.GetLockTime();

        public Task<TLockableObject> AcquireLockAsync(TId baseObjId, TLockableObject obj, int staleLockMultiplier,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _baseDataStore.AcquireLockAsync(baseObjId, obj, x => x, staleLockMultiplier, cancellationToken);
        }

        public Task<bool> RefreshLockAsync(TId baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _baseDataStore.RefreshLockAsync(baseObjId, baseObjId, lockedObjectLockId, x => x, cancellationToken);
        }

        public Task<bool> ReleaseLockAsync(TId baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _baseDataStore.ReleaseLockAsync(baseObjId, baseObjId, lockedObjectLockId, x => x, cancellationToken);
        }

        public Task<TLockableObject> GetLockedObjectAsync(TId baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _baseDataStore.GetLockedObjectAsync(baseObjId, baseObjId, lockedObjectLockId, x => x,
                cancellationToken);
        }
    }
}
