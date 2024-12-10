using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Fight,
    Prepare,
    None
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField]
    private GameObject winningPanel;
    public GameState state;
    [SerializeField]
    private AudioClip fightClip;
    [SerializeField]
    private AudioClip winningClip;


    private bool fightSongPlaying = false;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        SetGameState(GameState.Prepare);
    }
    private void Update()
    {
        if(!GetComponent<AudioSource>().isPlaying && fightSongPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(fightClip);
        }
    }
    public void SetGameState(GameState state)
    {
        this.state = state;
        switch(state)
        {
            case GameState.Fight:
            if(fightSongPlaying) return;
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().volume = 0.6f;
            GetComponent<AudioSource>().PlayOneShot(fightClip);
            fightSongPlaying = true;
            break;
            case GameState.Prepare:
            PrepareStageUI.Instance.gameObject.SetActive(true);
            break;
        }
    }
    public void WinTheGame()
    {
        SetGameState(GameState.None);
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().volume = 0.3f;
        GetComponent<AudioSource>().PlayOneShot(winningClip);
        winningPanel.SetActive(true);
    }
    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
