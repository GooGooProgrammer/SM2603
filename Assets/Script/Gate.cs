using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField]
    int gateHp;
    [SerializeField]
    Transform gateHealth; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            int damage = collision.transform.GetComponent<Enemy>().GetPower();
            gateHp = gateHp - damage;
            gateHealth.localScale -= new Vector3((float)damage / 2, 0, 0);
            Destroy(collision.gameObject);
        }
    }
}
