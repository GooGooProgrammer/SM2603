using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField]
    int gateHp;

    [SerializeField]
    Transform gateHealth;
    [SerializeField]
    TextMeshProUGUI gateHealthText;
    [SerializeField]
    GameObject continuePanel;
    [SerializeField]
    TextMeshProUGUI countDown;
    [SerializeField]
    Animator continueAnimator;
    private bool isResetting = false;


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            int damage = collision.transform.GetComponent<Enemy>().GetPower();
            gateHp = gateHp - damage;
            gateHealth.localScale -= new Vector3((float)damage / 2, 0, 0);
            gateHealthText.text = gateHp + " / 6";
            EnemySetControl.Instance.EnemyNumberDecreaseOne(
                collision.gameObject.GetComponent<Enemy>().GetId()
            );
            GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
        }
        if (gateHp <= 0)
        {
            EnemyControl.Instance.CancelInvoke();
            EnemyControl.Instance.ClearAllEnemy();

            GameManager.Instance.SetGameState(GameState.None);
            StartCoroutine(ContinuePanelCountDown());
            continuePanel.SetActive(true);         
        }
    }
    private IEnumerator ContinuePanelCountDown()
    {
        for(int i = 5 ; i>= 0; i--)
        {
            countDown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("GameScene");
        yield return 0;
    }
    public void OnContinueBtnClicked()
    {
        if(isResetting) return;
        isResetting = true;
        
        StopAllCoroutines();
        continueAnimator.SetBool("walking",true);
        Invoke("Continue",2f);

    }
    private void Continue()
    {
        StopAllCoroutines();
        continuePanel.SetActive(false);  
        isResetting = false;
        continueAnimator.SetBool("walking",false);
        
        EnemyControl.Instance.ResetWave();
        gateHp = 6;
        gateHealth.localScale = new Vector3(3,0.26f,0.5f);
        gateHealthText.text = gateHp + " / 6";   
        GameManager.Instance.SetGameState(GameState.Prepare);
    }
}
