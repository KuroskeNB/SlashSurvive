using System.Collections;
using UnityEngine;

public class BomberController : MonoBehaviour
{
    private Transform target;
    public GameObject boomEffect;
    public float speed=3;
    // Start is called before the first frame update
    private bool isFacingRight = false;
     private Vector2 lastPosition;
     private bool bCanAttack=true;
    // Start is called before the first frame update
    void Start()
    {
        target=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition=Vector2.MoveTowards(transform.position,target.position,speed*Time.fixedDeltaTime);
        transform.position = newPosition;
        Vector2 direction = newPosition - lastPosition;

        if(Vector2.Distance(transform.position,target.position)<1.5)
        {
         StartCoroutine(Boom());
        }

        // Флиппинг спрайта в зависимости от направления
        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }

        lastPosition = transform.position;
    }

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(0.25f);
        Vector2 direction = (transform.position).normalized;
        float distance = Vector2.Distance(transform.position, transform.position);
        if(boomEffect)
        Instantiate(boomEffect,transform.position,transform.rotation);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(4,4),0f, direction, distance, LayerMask.GetMask("Player"));

        if(hit.collider!=null &&hit.collider.gameObject.GetComponent<PlayerData>())
        {
            //Debug.Log("bomber hit");
          hit.collider.gameObject.GetComponent<PlayerData>().ApplyDamage(GetComponent<EnemyData>().Damage,gameObject);
        }
        else{
            Debug.Log("bomber did not hit");
        }
        Destroy(gameObject);

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
