using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Configs
{
    public abstract class AnimalMovementConfig : ScriptableObject
    {
        public abstract IAnimalMovement CreateMovement();
    }
}