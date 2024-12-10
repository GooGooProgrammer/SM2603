using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicRod : MonoBehaviour, IUpgradeAble
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

    [SerializeField]
    protected List<Toggle> toggles;

    [SerializeField]
    private AudioClip clip;

    private Vector2 direction;
    private bool onCoolDown = false;

    private float bulletSize = 1;
    private bool penetration = false;
    private bool crossFire = false;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        HandleWeaponRotation();
        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButton(0) && onCoolDown == false && GameManager.Instance.state == GameState.Fight)
        {
            FireBullet(transform.rotation);

            if (crossFire)
            {
                FireBullet(transform.rotation * Quaternion.Euler(Vector3.forward * 15));
                FireBullet(transform.rotation * Quaternion.Euler(Vector3.back * 15));
            }

            onCoolDown = true;
            StartCoroutine(CoolDownCalculate());
        }
    }

    void FireBullet(Quaternion rotation)
    {
        GameObject Bullet = Instantiate(BulletPrefab, transform.position, rotation);
        Bullet.GetComponent<Bullet>().speed = bulletSpeed;
        Bullet.GetComponent<Bullet>().bulletBlocker = bulletBlocker;
        Bullet.GetComponent<Bullet>().damage = damage;
        Bullet.transform.localScale *= bulletSize;
        Bullet.GetComponent<Bullet>().penetration = penetration;
        Bullet.GetComponent<BoxCollider2D>().size *= bulletSize;
        Bullet.GetComponent<Bullet>().clip = clip;
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

    public void CheckUpgrade()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            Upgrade(i, toggles[i].isOn);
        }
    }

    private void Upgrade(int i, bool isOn)
    {
        switch ((i, isOn))
        {
            case (0, true):
                bulletSize = 1.5f;
                penetration = true;
                break;
            case (0, false):
                bulletSize = 1f;
                penetration = false;
                break;
            case (1, true):
                damage = 1;
                crossFire = true;
                break;
            case (1, false):
                damage = 2;
                crossFire = false;
                break;
        }
    }
}
