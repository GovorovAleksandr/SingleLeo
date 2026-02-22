using System.Linq;
using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public static class EcsFilterExtensions
    {
        public static void AddIncludedType<T>(this EcsFilter filter) where T : struct
        {
            
        }
        
        public static string GetDescription(this EcsFilter filter)
        {
            var includedTypes = filter.IncludedTypes.Select(t => t.Name);
            var excludedTypes = filter.ExcludedTypes.Select(t => t.Name);
            const string separator = ", ";
            return $"[Included types: {string.Join(separator, includedTypes)}] [Excluded types: {string.Join(separator, excludedTypes)}]";
        }
    }
}