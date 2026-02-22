namespace GovorovAleksandr.SingleLeo
{
    public readonly struct SingletonMutationScope<T> where T : struct
    {
        private readonly EcsSingleton _singleton;

        internal SingletonMutationScope(EcsSingleton singleton)
        {
            _singleton = singleton;
        }

        public SingletonMutationScope<T> With<TComponent>(TComponent component) where TComponent : struct
        {
            _singleton.Add(component);
            return this;
        }

        public SingletonMutationScope<T> With<TComponent>() where TComponent : struct
        {
            _singleton.Add<TComponent>();
            return this;
        }
    }
}