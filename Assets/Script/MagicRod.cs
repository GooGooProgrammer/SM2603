using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRod : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private LayerMask bulletBlocker;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
    void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject Bullet = Instantiate(BulletPrefab,transform.position,transform.rotation);
            Bullet.GetComponent<Bullet>().speed = speed;
            Bullet.GetComponent<Bullet>().bulletBlocker = bulletBlocker;
            Bullet.GetComponent<Bullet>().damage = damage;
        }
    }
}
