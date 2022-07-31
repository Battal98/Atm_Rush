using UnityEngine;
using Signals;
using Keys;


namespace Managers
{
    public class GameManager : MonoBehaviour
    {    
        private void Awake()
        {
            
            Application.targetFrameRate = 60;
            GameOpen();
            
        }
        private void OnDisable()
        {
            GameClose();
        }

        private void GameOpen()
        {
            CoreGameSignals.Instance.onGameOpen?.Invoke();
        }

        private void OnApplicationFocus(bool _value)
        {
            CoreGameSignals.Instance.onGamePause?.Invoke(_value);
        }

        private void GameClose()
        {
            CoreGameSignals.Instance.onGameClose?.Invoke();
        }
    } 
}
