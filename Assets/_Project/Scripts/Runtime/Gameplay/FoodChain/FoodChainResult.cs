using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    public readonly struct FoodChainResult
    {
        public IAnimal Predator { get; }
        public IAnimal Victim { get; }
        public bool ShouldShowTastyLabel { get; }
        public bool HasVictim => Victim != null;

        public FoodChainResult(IAnimal predator, IAnimal victim, bool shouldShowTastyLabel)
        {
            Predator = predator;
            Victim = victim;
            ShouldShowTastyLabel = shouldShowTastyLabel;
        }
    }
}