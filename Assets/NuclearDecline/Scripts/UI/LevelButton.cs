using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]private TMP_Text _textMeshPro;

    private int _levelNumber;

    public Action<int> OnClick;

    public void Init(int levelNumber)
    {
        if (levelNumber >= 0)
        {
            _levelNumber = levelNumber;
        }

        _textMeshPro.text = (_levelNumber + 1).ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(_levelNumber );
       OnClick?.Invoke(_levelNumber);
    }
}
