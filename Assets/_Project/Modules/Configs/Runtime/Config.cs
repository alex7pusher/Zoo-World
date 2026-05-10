using UnityEngine;


namespace Modules.Configs
{
	public abstract class Config : ScriptableObject
	{
		public virtual void Validate () {}
	}
}
