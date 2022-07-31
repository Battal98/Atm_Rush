using UnityEngine.Events;
using Extentions;

public class CollectableSignals : MonoSingleton<CollectableSignals> 
{
    public UnityAction onCollectableUpdateMesh = delegate { };
}
