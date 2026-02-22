using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public readonly ref struct SingletonQueryScope<T> where T : struct
    {
        private readonly EcsSingleton<T> _singleton;
        private readonly bool _isValid;

        internal SingletonQueryScope(EcsSingleton<T> singleton, bool isValid = true)
        {
            _singleton = singleton;
            _isValid = isValid;
        }

        public bool IsValid => _isValid && _singleton.IsAlive;

        public SingletonQueryScope<T> With<TComponent>() where TComponent : struct
        {
            return new(_singleton, _isValid && _singleton.Entity.Has<TComponent>());
        }

        public SingletonQueryScope<T> Without<TComponent>() where TComponent : struct
        {
            return new(_singleton, _isValid && !_singleton.Entity.Has<TComponent>());
        }
        
        public EcsSingleton<T> Get()
        {
            if (!_isValid) throw new SingletonInvariantException($"Singleton {typeof(T).Name} scope validation failed");
            if (!_singleton.IsAlive) throw new SingletonInvariantException($"Singleton {typeof(T).Name} is no longer alive");
            return _singleton;
        }

        public bool TryGet(out EcsSingleton<T> singleton)
        {
            if (!IsValid) { singleton = default; return false;}
            singleton = _singleton;
            return true;
        }
    }
}