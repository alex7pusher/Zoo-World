using JetBrains.Annotations;
using Zenject;


namespace Modules.Bootstrap.Zenject
{
	[PublicAPI]
	public class BootstrapperInstaller : Installer<BootstrapperInstaller>
	{
		public override void InstallBindings ()
		{
			Container.Bind<Bootstrapper>().AsSingle();
		}
	}
}
