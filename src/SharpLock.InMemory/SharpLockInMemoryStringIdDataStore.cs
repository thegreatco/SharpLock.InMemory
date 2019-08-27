using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpLock.InMemory
{
    public class SharpLockInMemoryStringIdDataStore<TLockableObject> : ISharpLockDataStore<TLockableObject, string>
        where TLockableObject : class, ISharpLockable<string>
    {
        private readonly SharpLockInMemoryStringIdDataStore<TLockableObject, TLockableObject> _baseDataStore;

        public SharpLockInMemoryStringIdDataStore(IEnumerable<TLockableObject> rawStore, ILogger logger,
            TimeSpan lockTime)
        {
            _baseDataStore =
                new SharpLockInMemoryStringIdDataStore<TLockableObject, TLockableObject>(rawStore, logger, lockTime);
        }

        public SharpLockInMemoryStringIdDataStore(IEnumerable<TLockableObject> rawStore, ILoggerFactory loggerFactory,
            TimeSpan lockTime) : this(rawStore, loggerFactory.CreateLogger<SharpLockInMemoryStringIdDataStore<TLockableObject>>(), lockTime)
        {
        }

        public ILogger GetLogger() => _baseDataStore.GetLogger();
        public TimeSpan GetLockTime() => _baseDataStore.GetLockTime();

        public Task<TLockableObject> AcquireLockAsync(string baseObjId, TLockableObject obj, int staleLockMultiplier,
            CancellationToken cancellationToken = default)
        {
            return _baseDataStore.AcquireLockAsync(baseObjId, obj, x => x, staleLockMultiplier, cancellationToken);
        }

        public Task<bool> RefreshLockAsync(string baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default)
        {
            return _baseDataStore.RefreshLockAsync(baseObjId, baseObjId, lockedObjectLockId, x => x, cancellationToken);
        }

        public Task<bool> ReleaseLockAsync(string baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default)
        {
            return _baseDataStore.ReleaseLockAsync(baseObjId, baseObjId, lockedObjectLockId, x => x, cancellationToken);
        }

        public Task<TLockableObject> GetLockedObjectAsync(string baseObjId, Guid lockedObjectLockId,
            CancellationToken cancellationToken = default)
        {
            return _baseDataStore.GetLockedObjectAsync(baseObjId, baseObjId, lockedObjectLockId, x => x,
                cancellationToken);
        }
    }
}
