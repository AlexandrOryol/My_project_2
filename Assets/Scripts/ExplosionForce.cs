using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
    [SerializeField] private CubeSettings _settings;

    public void ApplyExplosion(GameObject[] cubes, Vector3 explosionCenter)
    {
        if (_settings == null)
            return;

        foreach (GameObject cube in cubes)
        {
            Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_settings.ExplosionForce, explosionCenter, _settings.ExplosionRadius, 0f, _settings.ExplosionForceMode);
            }
        }
    }
}