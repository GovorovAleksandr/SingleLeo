using System;
using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldComponentMutationExtensions
    {
        public static bool TryAddSingleton<T>(this EcsWorld world, T component, out SingletonRef<T> singletonRef) where T : struct
        {
            if (world.HasAtLeastOneSingleton<T>()) { singletonRef = default; return false; }
            world.AddSingleton(component);
            singletonRef = new SingletonRef<T>(world);
            return true;
        }

        public static ref T AddSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            if (world.HasAtLeastOneSingleton<T>()) throw new Exception($"Singleton {typeof(T).Name} already exists");
            world.NewEntity().Get<T>() = component;
            return ref world.GetSingleton<T>();
        }

        public static ref T ReplaceOrAddSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            if (world.HasAtLeastOneSingleton<T>()) return ref world.ReplaceSingleton(component);
            return ref world.AddSingleton(component);
        }

        public static bool TryReplaceSingleton<T>(this EcsWorld world, T component, out SingletonRef<T> singletonRef) where T : struct
        {
            if (!world.HasAtLeastOneSingleton<T>()) { singletonRef = default; return false; }
            world.ReplaceSingleton(component);
            singletonRef = new SingletonRef<T>(world);
            return true;
        }
        
        public static ref T ReplaceSingleton<T>(this EcsWorld world, T component) where T : struct
        {
            ref var singleton = ref world.GetSingleton<T>();
            singleton = component;
            return ref singleton;
        }
    }
}