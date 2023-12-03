using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public float TimePassed { get; private set; }

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Debug.LogError("Duplicate GameManager instance detected. Destroying the new instance.");
            Destroy(gameObject);
            return;
        }
        gameManager = this;
        DontDestroyOnLoad(gameObject);
        TimePassed = 0;
    }

    void UpdateTime()
    {
        TimePassed += Time.deltaTime;
    }

    public string TimeInFormat()
    {
        int minutes = Mathf.FloorToInt(TimePassed / 60.0f);
        int seconds = Mathf.FloorToInt(TimePassed % 60.0f);

        return $"{minutes:00}:{seconds:00}";
    }


    //public string TimeInFormat()
    //{
    //    if (TimePassed / 60.0f < 10)
    //    {
    //        return $"Time: 00:0{TimePassed / 60.0f}";
    //    }
    //    else if (TimePassed / 60.0f < 60)
    //    {
    //        return $"Time: 00:{TimePassed / 60.0f}";
    //    }
    //    else if (TimePassed / 3600.0f < 10)
    //    {
    //        if (TimePassed / 60.0f < 10)
    //            return $"Time: 0{TimePassed / 3600.0f}:0{TimePassed / 60.0f}";
    //        else if (TimePassed / 60.0f < 60)
    //            return $"Time: 0{TimePassed / 3600.0f}:{TimePassed / 60.0f}";
    //    }
    //    else if(TimePassed/3600.0f < 60)
    //    {
    //        if (TimePassed / 60.0f < 10)
    //            return $"Time: {TimePassed / 3600.0f}:0{TimePassed / 60.0f}";
    //    }
    //    return $"Time: {TimePassed / 3600.0f}:{TimePassed / 60.0f}";
    //}

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    private void OnDestroy()
    {
        gameManager = null;
    }
}
