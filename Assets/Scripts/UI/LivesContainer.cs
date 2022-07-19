using UnityEngine;
using System.Collections.Generic;

public class LivesContainer : MonoBehaviour
{
    [SerializeField] private List<Life> lives;

    private int currentLife = 2;
    private int totalHealth;
    private bool isDead = false;

    private void Start() 
    {
        totalHealth = 6;
        ActivateLife(currentLife);
    }

    public void Hit()
    {
        if (!isDead)
        {
            totalHealth -= 1;
            lives[currentLife].Hit();

            if (totalHealth >= 1)
            {
                if (totalHealth % 2 == 0 && totalHealth != 0)
                {
                    currentLife -= 1;
                    ActivateLife(currentLife);
                }
            }else{
                isDead = true;
            }
        }
    }

    private void ActivateLife(int lifeIndex)
    {
        lives[lifeIndex].TriggerLifeActivator();
    }

    public bool IsDead()
    {
        return isDead;
    }
}
