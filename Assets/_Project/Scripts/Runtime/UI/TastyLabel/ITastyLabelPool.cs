using System;

namespace Modules.ZooWorld._Project.Scripts.Runtime.UI.TastyLabel
{
    public interface ITastyLabelPool : IDisposable
    {
        TastyLabelItem Get();
        void Release(TastyLabelItem label);
    }
}
