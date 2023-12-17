using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainToMetropolis : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public Animator transition;
    private PlayerSpawn playerSpawn;
    public float transitionTime = 1f;
    public float[] spawnPos = new float[] {1.53f, -3.9f};

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
        sceneLoader.LoadNextScene(1);
    }
}
