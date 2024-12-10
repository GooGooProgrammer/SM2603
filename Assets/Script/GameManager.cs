using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Fight,
    Prepare
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    [SerializeField]
    private AudioClip fightClip;

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
}
