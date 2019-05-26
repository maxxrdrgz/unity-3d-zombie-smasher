using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    private Slider health_slider;
    private GameObject UI_Holder;

    // Start is called before the first frame update
    void Start()
    {
        health_slider = GameObject.Find("Health Bar").GetComponent<Slider>();
        health_slider.value = health;

        UI_Holder = GameObject.Find("UI Holder");
    }

    public void ApplyDamage(int damageAmount){
        health -= damageAmount;
        if(health < 0){
            health = 0;
        }
        health_slider.value = health;
        if(health == 0){
            UI_Holder.SetActive(false);
            GameplayController.instance.GameOver();
        }
    }
}
