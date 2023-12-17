using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetropolisToMain : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public Animator transition;
    private PlayerSpawn playerSpawn;
    public float transitionTime = 1f;
    public float[] spawnPos = new float[] {3.51f, 7.27f};

    void Start()
    {
        sceneLoader = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoader>();
        transition = GameObject.FindWithTag("SceneLoader").transform.Find("Crossfade").GetComponent<Animator>();
        playerSpawn = GameObject.FindWithTag("Player").GetComponent<PlayerSpawn>();
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        sceneLoader.transition = transition;
        playerSpawn.UpdateSpawn(spawnPos);
        sceneLoader.LoadNextScene(0);
    }
}
