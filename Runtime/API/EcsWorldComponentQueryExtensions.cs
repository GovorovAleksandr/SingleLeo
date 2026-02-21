using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldComponentQueryExtensions
    {
        public static bool HasUniqueSingleton<T>(this EcsWorld world) where T : struct
        {
            return SingletonCountValidator.HasUniqueSingleton(world.GetFilter(typeof(EcsFilter<T>)));
        }

        public static bool HasAtLeastOneSingleton<T>(this EcsWorld world) where T : struct
        {
            return SingletonCountValidator.HasAtLeastOneSingleton(world.GetFilter(typeof(EcsFilter<T>)));
        }

        public static bool HasMoreThanOneSingleton<T>(this EcsWorld world) where T : struct
        {
            return SingletonCountValidator.HasMoreThanOneSingleton(world.GetFilter(typeof(EcsFilter<T>)));
        }

        public static bool TryGetSingleton<T>(this EcsWorld world, out EcsSingleton<T> singleton) where T : struct
        {
            if (!world.HasAtLeastOneSingleton<T>()) { singleton = default; return false; }
            singleton = world.GetSingleton<T>();
            if (singleton.IsAlive) return true;
            singleton = default;
            return false;
        }

        public static EcsSingleton<T> GetSingleton<T>(this EcsWorld world) where T : struct
        {
            var filter = world.GetFilter(typeof(EcsFilter<T>));

            SingletonInvariantValidator.ValidateUniqueSingleton<T>(filter);

            var entity = filter.GetEntity(0);
            return new (entity, entity.Ref<T>());
        }

        public static bool TryGetSingletonScope<T>(this EcsWorld world, out SingletonScope<T> singletonScope) where T : struct
        {
            if (!world.TryGetSingleton<T>(out var singleton)) { singletonScope = default; return false; }
            singletonScope = new(world, singleton);
            return true;
        }

        public static SingletonScope<T> GetSingletonScope<T>(this EcsWorld world) where T : struct
        {
            var singleton = world.GetSingleton<T>();
            return new(world, singleton);
        }
    }
}