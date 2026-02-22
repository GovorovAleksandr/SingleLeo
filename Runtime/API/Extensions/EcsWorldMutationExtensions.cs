using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldMutationExtensions
    {
        public static bool TryAddSingleton<T>(this EcsWorld world, T component, out EcsSingleton<T> singleton) where T : struct
        {
            if (!world.TryAddSingleton(out singleton)) return false;
            singleton.ComponentRW.Unref() = component;
            return true;
        }

        public static bool TryAddSingleton<T>(this EcsWorld world, out EcsSingleton<T> singleton) where T : struct
        {
            if (world.HasAtLeastOneSingleton<EcsFilter<T>>()) { singleton = default; return false; }
            singleton = world.AddSingleton<T>();
            return true;
        }

        public static EcsSingleton<T> AddSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            var singleton = world.AddSingleton<T>();
            singleton.ComponentRW.Unref() = component;
            return singleton;
        }

        public static EcsSingleton<T> AddSingleton<T>(this EcsWorld world) where T : struct
        {
            var filter = world.GetFilter(typeof(EcsFilter<T>));
            SingletonInvariantValidator.ValidateDoesNotExist(filter);
            
            var entity = world.NewEntity();
            entity.Get<T>();
            return new(entity, entity.Ref<T>());
        }

        public static bool TryRemoveSingleton<T, TFilter>(this EcsWorld world)
            where T : struct
            where TFilter : EcsFilter
        {
            if (!world.HasAtLeastOneSingleton<TFilter>()) return false;
            world.RemoveSingleton<T, TFilter>();
            return true;
        }

        public static void RemoveSingleton<T, TFilter>(this EcsWorld world)
            where T : struct
            where TFilter : EcsFilter
        {
            world.GetSingleton<T, TFilter>().Entity.Destroy();
        }
    }
}