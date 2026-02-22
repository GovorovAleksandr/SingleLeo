using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public readonly struct SingletonMutationScope<T> where T : struct
    {
        private readonly EcsSingleton<T> _singleton;

        internal SingletonMutationScope(EcsSingleton<T> singleton)
        {
            _singleton = singleton;
        }

        public bool IsAlive => _singleton.IsAlive;

        public SingletonMutationScope<T> With<TComponent>(TComponent component) where TComponent : struct
        {
            _singleton.Entity.Get<TComponent>() = component;
            return this;
        }

        public SingletonMutationScope<T> With<TComponent>() where TComponent : struct
        {
            _singleton.Entity.Get<TComponent>();
            return this;
        }
    }
}