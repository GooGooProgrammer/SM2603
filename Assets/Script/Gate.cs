using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField]
    int gateHp;

    [SerializeField]
    Transform gateHealth;
    [SerializeField]
    TextMeshProUGUI gateHealthText;

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
            Destroy(collision.gameObject);
        }
        if (gateHp <= 0)
        {
            EnemyControl.Instance.ResetWave();
            gateHp = 6;
            gateHealth.localScale = new Vector3(3,0.26f,0.5f);
            gateHealthText.text = gateHp + " / 6";            
        }
    }
}
