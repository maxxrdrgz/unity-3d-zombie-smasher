using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public GameObject[] obstaclePrefabs, zombiePrefabs;
    public Transform[] lanes;
    public float min_obstacle_delay = 10f, max_obstacle_delay = 40f;

    private float half_ground_size;
    private BaseController playerController;

    private void Awake() {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        half_ground_size = GameObject
            .Find("GroundBlock Main")
            .GetComponent<GroundBlock>().halfLength;

        playerController = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<BaseController>();

        StartCoroutine("GenerateObstacles");
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
            max_obstacle_delay/playerController.speed.z
        );
        yield return new WaitForSeconds(timer);
        CreateObstacles(
            playerController.gameObject.transform.position.z + half_ground_size
        );
        StartCoroutine("GenerateObstacles");
    }

    void CreateObstacles(float zPos){
        int r = Random.Range(0, 10);
        if(0 <= r && r < 7){
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
                new Vector3(lanes[obstacleLane].transform.position.x, 0.15f, zPos)
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
} //class
