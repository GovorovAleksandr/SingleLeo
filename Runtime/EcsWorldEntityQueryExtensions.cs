using System;
using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldEntityQueryExtensions
    {
        public static bool TryGetSingletonEntity<T>(this EcsWorld world, out EcsEntity entity) where T : struct
        {
            if (!world.HasAtLeastOneSingleton<T>()) { entity = default; return false; }
            entity = world.GetEntitySingleton<T>();
            return true;
        }

        public static EcsEntity GetEntitySingleton<T>(this EcsWorld world) where T : struct
        {
            var filter = world.GetFilter(typeof(T));
            var count = filter.GetEntitiesCount();

            if (count == 0) throw new Exception($"Entity singleton {typeof(T).Name} not found");
            if (count > 1) throw new Exception($"Multiple {typeof(T).Name} Entity singletons  found");

            return filter.GetEntity(0);
        }
    }
}