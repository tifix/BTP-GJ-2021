using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Transform enemy_container;
    public Transform UnstableTemporaries;

    public GameObject boss_panel;
    public Slider boss_bar;
    public Health h;
    public TrackObject track;
    public SpawnProjectiles SP;
    public int num_of_projectiles_spawned;

    public void Awake()
    {
        if (enemy_container == null) enemy_container = GameObject.Find("Enemies").transform;
        if (UnstableTemporaries == null) UnstableTemporaries = GameObject.Find("UnstableTemporaries").transform;
        if (SP == null) SP = GetComponent<SpawnProjectiles>();

        h = GetComponent<Health>();
        track = GetComponent<TrackObject>();
        track.target_spotted.AddListener(OnBossEngaged);
        SP.bullet_death.AddListener(WarpStarter);
    }
    public void OnBossEngaged()
    {
        boss_panel.SetActive(true);
        StartCoroutine(UpdateBossHealthBar());

        foreach (Transform child in Player.instance.healthbar.GetComponentsInChildren<Transform>())
        {
            if (child.name == "Fill")
            {
                child.GetComponent<Image>().color = new Color(1, 0.843f,0);
            }
        }
    }

    private void OnDestroy()
    {
        foreach(Transform t in enemy_container.GetComponentsInChildren<Transform>())
        {
            bool all_bosses_defeated = true;
            if(t.TryGetComponent<Boss>(out Boss boss))
            {
                if (boss != this) all_bosses_defeated = true;
            }
            if (all_bosses_defeated) CamEffects.instance.Victory();
        }
    }


    public List<Ray2D> ShuffleTrajectiories(int amount)
    {
        List<Ray2D> ray2Ds = new List<Ray2D>();
        for (int i = 0; i < amount; i++)
        {
            Vector2 origin = transform.position;
            
            //Absolutelly Randomized trajectories
            Vector2 direction =( Vector2.right * Random.Range(-1, 1) + Vector2.up * Random.Range(-1, 1)).normalized;

            ray2Ds.Add( new Ray2D(origin, direction));
        }


        return ray2Ds;
    }

    IEnumerator UpdateBossHealthBar()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (h.HP > h.max_HP * 0.8f) num_of_projectiles_spawned = 2;
            else if (h.HP > h.max_HP * 0.6f) num_of_projectiles_spawned = 3;
            else if (h.HP > h.max_HP * 0.3f) num_of_projectiles_spawned = 4;
            else num_of_projectiles_spawned = 5;

            if(Time.time-SP.time_at_last_burst>SP.firing_interval) SP.projectile_trajectories=ShuffleTrajectiories(num_of_projectiles_spawned); //Now this one, I am proud of.
            boss_bar.maxValue = h.max_HP;
            boss_bar.value = h.HP;
        }

    }

    public void WarpStarter()
    {
        StartCoroutine(WarpToProjectiles(3));
    }

    public IEnumerator  WarpToProjectiles(float delay)
    {
        yield return new WaitForSeconds(delay);
        int randomizer = Random.Range(0, UnstableTemporaries.childCount);
        transform.position = UnstableTemporaries.GetChild(randomizer).position;
        Destroy(UnstableTemporaries.GetChild(randomizer).gameObject);

        for (int i = 0; i < UnstableTemporaries.childCount; i++)
        {
            Destroy(UnstableTemporaries.GetChild(i).gameObject);
        }

    }

}
