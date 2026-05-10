using System;


namespace Modules.Bootstrap
{
	public delegate void BootstrapStageCompletedHandler (StageEventArgs args);

	public class StageEventArgs : EventArgs
	{
		public int StageIndex {get;}

		public StageEventArgs (int stageIndex)
		{
			StageIndex = stageIndex;
		}
	}
}
