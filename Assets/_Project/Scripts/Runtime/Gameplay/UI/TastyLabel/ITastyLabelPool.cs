using System;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.UI.TastyLabel
{
    public interface ITastyLabelPool : IDisposable
    {
        TastyLabelItem Get();
        void Release(TastyLabelItem label);
    }
}
