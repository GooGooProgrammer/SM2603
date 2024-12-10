using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : Spell
{
    private bool castTwice = false;

    protected override void SpellAreaFollowMouse()
    {
        if (col.enabled)
            return;
        transform.position = Player.Instance.Weapon.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = -(mousePos - (Vector2)Player.Instance.Weapon.position).normalized;
        Vector3 orignalRotation = transform.eulerAngles;
        transform.eulerAngles = orignalRotation + new Vector3(0, 0, 90);
    }

    protected override void Upgrade(int i, bool isOn)
    {
        switch ((i, isOn))
        {
            case (0, true):
                castTwice = true;
                break;
            case (0, false):
                castTwice = false;
                break;
        }
    }

    protected override IEnumerator Casting()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.material.color = Color.white;
        col.enabled = true;

        animator.SetTrigger("Cast");
        GetComponent<AudioSource>().PlayOneShot(clip);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < checkFrequency; i++)
        {
            Debug.Log(EnemyList.Count);
            Effect();
            yield return new WaitForSeconds(duration / checkFrequency);
        }
        EndCasting();

        if (castTwice)
        {
            animator.SetTrigger("Cast");
            GetComponent<AudioSource>().PlayOneShot(clip);
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < checkFrequency; i++)
            {
                Debug.Log(EnemyList.Count);
                Effect();
                yield return new WaitForSeconds(duration / checkFrequency);
            }
            EndCasting();
        }

        spriteRenderer.enabled = false;
        col.enabled = false;
        EnemyList = new List<Transform>();
    }
}
