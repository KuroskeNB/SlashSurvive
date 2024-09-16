using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float launchForceMultiplier = 10f;
    public int MoveSpeed=4;
    public ScoreCounter scoreCounter;
    private PlayerData Data;
    private Rigidbody2D _rb;
    private Vector2 startMousePosition;
    private Vector2 currentMousePosition;
    private bool isDragging = false;
    private bool ReadyToLaunch = false;
    private bool isMoving = false;
    private Vector2 targetPosition;
    public bool bWaveFreezed=false;
    public GameObject triggerParticle;
    public GameObject chargeParticle;
    private GameObject tempParticle;
    public LayerMask layerMask;
    public Sprite weaponSprite;
    public GameObject critSprite;
     private bool isFacingRight = false;
     private SpriteRenderer spriteRenderer;
     public AudioClip slashSound;
     public AudioClip missSlashSound;
     private AudioSource audioSource;
     [SerializeField]
     private WeaponData weaponData;
     [SerializeField]
     private EffectsData effectsData;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Data = GetComponent<PlayerData>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        audioSource=GetComponent<AudioSource>();
    }
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Score"))
        scoreCounter=GameObject.FindGameObjectWithTag("Score").GetComponent<ScoreCounter>();
        Time.timeScale=1f;
        foreach(ItemData item in effectsData.effectsData)
        {
         if(item.itemName==PlayerPrefs.GetString("EffectPrefab"))
         {
            if(item.itemPrefab.GetComponent<ParticleDestroy>())
        triggerParticle = item.itemPrefab;
       // chargeParticle=item.itemPrefab;
         }
        }
    }

    void FixedUpdate()
    {
        if(ReadyToLaunch)
        {
            ReadyToLaunch=false;
            targetPosition = (Vector2)transform.position + (currentMousePosition - startMousePosition) * launchForceMultiplier;
            //_rb.MovePosition(Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed));
            PerformBoxCast(gameObject.transform.position,targetPosition,new Vector2(0.75f,0.75f),0,layerMask);
            Flip();
            StartMoving();
        }
    }

    void StartMoving()
    {
        isMoving = true;
    }

    void Update() {  

        if(Input.GetKeyDown(KeyCode.Space))
        StartCoroutine(SlowMo());
        
        if(Input.GetKeyDown(KeyCode.R))
        Data.RebuildData();

        Movement();
    }
    void Movement()
    {
        Touch touch;
        if (Input.touchCount > 0)
        touch = Input.GetTouch(0);
        if (Input.GetMouseButtonDown(0) )//|| touch.phase==TouchPhase.Began)
        {
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            if(!tempParticle)
            {
           tempParticle= Instantiate(chargeParticle,transform.position,transform.rotation,transform);
          
            }
        }

        if (isDragging)
        {
            currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//(touch)?Camera.main.ScreenToWorldPoint(touch.position):Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DrawLine(startMousePosition, currentMousePosition, Color.red);
        }

        if (Input.GetMouseButtonUp(0)) //|| touch.phase==TouchPhase.Ended)
        {
            isDragging = false;
            ReadyToLaunch=true;
            Destroy(tempParticle);
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput < 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0 && isFacingRight)
        {
            Flip();
        }

       if (isMoving)
        {
            // Плавное перемещение объекта
            _rb.MovePosition(Vector2.Lerp(_rb.position, targetPosition, MoveSpeed*5 *0.01f));

            // Проверка, достиг ли объект целевой позиции
            if (Vector2.Distance(_rb.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

     void OnDrawGizmos()
    {
        if (isDragging)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startMousePosition, currentMousePosition);
        }
    }
    void DrawLine(Vector2 start, Vector2 end, Color color)
    {
        Debug.DrawLine(start, end, color);
    }


   void PerformBoxCast(Vector2 start, Vector2 end, Vector2 size, float angle, LayerMask mask)
    {
        // Вычисление направления и расстояния
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);

        // Выполнение BoxCast
        RaycastHit2D[] hits = Physics2D.BoxCastAll(start, size, angle, direction, distance, mask);

        // Визуализация BoxCast
        Debug.DrawLine(start, end, Color.red,5f);
        int damage=Data.Damage;
        if(hits.Length>=1)
        {
        damage = Data.SumDamage();
            if(damage>Data.Damage)
            {
            Debug.Log("crit!!! " + damage);
            if(critSprite)
           GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UIManager>().Notification(critSprite);
            }
        }

        foreach (RaycastHit2D hit in hits)
        {
            EnemyData enemy = hit.collider.gameObject.GetComponent<EnemyData>();
            Instantiate(triggerParticle, hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.rotation);
            //count combo
            if(scoreCounter&&enemy.GetHealth()==enemy.MaxHealth && enemy.GetHealth()<damage) scoreCounter.comboCounter.CountCombo(ComboType.OneHitKills,1);

            if(scoreCounter&&enemy.GetHealth()<damage)scoreCounter.AddScore(enemy.ScoreReward);
            enemy.ApplyDamage(damage,gameObject);
            Data.ChargeUlt();
        }
        if(hits.Length>=3)
        scoreCounter.comboCounter.CountCombo(ComboType.MultiplyAttack);
        audioSource.clip=(hits.Length>0)?slashSound:missSlashSound;
        //if(!audioSource.isPlaying)
        audioSource.Play();
    }
     void Flip()
    {
        if(startMousePosition.x>currentMousePosition.x && !isFacingRight) return;
        if(startMousePosition.x<currentMousePosition.x && isFacingRight)return;

        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    public IEnumerator SlowMo()
    {
        bWaveFreezed=true;
     GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
     foreach(GameObject enemy in objects)
     {
      if(enemy.GetComponent<EnemyController>())
      enemy.GetComponent<EnemyController>().enabled=false;
      if(enemy.GetComponent<BomberController>())
      enemy.GetComponent<BomberController>().enabled=false;
     }
        yield return new WaitForSeconds(3f);
        foreach(GameObject enemy in objects)
     {
      if(enemy && enemy.GetComponent<EnemyController>())
      enemy.GetComponent<EnemyController>().enabled=true;
      if(enemy && enemy.GetComponent<BomberController>())
      enemy.GetComponent<BomberController>().enabled=true;
      bWaveFreezed=false;
     }
    }
}
