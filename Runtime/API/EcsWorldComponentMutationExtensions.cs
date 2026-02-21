using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldComponentMutationExtensions
    {
        public static bool TryAddSingleton<T>(this EcsWorld world, T component, out EcsSingleton<T> singleton) where T : struct
        {
            if (!world.TryAddSingleton(out singleton)) return false;
            
            singleton.ComponentRef.Unref() = component;
            
            return true;
        }

        public static bool TryAddSingleton<T>(this EcsWorld world, out EcsSingleton<T> singleton) where T : struct
        {
            if (world.HasAtLeastOneSingleton<T>()) { singleton = default; return false; }
            
            var entity = world.NewEntity();
            entity.Get<T>();
            singleton = new(entity, entity.Ref<T>());
            
            return true;
        }

        public static EcsSingleton<T> AddSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            var singleton = world.AddSingleton<T>();
            singleton.ComponentRef.Unref() = component;
            return singleton;
        }

        public static EcsSingleton<T> AddSingleton<T>(this EcsWorld world) where T : struct
        {
            var filter = world.GetFilter(typeof(EcsFilter<T>));
            SingletonInvariantValidator.ValidateDoesNotExist<T>(filter);
            
            var entity = world.NewEntity();
            entity.Get<T>();
            return new(entity, entity.Ref<T>());
        }

        public static bool TryRemoveSingleton<T>(this EcsWorld world) where T : struct
        {
            if (!world.HasAtLeastOneSingleton<T>()) return false;
            world.RemoveSingleton<T>();
            return true;
        }

        public static void RemoveSingleton<T>(this EcsWorld world) where T : struct
        {
            world.GetSingleton<T>().Entity.Destroy();
        }

        public static EcsSingleton<T> ReplaceOrAddSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            if (world.HasAtLeastOneSingleton<T>()) return world.ReplaceSingleton(component);
            return world.AddSingleton(component);
        }

        public static bool TryReplaceSingleton<T>(this EcsWorld world, T component, out EcsSingleton<T> singleton) where T : struct
        {
            if (!world.TryGetSingleton(out singleton)) { singleton = default; return false; }
            singleton.Entity.Get<T>() = component;
            return true;
        }
        
        public static EcsSingleton<T> ReplaceSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            var singleton = world.GetSingleton<T>();
            singleton.Entity.Get<T>() = component;
            return singleton;
        }
    }
}