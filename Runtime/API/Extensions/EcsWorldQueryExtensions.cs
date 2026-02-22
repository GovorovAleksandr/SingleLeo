using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldQueryExtensions
    {
        public static bool HasUniqueSingleton<TFilter>(this EcsWorld world) where TFilter : EcsFilter
        {
            return SingletonCountValidator.HasUniqueSingleton(world.GetFilter(typeof(TFilter)));
        }

        public static bool HasAtLeastOneSingleton<TFilter>(this EcsWorld world) where TFilter : EcsFilter
        {
            return SingletonCountValidator.HasAtLeastOneSingleton(world.GetFilter(typeof(TFilter)));
        }

        public static bool HasMoreThanOneSingleton<TFilter>(this EcsWorld world) where TFilter : EcsFilter
        {
            return SingletonCountValidator.HasMoreThanOneSingleton(world.GetFilter(typeof(TFilter)));
        }

        public static bool TryGetSingleton<TTarget, TFilter>(this EcsWorld world, out EcsSingleton<TTarget> singleton)
            where TTarget : struct
            where TFilter : EcsFilter
        {
            if (!world.HasAtLeastOneSingleton<TFilter>()) { singleton = default; return false; }
            singleton = world.GetSingleton<TTarget, TFilter>();
            return true;
        }

        public static EcsSingleton<TTarget> GetSingleton<TTarget, TFilter>(this EcsWorld world)
            where TTarget : struct
            where TFilter : EcsFilter
        {
            var filter = world.GetFilter(typeof(TFilter));

            SingletonInvariantValidator.ValidateUniqueSingleton(filter);

            var entity = filter.GetEntity(0);
            return new (entity, entity.Ref<TTarget>());
        }
    }
}