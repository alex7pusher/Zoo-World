using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.Bootstrap.Interfaces;


namespace Modules.Bootstrap
{
	[PublicAPI]
	public sealed class Bootstrapper
	{
		private readonly List<IBootstrapStage>       _stages;
		private readonly List<IBootstrapInitializer> _initializers;

		private int _currentStage;

		public int StageCount {get;}

		public event BootstrapStageCompletedHandler StageCompleted;

		public Bootstrapper (
			IEnumerable<IBootstrapStage> stages,
			IEnumerable<IBootstrapInitializer> initializers
		)
		{
			stages       ??= Enumerable.Empty<IBootstrapStage>();
			initializers ??= Enumerable.Empty<IBootstrapInitializer>();

			_stages       = new List<IBootstrapStage>(stages);
			_initializers = new List<IBootstrapInitializer>(initializers);

			StageCount = _stages.Count;

			if (_initializers.Any())
			{
				StageCount++;
			}
		}

		public async UniTask BootstrapAsync ()
		{
			if (!_stages.Any() && !_initializers.Any())
			{
				return;
			}

			_currentStage = 0;

			await ExecuteStagesAsync();
			await InitializeAsync();
		}

		private async UniTask ExecuteStagesAsync ()
		{
			if (!_stages.Any())
			{
				return;
			}

			foreach (IBootstrapStage stage in _stages)
			{
				await stage.Execute();

				StageCompleted?.Invoke(new StageEventArgs(++_currentStage));
			}
		}

		private async UniTask InitializeAsync ()
		{
			if (!_initializers.Any())
			{
				return;
			}

			IEnumerable<UniTask> tasks = _initializers.Select(static initializer => initializer.Initialize());

			await UniTask.WhenAll(tasks);

			StageCompleted?.Invoke(new StageEventArgs(++_currentStage));
		}
	}
}
