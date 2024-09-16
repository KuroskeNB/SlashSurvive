using UnityEngine;

public class ComboCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public int MultiplyAttacksCount=0;
    public int NonStopSlashesCount=0;
    public int OneHitKillsCount=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CountCombo(ComboType type,int count=0)
    {
        switch(type)
        {
        case ComboType.MultiplyAttack:
          MultiplyAttacksCount++;
          break;
        case ComboType.NonStopSlashes:
          NonStopSlashesCount+=count;
          break;
        case ComboType.OneHitKills:
          OneHitKillsCount+=count;
          break;

        }
    }
}
public enum ComboType
    {
        MultiplyAttack,
        NonStopSlashes,
        OneHitKills
    }
