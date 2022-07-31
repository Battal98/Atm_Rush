using System;
using Command;
using Signals;
using Controllers;
using Data.UnityObject;
using Enums;
using Data.ValueObject;
using UnityEngine;

public class ATMManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public ScoreData ScoreData;


    #endregion
    #region Serialized Variables

    [SerializeField]
    private GameObject moveable;

    [SerializeField]
    private AtmScoreTextController atmScoreTextController;


    #endregion
    #region Private Variables

    private ApplyShakeAnimationCommand applyShakeAnimationCommand;
    private ApplyShutDownAnimationCommand applyShutDownAnimationCommand;
    private float _currentScore=0;
    #endregion
    #endregion

    #region Subs

    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        AtmSignals.Instance.onAtmScoreUpdate += OnUpdateAtmScore;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnSubscribeEvents()
    {
        AtmSignals.Instance.onAtmScoreUpdate -= OnUpdateAtmScore;
        CoreGameSignals.Instance.onReset -= OnReset;
    }



    #endregion
    private void Awake()
    {
        applyShakeAnimationCommand = new ApplyShakeAnimationCommand();
        applyShutDownAnimationCommand = new ApplyShutDownAnimationCommand();
        ScoreData=GetScoreData();
    }

    private ScoreData GetScoreData() => Resources.Load<CD_Score>("Data/CD_Score").ScoreData;

    public void ApplyShakeAnimation()
    {
        applyShakeAnimationCommand.ApplyShakeAnimation(moveable.transform);
    }
    public void ApplyShutDownAnimation()
    {
        applyShutDownAnimationCommand.ApplyShutDownAnimation(moveable.transform);
    }

    public void OnUpdateAtmScore(int enumValue, bool _amIReset)
    {
        atmScoreTextController.CalculateandUpdateAtmScoreText(enumValue, _amIReset,ref _currentScore,ScoreData.IncomeValue);
        ScoreSignals.Instance.onScoreChange?.Invoke(ScoreTypes.AtmScore,_currentScore);
    }

    private void OnReset()
    {
        OnUpdateAtmScore(0, true);
    }


}
