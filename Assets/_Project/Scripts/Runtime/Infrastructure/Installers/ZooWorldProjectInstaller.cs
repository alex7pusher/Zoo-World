using UnityEngine;
using Zenject;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "Zoo World Project Installer", menuName = "Installers/Zoo World Project Installer")]
    public sealed class ZooWorldProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            ZooWorldEventsInstaller.Install(Container);
            ZooWorldStatisticsInstaller.Install(Container);
        }
    }
}