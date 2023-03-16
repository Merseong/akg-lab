using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private TurnController turnManager;

    // Start is called before the first frame update
    void Start()
    {
        turnManager.timer.OnValueChanged += UpdateTimer;
    }

    private void UpdateTimer(int prevValue, int newValue)
    {
        text.text = newValue.ToString();
    }
}
