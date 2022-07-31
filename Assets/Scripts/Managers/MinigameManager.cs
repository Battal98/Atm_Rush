using Signals;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Data.ValueObject;
using Data.UnityObject;
using Controllers;

public class MinigameManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    [Header("Data")]
    public ScoreBoardData Data;
    [Space]
    #endregion

    #region Serialized Variables

    [SerializeField]
    private MinigameController minigameController;

    [SerializeField]
    private GameObject cubeObj;
    
    [SerializeField]
    private Transform cubeTarget;

    [SerializeField]
    private Transform spawner;

    [SerializeField]
    private List<GameObject> cubeList = new List<GameObject>();
    
    #endregion

    #region Private Variables


    #endregion

    #endregion

    private void Awake()
    {
        Data = GetScoreBoardData();
        cubeObj = GetCubeObjPrefab();
    }


    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += OnInstantiateCubes;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= OnInstantiateCubes;

    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void Start()
    {
        OnInstantiateCubes();
    }

    private ScoreBoardData GetScoreBoardData()
    {
        return Resources.Load<CD_ScoreBoard>("Data/CD_ScoreBoard").ScoreboardData;
    }

    private GameObject GetCubeObjPrefab()
    {
        return Resources.Load<GameObject>("Prefabs/Minigame/Cube_Prefab");
    }

    private void OnInstantiateCubes()
    {
        for (int i = 0; i < Data.ScoreboardCount; i++)
        {
            if (i==0)
            {
                minigameController.SetBoardPositionsAndTexts(cubeObj,cubeTarget.transform.position,cubeList,spawner, Data.CountMultiplier);

            }
            else
            {
                Vector3 _pos = cubeList[i - 1].transform.position + new Vector3(0, cubeObj.transform.localScale.y, 0);
                minigameController.SetBoardPositionsAndTexts(cubeObj, _pos , cubeList, spawner, Data.CountMultiplier);
            }
        }
    }



}
