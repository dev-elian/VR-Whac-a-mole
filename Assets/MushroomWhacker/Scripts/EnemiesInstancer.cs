using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesProbability {
    public GameObject enemy;
    [Range(0,1)]
    public float probability;
}
public class EnemiesInstancer : MonoBehaviour
{
    [SerializeField] List<EnemiesProbability> _enemies; 
    [SerializeField] float _timeToSpawn = 1;
    public List<int> _holesWithEnemies;

    void Start(){
        _holesWithEnemies = new List<int>();
        CalculateProbability();
        StartCoroutine(InstanceEnemies());
    }
    void CalculateProbability(){
        _enemies.Sort((x, y) => x.probability.CompareTo(y.probability));
        float total=0;
        foreach (EnemiesProbability enemy in _enemies){
            total += enemy.probability;
        }
        float last=0;
        foreach (EnemiesProbability enemy in _enemies){
            enemy.probability = (enemy.probability / total)+last;
            last=enemy.probability;
        }
    }

    IEnumerator InstanceEnemies(){
        GameObject enemyToInstance = _enemies[0].enemy;
        while (true)
        {
            int selectedHole = Random.Range(0, transform.childCount);
            if(!_holesWithEnemies.Contains(selectedHole))
            {
                _holesWithEnemies.Add(selectedHole);
                float rnd = (float)Random.Range(0, 100)/100;
                for (int i = 0; i < _enemies.Count; i++){
                    Debug.Log(_enemies[i].probability);
                    if (rnd <= _enemies[i].probability) {
                        enemyToInstance = _enemies[i].enemy;                    
                        break;
                    }                   
                }
                Enemy x = GameObject.Instantiate(enemyToInstance, transform.GetChild(selectedHole).GetChild(0).position, Quaternion.identity).GetComponent<Enemy>();
                x.instancer = this;
                x.hole = selectedHole;
                yield return new WaitForSeconds(_timeToSpawn);
            }
            else
                yield return new WaitForSeconds(0.1f);
        }
    }
    public void RemoveEnemy(int idxHole){
        _holesWithEnemies.Remove(idxHole);
    }
    
}
