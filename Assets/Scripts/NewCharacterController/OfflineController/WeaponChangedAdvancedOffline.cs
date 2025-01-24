using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
public class WeaponChangeAdvancedOffline : MonoBehaviour
{
    public TwoBoneIKConstraint leftHand;
    public TwoBoneIKConstraint rightHand;
    public TwoBoneIKConstraint leftThumb;
    public RigBuilder rig;
    public Transform[] leftTargets;
    public Transform[] rightTargets;
    public Transform[] thumbTargets;
    public GameObject[] weapons;
    private int weaponNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            weaponNumber++;
            if (weaponNumber > weapons.Length - 1)
            {
                weaponNumber = 0;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[weaponNumber].SetActive(true);
            leftHand.data.target = leftTargets[weaponNumber];
            rightHand.data.target = rightTargets[weaponNumber];
            leftThumb.data.target = thumbTargets[weaponNumber];
            rig.Build();
        }
    }
}