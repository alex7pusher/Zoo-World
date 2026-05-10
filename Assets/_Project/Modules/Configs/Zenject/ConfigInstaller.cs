using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Modules.Configs.Zenject
{
	[CreateAssetMenu(fileName = "Config Installer", menuName = "Modules/Configs/Installer")]
	public class ConfigInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private List<Config> _configs;

		public override void InstallBindings ()
		{
			foreach (Config config in _configs)
			{
				config.Validate();

				Container.BindInterfacesAndSelfTo(config.GetType())
				         .FromInstance(config)
				         .AsSingle();
			}
		}
	}
}
