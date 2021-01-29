using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnProjectiles : MonoBehaviour
{
    public List<Ray2D> projectile_trajectories = new List<Ray2D>();
    public GameObject projectile_prefab;
    public GameObject attack_telegraph;
    public float firing_interval = 5;
    public float time_at_last_burst;
    private float init_accelaration=4;
    public float telegraph_length = 2;
    public UnityEvent bullet_fired;
    private bool fire_from_transpos=true;

    void Start()
    {
        StartCoroutine(ShootingCycle());   
    }

    // Update is called once per frame
    private void Telegraph(Ray2D trajectory)
    {
        if (fire_from_transpos) trajectory = new Ray2D(transform.position, trajectory.direction);
        GameObject telegraph = Instantiate(attack_telegraph, trajectory.origin, Quaternion.FromToRotation(Vector3.forward,(Vector3)trajectory.direction));
        Debug.DrawRay(trajectory.origin,trajectory.direction,Color.red, telegraph_length);

        //Increase the duration to telegraph length
        ParticleSystem.MainModule main = telegraph.GetComponent<ParticleSystem>().main;
        main.duration = telegraph_length;

        //rotate and destroy
       // telegraph.transform.up = trajectory.direction;
        Destroy(telegraph, telegraph_length);
    }
    public void Fire(Ray2D trajectory)
    {
        time_at_last_burst = Time.time;
        GameObject bullet;
        if(!fire_from_transpos)bullet=Instantiate(projectile_prefab, trajectory.origin, Quaternion.identity);
        else bullet=Instantiate(projectile_prefab, transform.position, Quaternion.identity);

        if (bullet.TryGetComponent<Charger>(out Charger c)) { c.Invoke("EnableDash", 1.5f); }   //Invoke(c.EnableDash, 2); }

        bullet.transform.SetParent(GetComponent<Boss>().UnstableTemporaries);
        bullet.GetComponent<Rigidbody2D>().AddForce(trajectory.direction* init_accelaration, ForceMode2D.Impulse);
        Destroy(bullet, firing_interval);
        
    }

    public IEnumerator TelegraphAndFire(Ray2D trajectory )
    {
        Telegraph(trajectory);
        yield return new WaitForSeconds(telegraph_length);
        Fire(trajectory);
    }

    public IEnumerator ShootingCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(firing_interval);
            for (int i = 0; i < projectile_trajectories.Count; i++)
            {
                bullet_fired.Invoke();
                StartCoroutine(TelegraphAndFire(projectile_trajectories[i]));
            }

        }
    }
}
