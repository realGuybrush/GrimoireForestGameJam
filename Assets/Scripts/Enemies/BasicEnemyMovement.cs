using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemyMovement : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;
    
    [SerializeField]
    protected Rigidbody2D body;

    [SerializeField]
    private Transform image;

    [SerializeField]
    private Health health;
    
    private Transform player;

    protected Vector3 followPosition;

    [SerializeField]
    private float defaultWalkDistance, chanceToRandomWalk;

    [SerializeField]
    protected float speed;
    
    protected bool followingPlayer;

    [SerializeField]
    private PlayerSeekerTrigger trig, stealthTrig, unseeTrig, biter;
    
    [SerializeField]
    protected float attackCD;

    protected float attackTimer;

    [SerializeField]
    protected Projectile bite;

    [SerializeField]
    private Item drop;

    [SerializeField]
    private float dropChance;

    [SerializeField]
    private bool lookingRight;

    [SerializeField]
    private SoundController soundController;

    private void Awake()
    {
        trig.OnPlayerEnter += SetPlayerIfNotHidden;
        stealthTrig.OnPlayerEnter += SetPlayer;
        unseeTrig.OnPlayerLeave += LosePLayer;
        biter.OnPlayerStay += Attack;
        health.OnDie += Die;
        followPosition = transform.position;
    }

    private void Update()
    {
        ChooseFollowPosition();
        if ((transform.position - followPosition).magnitude > 0.5f)
        {
            FollowPoint();
            animator.SetBool("Move", true);
        }
        else
            animator.SetBool("Move", false);
        SetDirection();
        AttackCDTimer();
    }

    private void SetPlayerIfNotHidden(PlayerMovement newPlayer)
    {
        if (newPlayer.Hidden) return;
        player = newPlayer.transform;
        followingPlayer = true;
    }
    
    private void SetPlayer(PlayerMovement newPlayer)
    {
        player = newPlayer.transform;
        followingPlayer = true;
    }

    private void LosePLayer()
    {
        followingPlayer = false;
    }

    private void ChooseFollowPosition()
    {
        if (followingPlayer)
        {
            followPosition = player.position;
        } else
        {
            if (Random.Range(0, 100) < chanceToRandomWalk)
                followPosition = transform.position + RandomPosAround(defaultWalkDistance);
        }
    }

    protected virtual void FollowPoint()
    {
        
    }

    private void AttackCDTimer()
    {
        if (attackTimer > 0) attackTimer -= Time.deltaTime;
    }

    private void SetDirection()
    {
        if (body.linearVelocityX > 0 && !lookingRight || body.linearVelocityX < 0 && lookingRight)
            Flip();
    }

    private void Flip()
    {
        image.eulerAngles = new Vector3(0f, image.eulerAngles.y > 1f?0f:180f, 0f);
        lookingRight = !lookingRight;
    }

    protected virtual void Attack()
    {
        if (attackTimer > 0) return;
        Instantiate(bite, transform.position, new Quaternion()).Init(followPosition - transform.position, gameObject.GetHashCode());
        attackTimer = attackCD;
        soundController?.PlayRandom();
    }

    protected Vector3 RandomPosAround(float distance)
    {
        return new Vector3(Random.Range(-distance, distance), Random.Range(-distance, distance), 0);
    }

    private void Die()
    {
        Drop();
        Destroy(gameObject);
    }

    private void Drop()
    {
        if (Random.Range(0, 100) < dropChance)
            Instantiate(drop, transform.position, new Quaternion());
    }

    public void BiterSetActive(bool value)
    {
        biter.gameObject.SetActive(value);
    }

    private void OnDestroy()
    {
        trig.OnPlayerEnter -= SetPlayerIfNotHidden;
        stealthTrig.OnPlayerEnter -= SetPlayer;
        unseeTrig.OnPlayerLeave -= LosePLayer;
        biter.OnPlayerStay -= Attack;
        health.OnDie -= Die;
    }
}
