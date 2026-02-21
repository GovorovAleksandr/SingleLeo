using System;
using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldComponentQueryExtensions
    {
        public static bool HasUniqueSingleton<T>(this EcsWorld world) where T : struct
        {
            return world.HasAtLeastOneSingleton<T>() && !world.HasMoreThanOneSingleton<T>();
        }

        public static bool HasAtLeastOneSingleton<T>(this EcsWorld world) where T : struct
        {
            return world.GetPool<T>().Items.Length != 0;
        }

        public static bool HasMoreThanOneSingleton<T>(this EcsWorld world) where T : struct
        {
            return world.GetPool<T>().Items.Length > 1;
        }

        public static bool TryGetSingleton<T>(this EcsWorld world, out SingletonRef<T> singleton) where T : struct
        {
            if (!world.HasAtLeastOneSingleton<T>()) { singleton = default; return false; }
            singleton = new SingletonRef<T>(world);
            return true;
        }

        public static ref T GetSingleton<T>(this EcsWorld world) where T : struct
        {
            var count = world.GetPool<T>().Items.Length;
            
            if (count == 0) throw new Exception($"Singleton {typeof(T).Name} not found");
            if (count > 1) throw new Exception($"Multiple {typeof(T).Name} singletons found");
            
            return ref world.GetPool<T>().GetItem(0);
        }
    }
}