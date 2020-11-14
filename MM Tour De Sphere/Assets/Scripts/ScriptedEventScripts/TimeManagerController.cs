using UnityEngine;

public class TimeManagerController : MonoBehaviour
{
    private const float timeFactorReset = 1.0f;
    private const float defaultSlowFactor = 0.5f;
    public float timeFactor;
    public float timeFactorLength;

    // Start is called before the first frame update
    void Start()
    {
        CheckFactor();
    }

    /*
     * When slow-motion happens, increase the motion until it returns back to normal
     */

    void Update()
    {
        Time.timeScale += (1.0f / timeFactorLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);

        Time.fixedDeltaTime += (0.02f / timeFactorLength) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0.0f, 0.02f);
    }

    /*
     * Changes game speed
     */

    public void ChangeMotion()
    {
        Time.timeScale = timeFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    /*
     * If any of the following variables are null, set a default value
     * 
     */

    void CheckFactor()
    {
        if(timeFactor == 0)
        {
            this.timeFactor = defaultSlowFactor;
        }
        if(timeFactorLength == 0)
        {
            this.timeFactorLength = 2.0f;
        }
    }

    /*
     * Resets speed of the game
     */

    public void ResetFactor()
    {
        this.timeFactor = timeFactorReset;

    }

    /*
     * Sets new slow motion (how slow things should get)
     */

    public void SetSlowMotion(float newTimeFactor, float newTimeLength)
    {
        this.timeFactor = Mathf.Clamp(newTimeFactor, 0.0f, 1.0f);
        this.timeFactorLength = newTimeLength;
    }

    /*
     * If the user wants to reset the slow-motion
     */

    public void ResetSlowMotion()
    {
        this.timeFactor = timeFactorReset;
        this.timeFactorLength = defaultSlowFactor;
    }
}
