using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldMutationExtensions
    {
        public static bool TryAddSingleton<T, TFilter>(this EcsWorld world, T component, out EcsSingleton<T> singleton)
            where T : struct
            where TFilter : EcsFilter
        {
            if (!world.TryAddSingleton<T, TFilter>(out singleton)) return false;
            singleton.ComponentRW.Unref() = component;
            return true;
        }

        public static bool TryAddSingleton<T, TFilter>(this EcsWorld world, out EcsSingleton<T> singleton)
            where T : struct
            where TFilter : EcsFilter
        {
            if (world.HasAtLeastOneSingleton<TFilter>()) { singleton = default; return false; }
            
            var entity = world.NewEntity();
            entity.Get<T>();
            singleton = new(entity, entity.Ref<T>());
            
            return true;
        }

        public static EcsSingleton<T> AddSingleton<T, TFilter>(this EcsWorld world, T component)
            where T : struct
            where TFilter : EcsFilter
        {
            var singleton = world.AddSingleton<T, TFilter>();
            singleton.ComponentRW.Unref() = component;
            return singleton;
        }

        public static EcsSingleton<T> AddSingleton<T, TFilter>(this EcsWorld world)
            where T : struct
            where TFilter : EcsFilter
        {
            var filter = world.GetFilter(typeof(TFilter));
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

        public static EcsSingleton<T> ReplaceOrAddSingleton<T, TFilter>(this EcsWorld world, T component)
            where T : struct
            where TFilter : EcsFilter
        {
            if (world.HasAtLeastOneSingleton<TFilter>()) return world.ReplaceSingleton<T, TFilter>(component);
            return world.AddSingleton<T, TFilter>(component);
        }

        public static bool TryReplaceSingleton<T, TFilter>(this EcsWorld world, T component, out EcsSingleton<T> singleton)
            where T : struct
            where TFilter : EcsFilter
        {
            if (!world.TryGetSingleton<T, TFilter>(out singleton)) { singleton = default; return false; }
            singleton.Entity.Get<T>() = component;
            return true;
        }
        
        public static EcsSingleton<T> ReplaceSingleton<T, TFilter>(this EcsWorld world, T component)
            where T : struct
            where TFilter : EcsFilter
        {
            var singleton = world.GetSingleton<T, TFilter>();
            singleton.Entity.Get<T>() = component;
            return singleton;
        }
    }
}