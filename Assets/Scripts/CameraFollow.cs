using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera cam;
    public float fishZoom = 5f;
    public float sharkZoom = 8f;
    public float megZoom = 12f;
    public Transform target;
    public Player player;

    void Start()
    {


    }
    
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        int stage = player.GetStage();

        if (stage == 2)
        {
            cam.orthographicSize = megZoom;
        }
        else if (stage == 1)
        {
            cam.orthographicSize = sharkZoom;
        }
        else
        {
            cam.orthographicSize = fishZoom;
        }
    }
}
