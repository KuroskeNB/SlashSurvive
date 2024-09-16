using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public CurrencyData currencyData;
    public Image goldImage;
    public Image silverImage;
    public Scrollbar scrollbar; // Ссылка на компонент Scrollbar
    private float[] pos;
    private float distance;
    private float scroll_pos;
    private int pageIndex=1;
    bool bCanUpdate=false;
    bool bIsLerping =false;
    private Coroutine smoothScrollCoroutine;

    void Start()
    {
        if(!PlayerPrefs.HasKey("WeaponPrefab"))
        PlayerPrefs.SetString("WeaponPrefab","honorsword");
        if(!PlayerPrefs.HasKey("PlayerPrefab"))
        PlayerPrefs.SetString("PlayerPrefab","KnightHeroPrefab");
        if (scrollbar != null)
        {
            //scrollbar = GetComponent<Scrollbar>(); // Если не назначен, попытайтесь найти компонент на том же объекте
            scrollbar.value=0.5f;
            Debug.Log("u have scrollbar "+scrollbar.value);
        }
    }
    private void OnDestroy() {
        Debug.Log("save currencydata");
        currencyData.Save();
    }

    // Update is called once per frame
    void Update()
    {
        pos=new float[transform.childCount];
        distance=0.5f;
        for(int i =0;i<pos.Length;i++)
        {
            pos[i]=distance*i;
        }
        if(Input.GetMouseButton(0))
        {
            scroll_pos=scrollbar.GetComponent<Scrollbar>().value;
            bCanUpdate=true;
        }
        else if(bCanUpdate){
             bool updated = false;
                for (int i = 0; i < pos.Length; i++)
                {
                    if (Mathf.Abs(scroll_pos-pos[i])<Mathf.Abs(scroll_pos-pos[pageIndex]))
                    {
                        //bCanUpdate=false;
                        //scrollbar.value = Mathf.Lerp(scroll_pos,pos[i],0.1f);
                        pageIndex=i;
                        updated = true;
                        //Debug.Log("pageindex " + pageIndex);
                        break;
                        
                    }
                }
                if (!updated && !Input.GetMouseButton(0))
                {
                
                //Debug.Log("Scrolling to pageIndex " + pageIndex);
                if(scrollbar.value!=pos[pageIndex])
                {
                //Debug.Log(scroll_pos + " move to " + pos[pageIndex]);
                float newPos=Mathf.Lerp(scroll_pos,pos[pageIndex],5*0.01f);
                //Debug.Log(Time.deltaTime);
                   scrollbar.value = (newPos>0)?newPos:0;
                   scroll_pos=scrollbar.value;
                   //Debug.Log("scrollbar " +scrollbar.value);
                }
                }
        }

        if(goldImage.GetComponentInChildren<Text>()!=null)
        goldImage.GetComponentInChildren<Text>().text=currencyData.goldCoins.coinCount.ToString();
        if(goldImage.GetComponentInChildren<Text>()!=null)
        silverImage.GetComponentInChildren<Text>().text=currencyData.silverCoins.coinCount.ToString();
    }
}
