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

    // Start is called before the first frame update
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

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

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

    public void IncreaseScore(){
        zombie_kill_count++;
        score_text.text = zombie_kill_count.ToString();
    }

    public void PauseGame(){
        pause_panel.SetActive(true);
        shoot_btn.raycastTarget = false;
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        pause_panel.SetActive(false);
        shoot_btn.raycastTarget = true;
        Time.timeScale = 1f;
    }

    public void ExitGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver(){
        Time.timeScale = 0f;
        final_score.text = "Killed: "+zombie_kill_count.ToString();
        gameover_panel.SetActive(true);
    }

    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }
} //class
