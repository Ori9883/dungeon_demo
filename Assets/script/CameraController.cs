using ProceduralLevelGenerator.Unity.Examples.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float ZoomOutSize;

    private GameObject player;
    private Vector2 position;
    private bool getOrNot;

    private new Camera camera;
    private bool isZoomedOut;
    private float previousOrthographicSize;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        getOrNot = false;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            previousOrthographicSize = camera.orthographicSize;
            camera.orthographicSize = ZoomOutSize;

            previousPosition = transform.position;
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            isZoomedOut = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            camera.orthographicSize = previousOrthographicSize;
            transform.position = previousPosition;
            isZoomedOut = false;
        }
    }

    public void LateUpdate()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if(player != null && !isZoomedOut)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    public void FollowPlayer(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
