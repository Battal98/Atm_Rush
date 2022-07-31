using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    internal class ShopSignal:MonoSingleton<ShopSignal>
    {
        public UnityAction<string> onChangeChar = delegate { };
    }
}
