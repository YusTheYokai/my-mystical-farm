using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    [SerializeField] private ItemClass item;
    bool isPlanted = false;
    public SpriteRenderer plant;

    public Sprite[] plantStages;
    int plantStage = 0;
    float timeBtwStages = 5f;
    float timer;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(isPlanted){
            timer -= Time.deltaTime;
            if (timer < 0 && plantStage<plantStages.Length-1){
                timer = timeBtwStages;
                plantStage ++;
                UpdatePlant();
            }
        }
    }
    

    private void OnMouseDown(){

        if (isPlanted){
            if (plantStage == plantStages.Length-1)
            //harvest
            Harvest();
        }
        else{
            Plant();
        }

    }

    void Harvest(){
        isPlanted = false;
        plant.gameObject.SetActive(false);
        
        InventoryManager.Instance.Add(item, 3);
    }

    void Plant(){
        isPlanted = true;
        plantStage = 0;
        UpdatePlant();
        timer = timeBtwStages;
        plant.gameObject.SetActive(true);
    }

    void UpdatePlant(){
        plant.sprite = plantStages[plantStage];
    }
}
