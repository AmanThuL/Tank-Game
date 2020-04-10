using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TankControls : MonoBehaviour
{
    // Movement
    [Header("Movement")]
    [SerializeField] private Vector3 acceleration;
    [SerializeField] [Range(0, 5)] private float accelRate;
    [SerializeField] [Range(0, 1)] private float deceleration;
    [SerializeField] [Range(0, 20)] private float maxSpeed;
    [SerializeField] private bool isMovable = false;

    [Header("Rotation")]
    [SerializeField] [Range(30, 200)] private float turnSpeed;
    //[SerializeField] private float angleOfRotation;

    // Setters
    public float AccelRate { set => accelRate = value; }
    public float Deceleration { set => deceleration = value; }
    public float MaxSpeed { set => maxSpeed = value; }
    public float TurnSpeed { set => turnSpeed = value; }

    //hold what the actual movement is of the tank
    [Header("Tank Properties")]
    public Vector3 tankPos;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 direction;

    private bool ifDecelerating = false;
    [Header("Controls")]
    [SerializeField] KeyCode moveUp;
    [SerializeField] KeyCode moveDown;
    [SerializeField] KeyCode moveLeft;
    [SerializeField] KeyCode moveRight;
    [SerializeField] KeyCode shoot;

    public Rigidbody2D rb;

    [Header("Bullet Fire")]
    [SerializeField] GameObject smoke;
    //Bullet fire rate
    public GameObject bullet;
    [SerializeField][Range (0,2)]float fireRate = .4f;
    float nextFire = 0f;

    //Bullet Related Fields
    Text ammoText;
    char tankID;

    [Header("Power Ups")]
    //Power Ups
    bool infAmmo;
    [SerializeField] [Range(1, 10)] int infAmmoDelay = 5;
    float infAmmoTime;

    bool speedUp;
    [SerializeField] [Range(1, 10)] int speedUpDelay = 5;
    float speedUpTime;

    bool doubleShot;
    [SerializeField] [Range(1, 10)] int doubleShotDelay = 2;
    float doubleShotTime;

    public bool shield;

    bool spreadShot;
    [SerializeField] [Range(1, 10)] int spreadShotDelay = 2;
    float spreadShotTime;


    // UI
    private GameObject UIManager;
   

    // start is called before the first frame update
    void Start()
    {
        infAmmo = false;
        speedUp = false;
        doubleShot = false;
        shield = false;
        spreadShot = false;
        velocity = Vector3.zero;
        tankPos = transform.position;
        velocity = Vector3.zero;
        tankID = gameObject.tag[0];

        UIManager = GameObject.Find("UI Manager");

        if (this.name == "Blue_Tank(Clone)")
        {
            ammoText = GameObject.Find("BlueText").GetComponent<Text>();
            if (GameStats.Instance.player1Color)
            {
                GetComponent<SpriteRenderer>().sprite = GameStats.Instance.player1Color;
            }
        }
        else
        {
            ammoText = GameObject.Find("RedText").GetComponent<Text>();
            if (GameStats.Instance.player2Color)
            {
                GetComponent<SpriteRenderer>().sprite = GameStats.Instance.player2Color;
            }
        }
    }

    //get player input in update
    void Update()
    {
        if (GameStats.Instance.isInputEnabled)
        {
            Rotate();
            Move();
            Decelerate();
            ShootBullet();
            AmmoText();
            RemovePowerUp();
        }
    }

    void AmmoText()
    {
        int currAmmonText = int.Parse(ammoText.text);

        // Check if UI needs update
        if (currAmmonText != GameStats.Instance.getBullets(tankID))
        {
            ammoText.text = GameStats.Instance.getBullets(tankID).ToString();
            UIManager.GetComponent<UIManager>().UpdateAmmoUI(tankID);
        }
    }

    public void AddAmmo()
    {
        for (int j = 0; j < 3; j++)
        {
            GameStats.Instance.incrementBullets(tankID);
        }
    }

    public void DoubleShot()
    {
        doubleShotTime = Time.time;
        doubleShot = true;
        GameStats.Instance.currActivePowerup = Powerup.DoubleShot;

        // Enable UI
        UIManager.GetComponent<UIManager>().ActivatePowerupUI(doubleShotDelay, tankID == 'B');
    }

    public void InfiniteAmmo()
    {
        infAmmoTime = Time.time;
        infAmmo = true;
        GameStats.Instance.currActivePowerup = Powerup.InfAmmo;

        // Enable UI
        UIManager.GetComponent<UIManager>().ActivatePowerupUI(doubleShotDelay, tankID == 'B');
    }

    public void SpeedUp()
    {
        speedUp = true;
        GameStats.Instance.currActivePowerup = Powerup.SpeedUp;

        // Enable UI
        UIManager.GetComponent<UIManager>().ActivatePowerupUI(doubleShotDelay, tankID == 'B');

        speedUpTime = Time.time;
        accelRate += .4f;
        maxSpeed += 2f;
    }

    public void Shield()
    {
        shield = true;
        GameStats.Instance.currActivePowerup = Powerup.Shield;

        // Enable UI
        UIManager.GetComponent<UIManager>().ActivatePowerupUI(doubleShotDelay, tankID == 'B');
    }

    public void SpreadShot()
    {
        spreadShot = true;
        GameStats.Instance.currActivePowerup = Powerup.SpreadShot;

        // Enable UI
        UIManager.GetComponent<UIManager>().ActivatePowerupUI(doubleShotDelay, tankID == 'B');

        spreadShotTime = Time.time;
    }

    public void RemovePowerUp()
    {
        GameStats.Instance.currActivePowerup = null;
        //Remove Infinite Ammo
        if (infAmmo == true && Time.time > infAmmoTime + infAmmoDelay)
        {
            infAmmo = false;
        }

        //Remove Speed Up
        if (speedUp == true && Time.time > speedUpTime + speedUpDelay)
        {
            speedUp = false;
            accelRate -= .4f;
            maxSpeed -= 2f;
        }

        if (doubleShot == true && Time.time > doubleShotTime + doubleShotDelay)
        {
            doubleShot = false;
        }

        if (spreadShot == true && Time.time > spreadShotTime + spreadShotDelay)
        {
            spreadShot = false;
        }
    }

    /// <summary>
    /// Sets the rigidbody of the tank
    /// </summary>
    protected void SetTransform()
    {
        //update info
        transform.rotation = Quaternion.Euler(0f,0f,Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg);
        transform.position += velocity * Time.deltaTime;
    }

    /// <summary>
    /// Rotates the tank using A/D key
    /// </summary>
    private void Rotate()
    {
        //turn the tank
        //rotate using angles
        if (Input.GetKey(moveLeft))
        {
            //angleOfRotation += turnSpeed * Time.deltaTime;
            direction = Quaternion.Euler(0, 0, turnSpeed * Time.deltaTime) * direction;
        }
        if (Input.GetKey(moveRight))
        {
            //angleOfRotation -= turnSpeed * Time.deltaTime;
            direction = Quaternion.Euler(0, 0, -turnSpeed * Time.deltaTime) * direction;
        }

        direction.Normalize();
        SetTransform();
    }

    /// <summary>
    /// The tank moves forward/backward
    /// </summary>
    private void Move()
    {
        //this grouping handles acceleration and deceleration
        if (Input.GetKey(moveUp))
        {
            ifDecelerating = false;

            acceleration = accelRate * direction * Time.deltaTime;

            //forward movement
            //increase speed
            velocity = (velocity.magnitude + acceleration.magnitude) *direction;

            //speed does not go over max speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else if (Input.GetKey(moveDown))
        { 
            ifDecelerating = false;

            acceleration = accelRate * direction * Time.deltaTime;

            //backward movement
            //decrease speed
            velocity = -(velocity.magnitude + acceleration.magnitude) * direction;

            //speed does not go over max speed
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        }

        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Decelerates the tank
    /// </summary>
    private void Decelerate()
    {
        if ((Input.GetKeyUp(moveUp) || Input.GetKeyUp(moveDown)) && velocity != Vector3.zero)
        {
            ifDecelerating = true;
        }

        if (ifDecelerating)
        {
            velocity *= deceleration;

            if (velocity.magnitude <= 0.05f)
            {
                velocity = Vector3.zero;

                ifDecelerating = false;
            }
            SetTransform();
        }
    }

    void ShootBullet()
    {
        GameObject tempBullet1;
        GameObject tempBullet2;
        GameObject tempBullet3;

        if (Input.GetKey(shoot) && Time.time > nextFire && GameStats.Instance.getBullets(tankID) > 0 || Input.GetKey(shoot) && Time.time > nextFire && infAmmo == true )
        {
            Debug.Log(GameStats.Instance.blueBullets);

            nextFire = Time.time + fireRate;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (doubleShot)
            {
                tempBullet1 = Instantiate(bullet, transform.position + direction * .35f, Quaternion.Euler(0, 0, angle));
                tempBullet1.tag = gameObject.tag.Substring(0, 3) + "Bullet";
                tempBullet1.GetComponent<Bullet_Test>().InitializeDoubleBullet(direction, 1);

                tempBullet2 = Instantiate(bullet, transform.position + direction * .35f, Quaternion.Euler(0, 0, angle));
                tempBullet2.tag = gameObject.tag.Substring(0, 3) + "Bullet";
                tempBullet2.GetComponent<Bullet_Test>().InitializeDoubleBullet(direction, 2);
            }
            else if (spreadShot)
            {
                tempBullet1 = Instantiate(bullet, transform.position + direction * .35f, Quaternion.Euler(0, 0, angle));
                tempBullet1.tag = gameObject.tag.Substring(0, 3) + "Bullet";
                tempBullet1.GetComponent<Bullet_Test>().InitializeSpreadShot(direction, 1);

                tempBullet2 = Instantiate(bullet, transform.position + direction * .35f, Quaternion.Euler(0, 0, angle));
                tempBullet2.tag = gameObject.tag.Substring(0, 3) + "Bullet";
                tempBullet2.GetComponent<Bullet_Test>().InitializeSpreadShot(direction, 2);

                tempBullet3 = Instantiate(bullet, transform.position + direction * .35f, Quaternion.Euler(0, 0, angle));
                tempBullet3.tag = gameObject.tag.Substring(0, 3) + "Bullet";
                tempBullet3.GetComponent<Bullet_Test>().InitializeSpreadShot(direction, 3);
            }
            else
            {
                tempBullet1 = Instantiate(bullet, transform.position + direction * .35f, Quaternion.Euler(0, 0, angle));
                tempBullet1.tag = gameObject.tag.Substring(0, 3) + "Bullet";
                tempBullet1.GetComponent<Bullet_Test>().Initialize(direction);
            }

            //Instantiate(smoke, new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z), Quaternion.identity);

            if (!infAmmo)
            {
                GameStats.Instance.decrementBullets(tankID);
            }

        }
    }
    
    public void addBullet()
    {
        GameStats.Instance.incrementBullets(gameObject.tag[0]);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;
        if (collisionObj.tag == "Crate" || collisionObj.layer == LayerMask.NameToLayer("Tiles"))
        {
            isMovable = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;
        if (collisionObj.tag == "Crate" || collisionObj.layer == LayerMask.NameToLayer("Tiles"))
        {
            isMovable = true;
        }
    }
}
