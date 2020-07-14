using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterMovement : MonoBehaviour
{
    public static int damagejump = 0;
    public static Rigidbody2D rb;
    float dirX;
	public static int BulletCount = 3;
	

    private AudioSource audioSrc;

    public GameObject Player_Death_Effect,endgame_bar;

    [SerializeField]
    float moveSpeed = 5f, jumpForce = 600f,bulletSpeed = 500f;

    bool facingRight = true;
	bool DoubleJumpAllowed = false;
	bool OnTheGround = false;
    Vector3 localScale;


    private Animator anim;

    public Transform barrel;
    public Rigidbody2D bullet;
    public static int StopSound = 0;

 
    // Start is called before the first frame update
    void Start()
    {
		WeaponSwitch.weaponLevel = 0;
        audioSrc = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
		endgame_bar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(rb.velocity.y == 0)
		{
			OnTheGround = true;
		}
		else
		{
			OnTheGround = false;
		}
		
	if(OnTheGround)
	{
		DoubleJumpAllowed = true;
	}
		
		
		if(CharacterMovement.damagejump == 1)
		{
			CharacterMovement.damagejump = 0;
			rb.AddForce(Vector2.up * 1000f);
		}
			if(CharacterMovement.damagejump == 2)
		{
			CharacterMovement.damagejump = 0;
			rb.AddForce(Vector2.up * 3000f);
		}
			
        if(HealthBar.health <= 0)
        {
            SoundEffect.PlaySound("playerdeath");
            Instantiate(Player_Death_Effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
			endgame_bar.gameObject.SetActive(true);
        }
       
        dirX = CrossPlatformInputManager.GetAxis("Horizontal")*moveSpeed;

        if (GroundCheck.grounded == false)
            audioSrc.Stop();
        if (dirX == 0)
        {
            anim.SetBool("isRunning", false);
            audioSrc.Stop();

        }
        else
        {
			if(rb.velocity.y == 0)
			{
            anim.SetBool("isRunning", true);
			}
			else{
				anim.SetBool("isRunning",false);
			}
            if (!audioSrc.isPlaying && GroundCheck.grounded == true)
                audioSrc.Play();

        }

        if (GroundCheck.grounded && CrossPlatformInputManager.GetButtonDown("Jump"))
        {

            Jump();
        }
		else if (DoubleJumpAllowed && CrossPlatformInputManager.GetButtonDown("Jump"))
        {

            Jump();
			DoubleJumpAllowed = false;
        }
			
		
		if(rb.velocity.y < -35 && GroundCheck.grounded == true)
		{
			HealthBar.health -= 10;
		}



        if(rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        if(rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }

        if(rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }

        if(CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            
            Fire();
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX*moveSpeed, rb.velocity.y);
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if(dirX > 0)
        {
            facingRight = true;
        }
        else
        {
            if(dirX < 0)
            {
                facingRight = false;
            }
        }
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Jump()
    {
			rb.velocity = new Vector2(rb.velocity.x,0f);
            SoundEffect.PlaySound("jump");
            rb.AddForce(Vector2.up * jumpForce);
        
    }

    void Fire()
    {
		if(CharacterMovement.BulletCount > 0)
		{
			if (facingRight)
			{
				SoundEffect.PlaySound("fire");
				var firedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
				firedBullet.AddForce(barrel.right * bulletSpeed);
			}
			else
			{
				SoundEffect.PlaySound("fire");
				var firedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
				firedBullet.AddForce(-barrel.right * bulletSpeed);
			}
			CharacterMovement.BulletCount -= 1;
		}
    }
	
	
 
}
