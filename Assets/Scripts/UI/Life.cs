using UnityEngine;

public class Life : MonoBehaviour
{
    private int health = 2;
    private int hits = 0;

    private bool isCurrentLife = false;

    private Animator anim;
    
    private void Awake() 
    {
        anim = GetComponent<Animator>();    
    }

    public void Hit()
    {
        hits += 1;

        if (hits < health)
        {
            anim.SetTrigger("Hit");
        }else{
            hits = health;
            anim.SetTrigger("Dead");
        }
    }

    public void TriggerLifeActivator()
    {
        isCurrentLife = !isCurrentLife;

        if (isCurrentLife) {
            anim.SetTrigger("Current");
        }
    }
}
