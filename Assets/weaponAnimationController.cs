using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponAnimationController : MonoBehaviour
{
    public float clipSize;
    public bool automatic;

    public TextMesh textMesh;
    Animator anim;
    public float Ammo;
    public string fireAnimationName;
    public string reloadAnimationName;
    public string emptyAnimationName;
    AnimationEvent fireEVT;
    AnimationEvent reloadEVT;
    AnimationEvent emptyEVT;
    // Start is called before the first frame update
    void Start()
    {
        Ammo = clipSize;
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
        anim.SetBool("ADS", Input.GetMouseButton(1));
        textMesh.text = Ammo + "";
        if (Ammo > 0)
        {
            if (((Input.GetMouseButtonDown(0) && !automatic) || (Input.GetMouseButton(0) && automatic)) && anim.GetCurrentAnimatorStateInfo(0).IsName(emptyAnimationName))
            {
                anim.Play(fireAnimationName);
            }
        }
        if (Input.GetKey(KeyCode.R))
            anim.Play(reloadAnimationName);
    }

    public void Fire()
    {
        Ammo--;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit)){
            FindObjectOfType<SceneSoundManager>().SpawnSound(hit.point, 2);
            fireCheckObject(hit.transform.gameObject);
            
        }
    }
    void fireCheckObject(GameObject check)
    {
        if (check.GetComponent<ZombieBehavior>())
            check.GetComponent<ZombieBehavior>().damageZombie();
    }
    public void Empty()
    {
        Ammo = 0;
    }
    public void Reload()
    {
        Ammo = clipSize;
    }
}
