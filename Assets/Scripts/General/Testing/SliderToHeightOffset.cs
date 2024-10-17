using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderToHeightOffset : MonoBehaviour
{
    public LevelHandler currLevelHandler;
    private Slider slider;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeHeight()
    {
        currLevelHandler.SetLevelTransformY(currLevelHandler._outsideEditorHeightOffset * slider.value);
        _text.SetText(Convert.ToString(currLevelHandler._outsideEditorHeightOffset * slider.value));
    }
}
