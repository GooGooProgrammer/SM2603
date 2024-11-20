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
    public GameState state = GameState.Prepare;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

}
