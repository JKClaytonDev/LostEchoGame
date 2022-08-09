using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAnimationController : MonoBehaviour
{
    Animator anim;
    public float Ammo;
    public string fireAnimationName;
    public string reloadAnimationName;
    AnimationEvent fireEVT;
    AnimationEvent reloadEVT;
    AnimationEvent emptyEVT;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fireEVT = new AnimationEvent();
        fireEVT.functionName = "Fire";
        reloadEVT = new AnimationEvent();
        reloadEVT.functionName = "Reload";
        emptyEVT = new AnimationEvent();
        emptyEVT.functionName = "Empty";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            anim.Play(fireAnimationName);
        if (Input.GetKey(KeyCode.R))
            anim.Play(reloadAnimationName);
    }

    public void Fire()
    {

    }
    public void Empty()
    {

    }
    public void Reload()
    {

    }
}
