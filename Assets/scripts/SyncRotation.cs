using UnityEngine;

public class SyncRotation : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        float cameraY = Camera.main.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, cameraY, 0);
    }
}
