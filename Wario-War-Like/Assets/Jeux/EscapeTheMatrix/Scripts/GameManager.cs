using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int number_player;
    public GameObject WaypointPlayer;  
    public GameObject prefab_player;
    public GameObject prefab_bullet;
    public float gravity;
    public List<GameObject> CreationPoints;
    public List<UnityEngine.Color> colors = new List<UnityEngine.Color>{Color.red, Color.blue, Color.green, Color.yellow};
    public float refresh_bullet;
    private int nombrepoints;
    private GameObject clone;
    private bool CreateBullet;

    public List<int> scores;

    private List<string> res = new List<string>(); 
    void Start(){
        CreateBullet = true; 
        for(int i = 0; i < number_player; i++){
            clone = Instantiate(prefab_player, WaypointPlayer.transform.position, Quaternion.identity);
            clone.GetComponent<SpriteRenderer>().color = colors[i];
            clone.name = "Player" + (i+1).ToString();
            clone.layer = LayerMask.NameToLayer("Player" + (i+1).ToString());
        }
        nombrepoints = CreationPoints.Count; 
        StartCoroutine(Create_bullet());
    }

    IEnumerator Create_bullet(){
        while (CreateBullet){
            yield return new WaitForSeconds(refresh_bullet);
            int indexApparitionPoint = Random.Range(0,nombrepoints);
            Instantiate(prefab_bullet, CreationPoints[indexApparitionPoint].transform.position, Quaternion.identity);
        } 
        yield return null;
    } 

    public void InfoDeath(string id){
        res.Add(id); 
        if(res.Count >= number_player){
            res.Reverse(); 
            CreateBullet = false;
            resultats(); 
        }
    }
    public void resultats(){
        for(int i = 0; i < number_player; i++){
            for(int j = 0; j < 4-number_player; j++){            
                scores[i] = scores[i] + scores[j+number_player]/number_player; 
            }
        }
        int totalapprox = 0; 
        for(int i = 0; i<number_player; i++){
            totalapprox = totalapprox+scores[i]; 
        }
        Debug.Log(totalapprox);
        if(totalapprox < 100){
            scores[0] = scores[0] + 100-totalapprox;
        }
        for(int i = 0; i < number_player; i++){
            Debug.Log(res[i] + "a gagné : " + scores[i] + " points.");
        }
    }
}
