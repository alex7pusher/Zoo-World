using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;


namespace Modules.Bootstrap.Interfaces
{
	[PublicAPI]
	public interface IBootstrapStage
	{
		UniTask Execute (CancellationToken cancellationToken = default);
	}
}
