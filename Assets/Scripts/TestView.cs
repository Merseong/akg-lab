using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VContainer;

public class TestView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI turnText;
    [Inject]
    private ITurnController turnManager;

    // Start is called before the first frame update
    void Start()
    {
        turnManager.Timer.OnValueChanged += UpdateTimer;
        turnManager.IsPlayerTurn.OnValueChanged += UpdateTurn;

        turnText.text = turnManager.IsPlayerTurn.Value.ToString();
    }

    private void UpdateTimer(int prevValue, int newValue)
    {
        timerText.text = newValue.ToString();
    }

    private void UpdateTurn(bool prevData, bool newData)
    {
        turnText.text = newData.ToString();
    }
}
