using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] AimComponent aimComp;
    [SerializeField] float damage = 5f;
    [SerializeField] ParticleSystem bulletVfx;
    public override void Attack()
    {
        GameObject target = aimComp.GetAimTarget(out Vector3 aimDir);
        DamageGameObject(target, damage);

        bulletVfx.transform.rotation = Quaternion.LookRotation(aimDir);
        bulletVfx.Emit(bulletVfx.emission.GetBurst(0).maxCount);
        PlayWeaponAudio();
    }
}
