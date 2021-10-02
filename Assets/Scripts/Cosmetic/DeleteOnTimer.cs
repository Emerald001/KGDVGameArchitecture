using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnTimer : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    public IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
