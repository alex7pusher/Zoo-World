using Cysharp.Threading.Tasks;
using Modules.Bootstrap;
using UnityEngine;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure
{
    public sealed class ZooWorldEntryPoint : MonoBehaviour
    {
        private Bootstrapper _bootstrapper;

        [Inject]
        public void Construct(Bootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        private void Start()
        {
            _bootstrapper.BootstrapAsync().Forget();
        }
    }
}
