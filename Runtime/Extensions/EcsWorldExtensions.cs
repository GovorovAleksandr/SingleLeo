using System;
using Leopotam.Ecs;

namespace LeoECS.Singletons
{
    public static class EcsWorldExtensions
    {
        public static bool HasSingleton<T>(this EcsWorld world) where T : struct
        {
            return world.GetPool<T>().Items.Length != 0;
        }

        public static bool TryGetSingleton<T>(this EcsWorld world, out SingletonRef<T> component) where T : struct
        {
            if (!world.HasSingleton<T>()) { component = default; return false; }
            component = new SingletonRef<T>(world);
            return true;
        }

        public static ref T GetSingleton<T>(this EcsWorld world) where T : struct
        {
            var pool = world.GetPool<T>();
            if (pool.Items.Length == 0) throw new Exception($"Singleton {typeof(T).Name} not found");
            if (pool.Items.Length > 1) throw new Exception($"Multiple {typeof(T).Name} singletons found");
            return ref pool.GetItem(0);
        }

        public static bool TryAddSingleton<T>(this EcsWorld world, T component, out SingletonRef<T> singletonRef) where T : struct
        {
            if (world.HasSingleton<T>()) { singletonRef = default; return false; }
            world.AddSingleton(component);
            singletonRef = new SingletonRef<T>(world);
            return true;
        }

        public static ref T AddSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            if (world.HasSingleton<T>()) throw new Exception($"Singleton {typeof(T).Name} already exists");
            world.NewEntity().Get<T>() = component;
            return ref world.GetSingleton<T>();
        }

        public static ref T ReplaceOrAddSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            if (world.HasSingleton<T>()) return ref world.ReplaceSingleton(component);
            return ref world.AddSingleton(component);
        }

        public static bool TryReplaceSingleton<T>(this EcsWorld world, T component, out SingletonRef<T> singletonRef) where T : struct
        {
            if (!world.HasSingleton<T>()) { singletonRef = default; return false; }
            world.ReplaceSingleton(component);
            singletonRef = new SingletonRef<T>(world);
            return true;
        }
        
        public static ref T ReplaceSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            if (!world.HasSingleton<T>())
                throw new Exception($"Singleton {typeof(T).Name} not found, cannot replace");

            ref var singleton = ref world.GetSingleton<T>();
            singleton = component;
            return ref singleton;
        }

        public static bool TryRemoveSingleton<T>(this EcsWorld world) where T : struct
        {
            if (!world.HasSingleton<T>()) return false;
            world.RemoveSingleton<T>();
            return true;
        }

        public static void RemoveSingleton<T>(this EcsWorld world) where T : struct
        {
            var pool = world.GetPool<T>();
            
            if (pool.Items.Length == 0) throw new Exception($"Singleton {typeof(T).Name} not found");
            
            var filter = world.GetFilter(typeof(T));
            
            var count = 0;
            
            foreach (var i in filter)
            {
                count++;
                ref var entity = ref filter.GetEntity(i);
                entity.Destroy();
            }

            if (count <= 1) return;
            
            throw new Exception($"Multiple {typeof(T).Name} singletons removed");
        }
    }
}