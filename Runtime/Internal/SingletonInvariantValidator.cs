using System.Linq;
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
            throw new SingletonInvariantException($"Entity singleton {GetDescription(filter)} not found");
        }

        public static void ValidateDoesNotExist(EcsFilter filter)
        {
            if (!SingletonCountValidator.HasAtLeastOneSingleton(filter)) return;
            throw new SingletonInvariantException($"Entity singleton with filter: {GetDescription(filter)} already exists");
        }

        public static void ValidateNotMultiple(EcsFilter filter)
        {
            if (!SingletonCountValidator.HasMoreThanOneSingleton(filter)) return;
            throw new SingletonInvariantException($"Multiple {GetDescription(filter)} Entity singletons found");
        }
        
        public static string GetDescription(EcsFilter filter)
        {
            var includedTypes = filter.IncludedTypes.Select(t => t.Name);
            var excludedTypes = filter.ExcludedTypes.Select(t => t.Name);
            const string separator = ", ";
            return $"[Included types: {string.Join(separator, includedTypes)}] [Excluded types: {string.Join(separator, excludedTypes)}]";
        }
    }
}