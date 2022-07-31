using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class PlayerMinigamePhysicController : MonoBehaviour
{
    private float value = 0;
    private float color = 0.05f;
    public TextMeshPro LastCubeObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cubes"))
        {
            SetColor(other.gameObject);
            value = other.gameObject.transform.localPosition.z;
            LastCubeObject = other.gameObject.GetComponentInChildren<TextMeshPro>();
            other.gameObject.transform.DOLocalMoveZ(value - 1f, 0.2f).SetEase(Ease.InOutExpo);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cubes"))
        {

            other.gameObject.transform.DOLocalMoveZ(value, 0.2f);
        }
    }

    private void SetColor(GameObject _coloredObj)
    {
        color += 0.05f;
        if (color >= 0.9f)
        {
            color = 0;
        }
        _coloredObj.GetComponentInChildren<Renderer>().material.color = Color.HSVToRGB(color, 1, 1);
    }


    public float GetMultiplyValueAndCalculated(float _currentScore)
    {
        String text =
            LastCubeObject.text.Remove(LastCubeObject.text.Length - 1);
        float multiplyValue = float.Parse(text);
        float TotalMiniGameScore = _currentScore * multiplyValue;
        return TotalMiniGameScore;
    }
}
