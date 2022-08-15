using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WareConfig> wareConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] float WaitAndLoadEnemy = 3f;
    //am thanh
    [SerializeField] AudioClip BossSound;
    [SerializeField][Range(0, 1)] float BossSoubdVoluem = 1f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(WaitAndLoadEnemy);
        do
        {
            yield return StartCoroutine(SpawnAllWave());
        }
        while (looping);

    }

    private IEnumerator SpawnAllWave()
    {
        //for (int waveIndex  = startingWave; waveIndex < wareConfigs.Count; waveIndex++)
        for (int waveIndex = startingWave; waveIndex < 7; waveIndex++)
        {
            var currentWave = wareConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));  
        }
            yield return new WaitForSeconds(3);
            FindObjectOfType<MusicPlayer>().TurnOffSound();
            //FindObjectOfType<VideoPlayer>().StartVideo();
        AudioSource.PlayClipAtPoint(BossSound, Camera.main.transform.position, BossSoubdVoluem);
            yield return new WaitForSeconds(3);
        for (int waveIndex = 7; waveIndex < 8; waveIndex++)
        {
            var currentWave = wareConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }

    }
   
    private IEnumerator SpawnAllEnemiesInWave(WareConfig wareConfig)
    {
        for ( int enemyCount = 0; enemyCount < wareConfig.GetNumberOfEnemy(); enemyCount++)
        {
            var newEnemy = Instantiate(
                wareConfig.GetEnemyPrefab(),
                wareConfig.GetWayPoints()[1].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wareConfig);
            yield return new WaitForSeconds(wareConfig.GetTimeBetweenSpawns());
        }
        
    }
    
}
