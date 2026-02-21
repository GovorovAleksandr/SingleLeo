using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    internal static class SingletonCountValidator
    {
        public static bool HasUniqueSingleton(EcsFilter filter) => filter.GetEntitiesCount() == 1;
        public static bool HasAtLeastOneSingleton(EcsFilter filter) => filter.GetEntitiesCount() != 0;
        public static bool HasMoreThanOneSingleton(EcsFilter filter) => filter.GetEntitiesCount() > 1;
    }
}