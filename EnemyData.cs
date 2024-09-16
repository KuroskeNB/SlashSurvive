using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
   public int MaxHealth=100;
private int CurrentHealth;
public float Damage=25;
public int ScoreReward=1;
public delegate void OnDestroyDelegate();

 public event OnDestroyDelegate onDestroyEvent;

    private void OnDestroy()
    {
        if (onDestroyEvent != null)
        {
            onDestroyEvent(); // Вызываем событие
        }
    }
    public int GetHealth(){return CurrentHealth;}
    void Start()
    {
        CurrentHealth=MaxHealth;
    }
    public void ApplyDamage(int damage, GameObject DamageCauser)
    {
        CurrentHealth-=damage;
        if(CurrentHealth<=0)
        {
            if(GetComponent<HealObject>())
            {
                Heal(DamageCauser);
            }
            Destroy(gameObject);
        }
    }
    public void ScaleData(float multiply)
    {
        MaxHealth=(int)(MaxHealth*multiply);
        CurrentHealth=MaxHealth;
        //Damage=(int)(Damage*(multiply));
    }
    void Heal(GameObject DamageCauser)
    {
     DamageCauser.GetComponent<PlayerData>().CurrentHealth+=(float)(DamageCauser.GetComponent<PlayerData>().MaxHealth*0.1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
