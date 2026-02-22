using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    internal static class SingletonInvariantValidator
    {
        public static void ValidateUniqueSingleton(EcsFilter filter)
        {
            ValidateExists(filter);
            ValidateNotMultiple(filter);
        }

        public static void ValidateExists(EcsFilter filter)
        {
            if (SingletonCountValidator.HasAtLeastOneSingleton(filter)) return;
            throw new SingletonInvariantException($"Entity singleton {filter.GetDescription()} not found");
        }

        public static void ValidateDoesNotExist(EcsFilter filter)
        {
            if (!SingletonCountValidator.HasAtLeastOneSingleton(filter)) return;
            throw new SingletonInvariantException($"Entity singleton with filter: {filter.GetDescription()} already exists");
        }

        public static void ValidateNotMultiple(EcsFilter filter)
        {
            if (!SingletonCountValidator.HasMoreThanOneSingleton(filter)) return;
            throw new SingletonInvariantException($"Multiple {filter.GetDescription()} Entity singletons found");
        }
    }
}