using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float min_Y = -4.3f, max_Y = 4.3f;
   // public float interval = 100;
   // private float counter = 0;

    public GameObject[] enemy_turretship_small;

   
    [SerializeField] private float interval = 2f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
       Invoke("SpawnEnemies", timer);
    }

   void SpawnEnemies()
    {
        float pos_Y = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.y = pos_Y;

     // Instantiate(enemy_turretship_small, temp, Quaternion.identity);

        Invoke("SpawnEnemies", timer);
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            Instantiate(enemy_turretship_small, transform.position, transform.rotation);
        }
    }
    /*  void FixedUpdate()
      {
          counter += 1;

          if (counter >= interval)
          {
              counter = 0;
              Instantiate(enemy_turretship_small, transform.position, transform.rotation);

          }
      } */


} // class
