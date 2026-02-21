using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsWorldEntityMutationExtensions
    {
        public static bool TryRemoveSingleton<T>(this EcsWorld world) where T : struct
        {
            if (!world.HasAtLeastOneSingleton<T>()) return false;
            world.RemoveSingleton<T>();
            return true;
        }

        public static void RemoveSingleton<T>(this EcsWorld world) where T : struct
        {
            world.GetEntitySingleton<T>().Destroy();
        }
    }
}