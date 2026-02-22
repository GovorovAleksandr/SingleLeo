using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldMutationExtensions
    {
        public static bool TryAddSingleton<T>(this EcsWorld world, T component, out EcsSingleton singleton) where T : struct
        {
            if (!world.TryAddSingleton<T>(out singleton)) return false;
            singleton.GetRW<T>().Unref() = component;
            return true;
        }

        public static bool TryAddSingleton<T>(this EcsWorld world, out EcsSingleton singleton) where T : struct
        {
            if (world.HasAtLeastOneSingleton<EcsFilter<T>>()) { singleton = default; return false; }
            singleton = world.AddSingleton<T>();
            return true;
        }

        public static EcsSingleton AddSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            var singleton = world.AddSingleton<T>();
            singleton.GetRW<T>().Unref() = component;
            return singleton;
        }

        public static EcsSingleton AddSingleton<T>(this EcsWorld world) where T : struct
        {
            var filter = world.GetFilter(typeof(EcsFilter<T>));
            SingletonInvariantValidator.ValidateDoesNotExist(filter);
            
            var entity = world.NewEntity();
            entity.Get<T>();
            return new(entity);
        }

        public static bool TryRemoveSingleton<TFilter>(this EcsWorld world) where TFilter : EcsFilter
        {
            if (!world.HasAtLeastOneSingleton<TFilter>()) return false;
            world.RemoveSingleton<TFilter>();
            return true;
        }

        public static void RemoveSingleton<TFilter>(this EcsWorld world) where TFilter : EcsFilter
        {
            world.GetSingleton<TFilter>().Destroy();
        }
    }
}