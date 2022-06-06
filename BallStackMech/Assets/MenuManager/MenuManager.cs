using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class MenuManager : MonoBehaviour
{
    public static MenuManager MenuManagerInstance;
    public bool GameState;
    public GameObject[] menuElement=new GameObject[2];
    [SerializeField] private TextMeshProUGUI Tp_text;
    [SerializeField] public TextMeshProUGUI TapToPlay;
    [SerializeField] private Ease MotionType;
    // [SerializeField] private Ease MotionType; TAP TO PLAYI CANLANDIR.


    void Start()
    {
        GameState = false;
        MenuManagerInstance = this;
        menuElement[3].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("score").ToString();

        TapToPlay.transform.DOScale(1.2f, 0.5f).SetLoops(1000,LoopType.Yoyo).SetEase(MotionType);
    }
    public void StartTheGame()
    {
        GameState = true;
        menuElement[0].SetActive(false);
        GameObject.FindWithTag("particle").GetComponent<ParticleSystem>().Play();
    }
    public void Retry_btn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
