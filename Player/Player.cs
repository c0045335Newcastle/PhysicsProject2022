using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float testMultiplier;
    
    
    [SerializeField]
    int targetFrameRate = 90;

    [SerializeField]
    Rigidbody playerRigidBody;

    [SerializeField]
    GameObject mainCamera;
    public int playerSensitivity = 350;

    //  text prefab to display
    [SerializeField]
    GameObject playerPopText;

    //  
    [SerializeField]
    float moveDefaultForce = 650f;
    float moveForce;

    //  UI shown upon dying
    [SerializeField]
    GameObject deadUI;


    //  Player Stats
    public int score = 0;
    public int health = 100;
    public HealthBar healthBar;
    public bool playerIsAlive;

    public enum playerState
    {
        healthy,
        hurt,
        weak,
        critical,
        dead
    }

    public enum playerDifficulty
    {
        beginner,
        regular,
        hardened
    }

    public playerState currentPlayerState;
    public playerDifficulty difficulty;

    void Start()
    {
        //gameObject holding the difficulty - passed from the difficulty menu
        //difficulty = GameObject.FindGameObjectWithTag("Difficulty").GetComponent<Difficulty>().difficulty;
        difficulty = Difficulty.globalDifficulty.difficulty;

        playerIsAlive = true;
        moveForce = moveDefaultForce;

        //  move quicker as a beginner
        if (difficulty == playerDifficulty.beginner)
        {
            moveForce = moveDefaultForce * 1.25f;
        }

        deadUI.gameObject.SetActive(false);

        spawnText(gameObject, Vector3.zero, "Run!", Color.green);

        //  Move to main game script later?
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }


    void FixedUpdate()
    {       
        if (!playerIsAlive)
        {
            //  Dead player cannot move
            moveForce = 0;
        }
        else {
            //  update the player's state depending on health
            setPlayerState();

            if (currentPlayerState == playerState.critical)
            {
                //  injured, move slower
                moveForce = moveDefaultForce * 0.8f;
            }
            else
            {
                //  Alive player moves as normal
                moveForce = moveDefaultForce;
            }

            //  Incrememnt score
            score = Mathf.Max(0, score + 1);
            //  Debug.Log($"score: {score}");
        }

        //..Debug.Log($"MF: {moveForce}, Alive: {playerIsAlive}");

        //  WASD movement corresponds to camera direction
        Vector3 flatForward = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized;
        Vector3 flatRight = new Vector3(mainCamera.transform.right.x, 0, mainCamera.transform.right.z).normalized;

        flatForward = flatForward * moveForce * Time.deltaTime;
        flatRight = flatRight * moveForce * Time.deltaTime;

        if (Input.GetKey("w"))
        {
            playerRigidBody.AddForce(flatForward);
        }
        else if (Input.GetKey("s"))
        {
            playerRigidBody.AddForce(-flatForward);
        }
        else if (Input.GetKey("a"))
        {
            playerRigidBody.AddForce(-flatRight);
        }
        else if (Input.GetKey("d"))
        {
            playerRigidBody.AddForce(flatRight);
        }


        //  Sensitivity
        else if (Input.GetKey("o"))
        {
            playerSensitivity -= 25;
        }
        else if (Input.GetKey("p"))
        {
            playerSensitivity += 25;
        }

        else if (Input.GetKey("k"))
        {
            SceneManager.LoadScene(1);
        }
        //  check for healthpack, alert player if available!
        //healthPackCheck();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            spawnText(collision.gameObject, Vector3.zero, "HIT!", Color.white);
            hitRandomForce();
            reduceRndHealth(11, 34);

            //  delay showing the health state of the player
            //  3 seconds
            //  prevent overlapping text
            Invoke(nameof(revealHealthState), 3);

        }
        else if (collision.gameObject.CompareTag("FlockEnemy"))
        {
            hitRandomForce();
            reduceRndHealth(1, 5);
        }
        else if (collision.gameObject.CompareTag("HealthPack"))
        {
            if (health != 100)
            {
                health += 33;
                healthBar.setHealth(health);

                if (health > 100)
                {
                    //  can't exceed 100 health
                    health = 100;
                }
            }
            else
            {
                spawnText(gameObject, Vector3.zero, "Health Full!", Color.blue);
            }
            
        }
    }

 

    void healthCheck() {
        if (health <= 0)
        {
            playerHasDied();
        }
        healthBar.setHealth(health);

    }


    void playerHasDied() {
        playerIsAlive = false;

        Debug.Log("Player has died!");
        spawnText(gameObject, Vector3.zero, "You died!" , Color.yellow);

        deadUI.gameObject.SetActive(true);

        //  Reset health to zero incase of negative health
        health = 0;
        healthBar.setHealth(0);
    }

    public int getScore() {
        return score;
    }

    public int getHealth() {
        return health;
    }

    public void spawnText(GameObject go, Vector3 additionalPos, string newText, Color newColour) {
        //  Create various text objects attached to the player
        //  Damage taken, Died etc.
        GameObject[] currentTextSpawned = GameObject.FindGameObjectsWithTag("PlayerText");
        if (currentTextSpawned.Length > 0)
        {
            foreach (GameObject textInstance in currentTextSpawned)
            {
                if (textInstance.GetComponent<TextMesh>().color == newColour)
                {
                    Destroy(textInstance);
                    //  Only allow one type of message!
                }
            }
        }

        Vector3 rotation = transform.position;
        var text = Instantiate(playerPopText, go.transform.position + additionalPos, Quaternion.FromToRotation(rotation, rotation), go.transform);
        text.GetComponent<TextMesh>().text = newText;
        text.GetComponent<TextMesh>().color = newColour;
        text.tag = "PlayerText";
    }

    void hitRandomForce() {
        //  When the player is hit, a random force is applied
        Vector3 rndForce = new Vector3();
        rndForce.Set((Random.Range(-1000f, 1000f)),(Random.Range(0, 1000f)),(Random.Range(-1000f, 1000f)));

                //Debug.Log($"HIT FORCE: ({rndForce})");
                //Debug.DrawLine(transform.position, rndForce, Color.cyan);
        playerRigidBody.AddForce(rndForce);
    }

    public void hitRandomForce(float value)
    {
        //  When the player is hit, a random force is applied
        Vector3 rndForce = new Vector3();
        rndForce.Set((Random.Range(-value, value)), (Random.Range(0, 1000f)), (Random.Range(-value, value)));

        //Debug.Log($"HIT FORCE: ({rndForce})");
        //Debug.DrawLine(transform.position, rndForce, Color.cyan);
        playerRigidBody.AddForce(rndForce);
    }


        //void reduceRndHealth(){
        ////  Hitting the player will reduce health by a rnd amount between 11-34
        //int healthDeduction = Random.Range(11, 34);

        ////  reveal the amount of health lost
        //health -= healthDeduction;
        //addText(gameObject, $"-{healthDeduction}", Color.red);

        ////  check whether the player is dead or not
        //healthCheck();
        //}

    public void reduceRndHealth(int low, int high)
    {
        //  Hitting the player will reduce health by a rnd amount between low and high
        int healthDeduction = Random.Range(low, high);

        //  reduce the damage from enemies if the player is a beginner
        if (difficulty == playerDifficulty.beginner)
        {
            healthDeduction = Mathf.FloorToInt(healthDeduction * 0.65f);
        }

        health -= healthDeduction;

        //  reveal the amount of health lost as text above player
        spawnText(gameObject, Vector3.zero, $"-{healthDeduction}", Color.red);

        //  check whether the player is dead or not
        healthCheck();

    }

    public void killPlayer() {
        //  Instantly kill the player - useful for falling out of bounds
        playerHasDied();
    }



    void setPlayerState() {
        if (health > 99)
        {
            currentPlayerState = playerState.healthy;
        }
        else if (health < 100 && health > 75)
        {
            currentPlayerState = playerState.hurt;

        }
        else if (health < 76 && health > 25)
        {
            currentPlayerState = playerState.weak;
        }
        else if (health < 26 && health > 0)
        {
            currentPlayerState = playerState.critical;
        }
        else if (health > 1)
        {
            currentPlayerState = playerState.dead;
        }
    }

    void revealHealthState()
    {
        Vector3 textHeight = new Vector3(2, -1, 0);
        spawnText(gameObject, textHeight, $"i am {currentPlayerState.ToString().ToUpper()}", Color.yellow);
    }

    public void seeHealthPack()
    {
        Vector3 textHeight = new Vector3(0, 3, 0);
        spawnText(gameObject, textHeight, $"           i see a health pack!", Color.cyan);
    }

    //bool healthPackAvailable() {
    //    var HP = GameObject.FindGameObjectWithTag("HealthPack");
    //    if (HP != null)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //void healthPackCheck() {
    //    if (healthPackAvailable())
    //    {
    //        seeHealthPack();
    //    }
    //}
}
