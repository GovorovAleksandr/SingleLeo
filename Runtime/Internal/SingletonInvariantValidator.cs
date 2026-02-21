using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    internal static class SingletonInvariantValidator
    {
        public static void ValidateUniqueSingleton<T>(EcsFilter filter) where T : struct
        {
            ValidateExists<T>(filter);
            ValidateNotMultiple<T>(filter);
        }

        public static void ValidateExists<T>(EcsFilter filter) where T : struct
        {
            if (SingletonCountValidator.HasAtLeastOneSingleton(filter)) return;
            throw new SingletonInvariantException($"Entity singleton {typeof(T).Name} not found");
        }

        public static void ValidateDoesNotExist<T>(EcsFilter filter) where T : struct
        {
            if (!SingletonCountValidator.HasAtLeastOneSingleton(filter)) return;
            throw new SingletonInvariantException($"Entity singleton {typeof(T).Name} already exists");
        }

        public static void ValidateNotMultiple<T>(EcsFilter filter) where T : struct
        {
            if (!SingletonCountValidator.HasMoreThanOneSingleton(filter)) return;
            throw new SingletonInvariantException($"Multiple {typeof(T).Name} Entity singletons found");
        }
    }
}