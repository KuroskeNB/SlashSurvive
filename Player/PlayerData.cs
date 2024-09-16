using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject weaponPlace;
    private GameObject tempWeapon;
    public GameObject CurrentWeapon;
    private string CurrentWeaponName;
    [SerializeField]
    private WeaponData weaponData;
public float MaxHealth=100;
public float CurrentHealth;
public float CurrentUltCharge;
public float UltChargeMultiply=1;
public int MaxUltCharge=100;
public int Damage=25;
public float CritRate=10;
public float CritDamage=25;
public float RewardMultiply=1;
public AudioClip hitSound;
private AudioSource audioSource;

    void Start()
    {
        weaponPlace=transform.GetChild(0).gameObject;
        LoadStats();
        CurrentHealth=MaxHealth;
        audioSource=GetComponent<AudioSource>();
        Debug.Log("max health is "+MaxHealth);
        CurrentWeaponName="none";
        if(CurrentWeapon)
        {
            Debug.Log("Current weapon");
        tempWeapon=Instantiate(CurrentWeapon,weaponPlace.transform.position,weaponPlace.transform.rotation,weaponPlace.transform);
        CurrentWeaponName=CurrentWeapon.name;
        }
    }
    void ChangeWeapon()
    {
        foreach(ItemData item in weaponData.weaponData)
        {
         //if(item.itemName==PlayerPrefs.GetString("WeaponPrefab"))
        //CurrentWeapon = item.itemPrefab;
        }
        if(tempWeapon)
        Destroy(tempWeapon);
        tempWeapon=Instantiate(CurrentWeapon,weaponPlace.transform.position,weaponPlace.transform.rotation,weaponPlace.transform);
        CurrentWeaponName=CurrentWeapon.name;
    }
    public void ChargeUlt()
    {
        if(CurrentUltCharge>=MaxUltCharge) return;
        CurrentUltCharge+=1.5f*UltChargeMultiply;
    }
    void LoadStats()
    {
        if(PlayerPrefs.GetInt("PlayerMaxHealth")>0)
        MaxHealth=PlayerPrefs.GetInt("PlayerMaxHealth");
        if(PlayerPrefs.GetInt("PlayerDamage")>0)
        Damage=PlayerPrefs.GetInt("PlayerDamage");
        if(PlayerPrefs.GetInt("PlayerCritRate")>0)
        CritRate=PlayerPrefs.GetInt("PlayerCritRate");
        if(PlayerPrefs.GetInt("PlayerCritDamage")>0)
        CritDamage=PlayerPrefs.GetInt("PlayerCritDamage");
        if(PlayerPrefs.GetFloat("PlayerRewardMultiply")>0)
        RewardMultiply=PlayerPrefs.GetFloat("PlayerRewardMultiply");
        if(PlayerPrefs.GetFloat("PlayerUltChargeMultiply")>0)
        UltChargeMultiply=PlayerPrefs.GetFloat("PlayerUltChargeMultiply");
    }
    public void ApplyDamage(float damage, GameObject DamageCauser)
    {
        audioSource.clip=hitSound;
        if(!audioSource.isPlaying)
        audioSource.Play();
        //Debug.Log("damage recieved");
        CurrentHealth-=damage;
    }
    // Update is called once per frame
    void Update()
    {
        if(!PlayerPrefs.GetString("WeaponPrefab").Contains(CurrentWeaponName))
        {
        foreach(ItemData item in weaponData.weaponData)
        {
            if(item.itemPrefab==null)
            continue;
            if(PlayerPrefs.GetString("WeaponPrefab").Contains(item.itemPrefab.name))
            {
            CurrentWeapon=item.itemPrefab;
            }
        }
        }
        if(CurrentWeapon&& CurrentWeapon.name!=CurrentWeaponName)
        ChangeWeapon();
    }
    public void UpStats(StatTypes type)
    {
        switch(type)
        {
            case StatTypes.Health:
            MaxHealth+=10;
            PlayerPrefs.SetInt("PlayerMaxHealth",(int)MaxHealth);
            Debug.Log("your max health is " + MaxHealth);
            break;
            case StatTypes.Damage:
            Damage+=2;
            PlayerPrefs.SetInt("PlayerDamage",(int)Damage);
            break;
            case StatTypes.CritRate:
            CritRate+=2;
            PlayerPrefs.SetInt("PlayerCritRate",(int)CritRate);
            break;
            case StatTypes.CritDamage:
            CritDamage+=5;
            PlayerPrefs.SetInt("PlayerCritDamage",(int)CritDamage);
            break;
            case StatTypes.RewardMultiply:
            RewardMultiply+=0.05f;
            PlayerPrefs.SetFloat("PlayerRewardMultiply",RewardMultiply);
            break;
            case StatTypes.UltChargeMultiply:
            UltChargeMultiply+=0.1f;
            PlayerPrefs.SetFloat("PlayerUltChargeMultiply",UltChargeMultiply);
            break;
        }

    }
    public int SumDamage()
    {
        Debug.Log("sum damage");
        int damage=Damage;
    if(Random.Range(0,100)<=CritRate)
    {
       damage =Damage+(int)(Damage*(CritDamage/100));
    }
        return damage;
    }
    public void RebuildData()
    {
        PlayerPrefs.SetInt("PlayerMaxHealth",100);
        PlayerPrefs.SetInt("PlayerDamage",25);
        PlayerPrefs.SetInt("PlayerCritRate",(int)10);
        PlayerPrefs.SetInt("PlayerCritDamage",(int)25);
        PlayerPrefs.SetFloat("PlayerRewardMultiply",1);
        PlayerPrefs.SetFloat("PlayerUltChargeMultiply",1);

        PlayerPrefs.SetInt("MaximalScore",0);
    }

    public float GetStatValue(StatTypes statType)
    {
     switch(statType){
        case StatTypes.Health:
        return MaxHealth;
        case StatTypes.Damage:
        return Damage;
        case StatTypes.CritRate:
        return CritRate;
        case StatTypes.CritDamage:
        return CritDamage;
        case StatTypes.RewardMultiply:
        return RewardMultiply;
        case StatTypes.UltChargeMultiply:
        return UltChargeMultiply;
     }
     return 0;
    }
    public float GetStatLevelUp(StatTypes statType)
    {
        switch(statType){
        case StatTypes.Health:
        return 10f;
        case StatTypes.Damage:
        return 2f;
        case StatTypes.CritRate:
        return 2f;
        case StatTypes.CritDamage:
        return 5f;
        case StatTypes.RewardMultiply:
        return 0.05f;
        case StatTypes.UltChargeMultiply:
        return 0.1f;
        }
        return 0;
    }
}
public enum StatTypes
{
    Health,
    Damage,
    CritRate,
    CritDamage,
    RewardMultiply,
    UltChargeMultiply
}
