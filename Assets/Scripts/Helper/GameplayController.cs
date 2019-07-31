using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public GameObject[] obstaclePrefabs, zombiePrefabs;
    public Transform[] lanes;
    public float min_obstacle_delay = 10f, max_obstacle_delay = 40f;

    private float half_ground_size;
    private Image shoot_btn;
    private BaseController playerController;
    private Text score_text;
    private int zombie_kill_count;
    [SerializeField]
    private GameObject pause_panel;
    [SerializeField]
    private GameObject gameover_panel;

    [SerializeField]
    private Text final_score;

    private void Awake() {
        MakeInstance();
    }

    /**
        Gets a few game objects and starts the GenerateObstacles as a coroutine.
    */
    void Start()
    {
        half_ground_size = GameObject
            .Find("GroundBlock Main")
            .GetComponent<GroundBlock>().halfLength;
        
        shoot_btn = GameObject
            .Find("ShootBtn")
            .GetComponent<Image>();

        playerController = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<BaseController>();

        StartCoroutine("GenerateObstacles");
        score_text = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    /**
        Creates a singleton that only lasts within the current scene
    */
    void MakeInstance(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    /** 
        As the time delay finishes, this function will call the create obstacles
        function and start itself as a coroutine recursively.

        @returns {IEnumerator} returns time delay that is a range from min and
        max obstacle delay, divided by the playerController.speed.z
    */
    IEnumerator GenerateObstacles(){
        float timer = Random.Range(
            min_obstacle_delay, 
            max_obstacle_delay
        )/playerController.speed.z;
        print("timer is " + timer);
        yield return new WaitForSeconds(timer);
        CreateObstacles(
            playerController.gameObject.transform.position.z + half_ground_size
        );
        StartCoroutine("GenerateObstacles");
    }

    /**
        This function will create both obstacles and zombies at the given z
        position. This function will also determine to spawn the obstacle/zombie
        in the middle, left or right lane.

        @param {float} The z position of where the obstacle will be created at.
    */
    void CreateObstacles(float zPos){
        int r = Random.Range(0, 10);
        if(0 <= r && r < 8){
            int obstacleLane = Random.Range(0, lanes.Length);
            AddObstacle(
                new Vector3(lanes[obstacleLane].transform.position.x, 0f, zPos), 
                Random.Range(0, obstaclePrefabs.Length)
            );
            int zombieLane = 0;
            if(obstacleLane == 0){
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 2;
            } else if(obstacleLane == 1){
                zombieLane = Random.Range(0, 2) == 1 ? 0 : 2;
            } else if(obstacleLane == 2){
                zombieLane = Random.Range(0, 2) == 1 ? 1 : 0;
            }
            AddZombies(
                new Vector3(
                    lanes[obstacleLane].transform.position.x, 
                    0.15f, 
                    zPos
                )
            );
        }
    }

    /**
        This function will create an obstacle at the given vector3 position and
        the type of obstacle that is created must also be given. Depending on
        the type of obstacle that is created, the rotation will be mirrored
        correctly.

        @param {Vector3} Position of the obstacle to be generated
        @param {int} index obestacle to be created from obstaclePrefabs array
    */
    void AddObstacle(Vector3 pos, int type){
        GameObject obstalce = Instantiate(
            obstaclePrefabs[type], 
            pos, 
            Quaternion.identity
        );
        bool mirror = Random.Range(0, 2) == 1;

        switch (type)
        {
            case 0:
                obstalce.transform.rotation = Quaternion.Euler(
                    0f, 
                    mirror ? -20 : 20, 
                    0f
                );
                break;
            case 1:
                obstalce.transform.rotation = Quaternion.Euler(
                    0f, 
                    mirror ? -20 : 20, 
                    0f
                );
                break;
            case 2:
                obstalce.transform.rotation = Quaternion.Euler(
                    0f, 
                    mirror ? -1 : 1, 
                    0f
                );
                break;
            case 3:
                obstalce.transform.rotation = Quaternion.Euler(
                    0f, 
                    mirror ? -170 : 170, 
                    0f
                );
                break;
        }
        obstalce.transform.position = pos;
    }

    /**
        This function create a zombie at the given vector3 position. The type of
        zombie that is created is determined randomly. 

        @param {Vector3} Position of the zpmbie to be created.
    */
    void AddZombies(Vector3 pos){
        int count = Random.Range(0, 3) +1;
        for(int i = 0; i < count; i++){
            Vector3 shift = new Vector3 (
                Random.Range(-0.5f, 0.5f), 
                0f, 
                Random.Range(1f, 10f) * i
            );
            Instantiate(
                zombiePrefabs[Random.Range(0, zombiePrefabs.Length)],
                pos + shift * i, 
                Quaternion.identity
            );
        }
    }

    /**
        Increases the zombie kill count and updates the score text.
    */
    public void IncreaseScore(){
        zombie_kill_count++;
        score_text.text = zombie_kill_count.ToString();
    }

    /**
        Activates the pause panel game object, disables the shoot button and
        sets the timeScale to 0.
    */
    public void PauseGame(){
        pause_panel.SetActive(true);
        shoot_btn.raycastTarget = false;
        Time.timeScale = 0f;
    }

    /**
        Deactivates the pause panel game object, reactivates the shoot button
        and sets the timescale back to 1 so that gameplay continues.
    */
    public void ResumeGame(){
        pause_panel.SetActive(false);
        shoot_btn.raycastTarget = true;
        Time.timeScale = 1f;
    }

    /**
        Sets the timescale to 1, and loads the main menu scene.
    */
    public void ExitGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /**
        Sets the timeScale to 0, updates the final score text and activates the
        game over panel game object.
    */
    public void GameOver(){
        Time.timeScale = 0f;
        final_score.text = "Killed: "+zombie_kill_count.ToString();
        gameover_panel.SetActive(true);
    }
    
    /**
        Sets the timescale to 1, and loads the gameplay scene.
    */
    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }
} //class
