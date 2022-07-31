using Extentions;
using UnityEngine;
using Enums;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<ScoreTypes,float> onScoreChange= delegate { };
        public UnityAction onScoreNeedToSave= delegate { };

    }
}