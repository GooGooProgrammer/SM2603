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
    private float bulletSpeed;

    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private int damage;
    private Vector2 direction;
    private bool onCoolDown = false;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Fire();
        HandleWeaponRotation();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0) && onCoolDown == false)
        {
            GameObject Bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            Bullet.GetComponent<Bullet>().speed = bulletSpeed;
            Bullet.GetComponent<Bullet>().bulletBlocker = bulletBlocker;
            Bullet.GetComponent<Bullet>().damage = damage;
            onCoolDown = true;
            StartCoroutine(CoolDownCalculate());
        }
    }

    void HandleWeaponRotation()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - (Vector2)transform.position).normalized;
        transform.right = direction;
    }

    IEnumerator CoolDownCalculate()
    {
        // a cool down animation should be calculated
        yield return new WaitForSeconds(attackSpeed);
        onCoolDown = false;
    }
}
