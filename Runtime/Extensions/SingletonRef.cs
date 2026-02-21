using Leopotam.Ecs;

namespace GovorovAleksandr.SingleLeo
{
    public readonly struct SingletonRef<T> where T : struct
    {
        private readonly EcsWorld _world;

        internal SingletonRef(EcsWorld world)
        {
            _world = world;
        }

        public ref T Get => ref _world.GetSingleton<T>();
    }
}