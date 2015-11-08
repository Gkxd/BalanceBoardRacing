using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AccelerometerCalibration : MonoBehaviour
{

    //private static List<Vector3> forwardSamples, backwardSamples, leftSamples, rightSamples, restingSamples;
    private static VectorStats forward, backward, left, right, rest;

    //calculates the natural logarithm of the probability of x given stdDev and mean for a normal distribution
    private static float probNormalDist(float x, float stdDev, float mean)
    {
        float coefficient = 1 / (stdDev * Mathf.Sqrt((2 * Mathf.PI)));
        float exponent = (Mathf.Pow((x - mean), 2)) / (-2 * Mathf.Pow(stdDev, 2));
        return (Mathf.Log(coefficient) + exponent);
    }

    public static float horizontalAxis
    {
        get
        {
            Vector3 acceleration = OSCManager.accelerationVector;
            //Debug.Log(acceleration);
            float x = acceleration.x;
            float y = acceleration.y;
            float z = acceleration.z;

            Vector3 lstdDev = left.stdDev;
            Vector3 lmean = left.average;
            Vector3 rstdDev = right.stdDev;
            Vector3 rmean = right.average;
            Vector3 reststdDev = rest.stdDev;
            Vector3 restmean = rest.average;

            float probLeft = probNormalDist(x, lstdDev.x, lmean.x) +
                probNormalDist(y, lstdDev.y, lmean.y) +
                probNormalDist(z, lstdDev.z, lmean.z);


            float probRight = probNormalDist(x, rstdDev.x, rmean.x) +
                probNormalDist(y, rstdDev.y, rmean.y) +
                probNormalDist(z, rstdDev.z, rmean.z);

            float probRest = probNormalDist(x, reststdDev.x, restmean.x) +
                probNormalDist(y, reststdDev.y, restmean.y) +
                probNormalDist(z, reststdDev.z, restmean.z);

            Debug.Log("probLeft: " + Mathf.Log10(-probLeft) + " probRight: " + Mathf.Log10(-probRight));

            if (probLeft >= probRest && probLeft >= probRight)
            {
                return -1;
            }
            else if (probRight > probLeft && probRight >= probRest)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public static float verticalAxis
    {
        get
        {
            Vector3 acceleration = OSCManager.accelerationVector;
            //Debug.Log(acceleration);
            float x = acceleration.x;
            float y = acceleration.y;
            float z = acceleration.z;

            Vector3 fstdDev = forward.stdDev;
            Vector3 fmean = forward.average;
            Vector3 bstdDev = backward.stdDev;
            Vector3 bmean = backward.average;
            Vector3 reststdDev = rest.stdDev;
            Vector3 restmean = rest.average;

            float probForward = probNormalDist(x, fstdDev.x, fmean.x) +
                probNormalDist(y, fstdDev.y, fmean.y) +
                probNormalDist(z, fstdDev.z, fmean.z);

            float probBackward = probNormalDist(x, bstdDev.x, bmean.x) +
                probNormalDist(y, bstdDev.y, bmean.y) +
                probNormalDist(z, bstdDev.z, bmean.z);

            float probRest = probNormalDist(x, reststdDev.x, restmean.x) +
                probNormalDist(y, reststdDev.y, restmean.y) +
                probNormalDist(z, reststdDev.z, restmean.z);

            Debug.Log("probForward: " + Mathf.Log10(-probForward) + " probBackward: " + Mathf.Log10(-probBackward));

            if (probBackward >= probRest && probBackward >= probForward)
            {
                return -1;
            }
            else if (probForward > probBackward && probForward >= probRest)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    void Awake()
    {
        resetSamples();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            left.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            right.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            forward.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            backward.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            rest.add(OSCManager.accelerationVector);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            string hor = "NONE";
            string ver = "NONE";

            if (horizontalAxis == 1) 
            {
                hor = "RIGHT";
            }
            else if (horizontalAxis == -1)
            {
                hor = "LEFT";
            }
            if (verticalAxis == 1)
            {
                ver = "FORWARD";
            }
            else if (verticalAxis == -1)
            {
                ver = "BACKWARD";
            }
            Debug.Log(hor + " " + ver);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            resetSamples();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            forward.save("forward");
            backward.save("backward");
            left.save("left");
            right.save("right");
            rest.save("rest");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            forward.load("forward");
            backward.load("backward");
            left.load("left");
            right.load("right");
            rest.load("rest");
        }

        Debug.DrawRay(Vector3.zero, left.average, Color.red);
        Debug.DrawRay(Vector3.zero, right.average, Color.blue);
        Debug.DrawRay(Vector3.zero, forward.average, Color.green);
        Debug.DrawRay(Vector3.zero, backward.average, Color.yellow);
        Debug.DrawRay(Vector3.zero, rest.average, Color.white);
    }

    private void resetSamples()
    {
        forward.reset();
        backward.reset();
        left.reset();
        right.reset();
        rest.reset();
    }



    private struct VectorStats
    {
        public Vector3 average { get; private set; }
        private Vector3 variance { get; set; }
        public Vector3 stdDev { get; private set; }
        public int count { get; private set; }

        public void reset()
        {
            average = Vector3.zero;
            variance = Vector3.zero;
            stdDev = Vector3.zero;
            count = 0;
        }

        public void save(string name)
        {
            PlayerPrefs.SetFloat(name + "avgx", average.x);
            PlayerPrefs.SetFloat(name + "avgy", average.y);
            PlayerPrefs.SetFloat(name + "avgz", average.z);

            PlayerPrefs.SetFloat(name + "sdx", stdDev.x);
            PlayerPrefs.SetFloat(name + "sdy", stdDev.y);
            PlayerPrefs.SetFloat(name + "sdz", stdDev.z);

            PlayerPrefs.Save();
        }

        public void load(string name)
        {
            Vector3 avg = new Vector3(PlayerPrefs.GetFloat(name + "avgx"),
                PlayerPrefs.GetFloat(name + "avgy"),
                PlayerPrefs.GetFloat(name + "avgz"));

            average = avg;

            Vector3 sd = new Vector3(PlayerPrefs.GetFloat(name + "sdx"),
                PlayerPrefs.GetFloat(name + "sdy"),
                PlayerPrefs.GetFloat(name + "sdz"));

            stdDev = sd;
        }

        public void add(Vector3 vector)
        {
            Vector3 newTotal = average * count++ + vector;
            Vector3 newAverage = newTotal / count;

            //for more information on this formula, consult http://math.stackexchange.com/q/775678
            if (count > 1) {
                variance = ((count - 2) * variance + Vector3.Scale((vector - newAverage), (vector - average))) / (count - 1);
            }

            stdDev = new Vector3(Mathf.Sqrt(variance.x), Mathf.Sqrt(variance.y), Mathf.Sqrt(variance.z));

            average = newAverage;
        }
    }
}
