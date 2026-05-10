namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    public readonly struct AnimalCollision
    {
        public AnimalView Source { get; }
        public AnimalView Other { get; }

        public AnimalCollision(AnimalView source, AnimalView other)
        {
            Source = source;
            Other = other;
        }
    }
}
