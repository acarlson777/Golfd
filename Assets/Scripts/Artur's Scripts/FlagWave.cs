using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlagWave : MonoBehaviour
{
    public enum Axes
    {
        X,Y,Z
            
    }
    private float getAxisOfVector(Vector3 v, Axes a)
    {
        switch(a)
        {
            case Axes.X:
                return v.x;
            case Axes.Y:
                return v.y;
            case Axes.Z:
                return v.z;
            default:
                return 0;
        }

    }
    private void setAxisOfVector(ref Vector3 v, Axes a, float b)
    {
        switch (a)
        {
            case Axes.X:
                v.x = b;
                break;
            case Axes.Y:
                v.y = b;
                break;
            case Axes.Z:
                v.z = b;
                break;
        }
    }

    public Axes axis;
    [Range(-180, 180)]
    public float range;

    [Range(0,2)]
    public float speed = 1;

    public bool randomStart;
    private float startingRot;
#if UNITY_EDITOR
    private float time;
#else
    private float startTime;
#endif

    // Start is called before the first frame update
    void Start()
    {
        startingRot = getAxisOfVector(transform.localEulerAngles,axis);
#if UNITY_EDITOR
        if (randomStart)
            time = Random.Range(0, speed * 2 * Mathf.PI);
#else
        if (randomStart) 
            startTime = Random.Range(0, speed * 2 * Mathf.PI);

#endif
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.localEulerAngles;

#if UNITY_EDITOR
        time += speed * Time.deltaTime;
        float cur = Mathf.Sin(time);
#else
        float cur = Mathf.Sin(Time.time*speed + startTime);
#endif
        cur *= cur;
        setAxisOfVector(ref rot, axis, startingRot + cur * range);
        transform.localEulerAngles = rot;
    }
}