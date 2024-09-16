using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    ///
    /// PROPERTIES
    ///
    //public GameData gameData;
    [SerializeField]
    private Sprite[] backSprites;
    public LevelData levelData;
    public HeroesData heroesData;
    public PlayerData playerData;
    public float SpawnRate=0.5f;
    public float TimeBetweenWaves=3f;
    public int WavesPerLevel=3;
    private int WavesSpawned=0;
    private int SpawnedCount=0;
    public float enemyMulti=1;
    public Transform[] transformsToSpawn;
    
    public delegate void OnLevelEnd(bool bIsWon);
    public event OnLevelEnd onLevelEnd;

    public delegate void OnNewChance();

    bool LevelEnded=false;
 public event OnNewChance onNewChanceEvent;

    ///
    ///  FUNCTIONS
    ///
    void Update()
    {
        if(playerData && playerData.CurrentHealth<=0)
        {
            LevelFailed();
        }
        else if(playerData&& playerData.CurrentHealth>0&&LevelEnded)
        {
            LevelEnded=false;
        }
    } 
    void Start()
    {
        if(backSprites.Length>1)
        GetComponent<SpriteRenderer>().sprite=backSprites[Random.Range(0,backSprites.Length)];
        SpawnPlayer();
        StartLevel();
    }
    void SpawnPlayer()
    {
        GameObject objectToSpawn=null;
        for(int i=0;i<heroesData.heroData.Length;i++)
        {
            if(heroesData.heroData[i].itemName==PlayerPrefs.GetString("PlayerPrefab"))
            {
                objectToSpawn=Instantiate(heroesData.heroData[i].itemPrefab,new Vector2(0,0),gameObject.transform.rotation);
            }
        }
        if(objectToSpawn!=null)
        playerData=objectToSpawn.GetComponent<PlayerData>();
        if(objectToSpawn==null)
        Debug.Log("wtf maaaan");
    }
    
    public void StartLevel()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int levelToSpawn=Random.Range(0,levelData.LevelsData.Length);
           for(int i =0;i<levelData.LevelsData[levelToSpawn].WavesData.Length;i++)
           {

            for(int k=0;k<levelData.LevelsData[levelToSpawn].WavesData[i].enemyPerWave;k++)
            {
            GameObject SpawnedChar = Instantiate(levelData.LevelsData[levelToSpawn].WavesData[i].enemy,transformsToSpawn[Random.Range(0,transformsToSpawn.Length)].position,transformsToSpawn[Random.Range(0,transformsToSpawn.Length-1)].rotation);
            SpawnedChar.GetComponent<EnemyData>().onDestroyEvent+=OnSpawnedDestroyed;
            SpawnedChar.GetComponent<EnemyData>().ScaleData(enemyMulti);
            SpawnedCount++;
            yield return new WaitForSeconds(0.1f);
            }

           }
        WavesSpawned++;
        if(WavesSpawned%5==0)
        {
            SpawnRate-=(SpawnRate>0.2f)?0.1f:0;
            TimeBetweenWaves-=(TimeBetweenWaves>1.5f)?0.5f:0;
        }
        if(WavesSpawned%3==0)
        {
            enemyMulti+=0.05f;
        }

        yield return new WaitForSeconds(TimeBetweenWaves);
        if(playerData.GetComponent<PlayerController>().bWaveFreezed)
        yield return new WaitForSeconds(3f);
        
        if( playerData.gameObject.GetComponent<PlayerController>().scoreCounter)
        Debug.Log("score is " + playerData.gameObject.GetComponent<PlayerController>().scoreCounter.GetScore());
        StartCoroutine(SpawnWave());
    }

     private void OnSpawnedDestroyed()
    {
    //Debug.Log("OtherObject destroyed! Performing actions...");
        SpawnedCount--;
        if(SpawnedCount<=0 && WavesSpawned==WavesPerLevel)
        {
            //LevelEnd();
        }
    }

    private void LevelFailed()
    {
        onLevelEnd(false);
        if(playerData.GetComponent<PlayerController>().scoreCounter && !LevelEnded)
        playerData.GetComponent<PlayerController>().scoreCounter.GameEnd(playerData.RewardMultiply);
               LevelEnded=true;
    }

    public void NewChance()
    {
        onNewChanceEvent();
        playerData.CurrentHealth=(int)(playerData.MaxHealth*0.2);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}
