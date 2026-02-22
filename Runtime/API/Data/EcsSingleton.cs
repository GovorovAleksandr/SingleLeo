using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public readonly struct EcsSingleton<T> where T : struct
    {
        public readonly EcsEntity Entity;
        public readonly EcsComponentRef<T> ComponentRW;

        internal EcsSingleton(EcsEntity entity, EcsComponentRef<T> componentRW)
        {
            Entity = entity;
            ComponentRW = componentRW;
        }

        public bool IsAlive => Entity.IsAlive();
        public T ComponentRO => ComponentRW.Unref();
    }
}