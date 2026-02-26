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
            var includedTypes = "none";
            if (filter.IncludedTypes != null && filter.IncludedTypes.Length > 0)
            {
                for (var i = 0; i < filter.IncludedTypes.Length; i++)
                {
                    var type = filter.IncludedTypes[i];
                    var separator = i == filter.IncludedTypes.Length - 1 ? "" : ", ";
                    includedTypes += $"{type.Name}{separator}";
                }
            }
            
            var excludedTypes = "none";
            if (filter.ExcludedTypes != null && filter.ExcludedTypes.Length > 0)
            {
                for (var i = 0; i < filter.ExcludedTypes.Length; i++)
                {
                    var type = filter.ExcludedTypes[i];
                    var separator = i == filter.ExcludedTypes.Length - 1 ? "" : ", ";
                    excludedTypes += $"{type.Name}{separator}";
                }
            }
            
            return $"[Included types: {includedTypes}] [Excluded types: {excludedTypes}]";
        }
    }
}
