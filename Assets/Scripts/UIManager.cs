using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject Panel_GameCompleted;

    public void ShowGameCompleted()
    {
        Panel_GameCompleted.SetActive(true);
    }
}
