using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public readonly struct SingletonScope<T> where T : struct
    {
        private readonly EcsWorld _world;
        private readonly EcsSingleton<T> _singleton;
        private readonly bool _isValid;

        internal SingletonScope(EcsWorld world, EcsSingleton<T> singleton, bool isValid = true)
        {
            _world = world;
            _singleton = singleton;
            _isValid = isValid;
        }

        public bool IsValid => _isValid;

        public SingletonScope<T> With<TTag>() where TTag : struct
        {
            return new(_world, _singleton, _isValid && _singleton.Entity.Has<TTag>());
        }

        public SingletonScope<T> Without<TTag>() where TTag : struct
        {
            return new(_world, _singleton, _isValid && !_singleton.Entity.Has<TTag>());
        }
        
        public EcsComponentRef<T> GetComponentRef()
        {
            if (!_isValid) throw new SingletonInvariantException($"Singleton {typeof(T).Name} scope validation failed");
            if (!_singleton.IsAlive) throw new SingletonInvariantException($"Singleton {typeof(T).Name} is no longer alive");
            return _singleton.ComponentRef;
        }

        public bool TryGetComponentRef(out EcsComponentRef<T> value)
        {
            if (!_isValid || !_singleton.IsAlive) { value = default; return false;}
            value = _singleton.ComponentRef;
            return _isValid;
        }
    }
}