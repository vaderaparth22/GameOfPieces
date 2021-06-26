using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private UIManager uiManager;

    readonly int totalSlots = 16;
    private int currentSlotFilled = 0;

    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    void Awake()
    {
        #region Singleton

        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        #endregion

        Initialize();
    }

    private void Initialize()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    public void IncreaseCorrectCount()
    {
        currentSlotFilled++;

        if(currentSlotFilled >= totalSlots)
        {
            currentSlotFilled = 0;
            isGameOver = true;
            uiManager.ShowGameCompleted();
            SoundManager.Instance.PlayAudio(AudioType.Success);
        }
    }
}
