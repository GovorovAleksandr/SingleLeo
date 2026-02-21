using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public readonly struct EcsSingleton<T> where T : struct
    {
        public readonly EcsEntity Entity;
        public readonly EcsComponentRef<T> ComponentRef;

        internal EcsSingleton(EcsEntity entity, EcsComponentRef<T> componentRef)
        {
            Entity = entity;
            ComponentRef = componentRef;
        }
        
        public bool IsAlive => Entity.IsAlive();
    }
}