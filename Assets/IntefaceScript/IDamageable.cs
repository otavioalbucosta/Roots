using UnityEngine;
public interface IDamageable {
    float Health {set;  get;}

    public void OnHit(float damage, Vector2 knockback);

    public void OnHit(float damage);
}