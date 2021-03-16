using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactHazard : MonoBehaviour
{
    public enum Type
    {
        Damage,
        Knockback
    }
    public float damage = 0;
    public Type type = Type.Damage;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;
        Status collider_status = collider.GetComponent<Status>();
        if( collider_status == null) return;

        switch (type)
        {
            case Type.Damage:
                damageStatus(collider_status);
                break;
        }


    }

    void damageStatus(Status status)
    {
        status.hp -= damage;
    }

}
