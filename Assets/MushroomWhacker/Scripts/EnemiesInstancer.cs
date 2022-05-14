using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesProbability {
    public EnemyScriptable enemy;
    [Range(0,1)]
    public float probability;
}
public class EnemiesInstancer : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<EnemiesProbability> _enemies; 
    [SerializeField] float _timeToSpawn = 1;
    public List<int> _holesWithEnemies;

    void Start(){
        _holesWithEnemies = new List<int>();
        CalculateProbability();
    }

    void OnEnable() {
        GameManager.instance.onChangeState += OnChangeGameState;        
    }

    void OnDisable() {
        GameManager.instance.onChangeState -= OnChangeGameState;        
    }

    void OnChangeGameState(GameState state){
        switch (state){
            case GameState.NotStarted:
                StopAllCoroutines();
            break;
            case GameState.InGame:
                StartCoroutine(InstanceEnemies());
            break;
            case GameState.GameOver:
                StopAllCoroutines();
            break;
            default:
                break;
        }
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
        EnemyScriptable enemyFeatures = _enemies[0].enemy;
        yield return new WaitForSeconds(2f);
        while (true)
        {
            int selectedHole = Random.Range(0, transform.childCount);
            if(!_holesWithEnemies.Contains(selectedHole))
            {
                _holesWithEnemies.Add(selectedHole);
                float rnd = (float)Random.Range(0, 100)/100;
                for (int i = 0; i < _enemies.Count; i++){
                    if (rnd <= _enemies[i].probability) {
                        enemyFeatures = _enemies[i].enemy;                    
                        break;
                    }                   
                }
                Enemy x = GameObject.Instantiate(enemyPrefab, transform.GetChild(selectedHole).GetChild(0).position, Quaternion.identity).GetComponent<Enemy>();
                x.SetInitValues(this, selectedHole, enemyFeatures);
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
