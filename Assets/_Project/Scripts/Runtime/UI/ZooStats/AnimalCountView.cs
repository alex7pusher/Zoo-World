using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using TMPro;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.UI.ZooStats
{
    public sealed class AnimalCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private string _format = "Dead {0}: {1}";

        private AnimalChainType _chainType;

        public void Setup(AnimalChainType chainType)
        {
            _chainType = chainType;
            SetCount(0);
        }

        public void SetCount(int count)
        {
            _countText.text = string.Format(_format, _chainType, count);
        }
    }
}