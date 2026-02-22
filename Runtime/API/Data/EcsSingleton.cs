using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public readonly struct EcsSingleton
    {
        private readonly EcsEntity _entity;

        internal EcsSingleton(EcsEntity entity)
        {
            _entity = entity;
        }

        public bool IsAlive => _entity.IsAlive();

        public bool Has<T>() where T : struct => _entity.Has<T>();

        public bool TryGetRO<T>(out T component) where T : struct
        {
            if (!Has<T>()) { component = default; return false; }
            component = GetRO<T>();
            return true;
        }

        public T GetRO<T>() where T : struct
        {
            if (!Has<T>()) ThrowComponentNotExists<T>();
            return _entity.Get<T>();
        }

        public bool TryGetRW<T>(out EcsComponentRef<T> component) where T : struct
        {
            if (!Has<T>()) { component = default; return false; }
            component = GetRW<T>();
            return true;
        }

        public EcsComponentRef<T> GetRW<T>() where T : struct
        {
            if (!Has<T>()) ThrowComponentNotExists<T>();
            return _entity.Ref<T>();
        }
        
        public bool TryAdd<T>(T component) where T : struct
        {
            if (Has<T>()) return false;
            _entity.Get<T>() = component;
            return true;
        }

        public bool TryAdd<T>() where T : struct
        {
            if (Has<T>()) return false;
            _entity.Get<T>();
            return true;
        }
        
        public void Add<T>(T component) where T : struct
        {
            if (Has<T>()) ThrowComponentAlreadyExists<T>();
            _entity.Get<T>() = component;
        }

        public void Add<T>() where T : struct
        {
            if (Has<T>()) ThrowComponentAlreadyExists<T>();
            _entity.Get<T>();
        }

        public void Destroy() => _entity.Destroy();

        private static void ThrowComponentNotExists<T>() where T : struct => throw new SingletonInvariantException($"Component {typeof(T)} does not exists");
        private static void ThrowComponentAlreadyExists<T>() where T : struct => throw new SingletonInvariantException($"Component {typeof(T)} already exists");
    }
}