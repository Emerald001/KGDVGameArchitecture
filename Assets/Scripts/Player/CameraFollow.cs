using UnityEngine;

public class CameraFollow 
{
    private Camera playerCam;
    private GameObject objectToFollow;

    private Vector3 offset;

    public CameraFollow(Camera _playerCam, GameObject _objectToFollow)
    {
        this.playerCam = _playerCam;
        this.objectToFollow = _objectToFollow;
    }

    // Start is called before the first frame update
    public void OnEnter()
    {
        offset = new Vector3(0, 0, -10);
        playerCam.transform.position = objectToFollow.transform.position + offset;
    }

    // Update is called once per frame
    public void OnUpdate()
    {
        playerCam.transform.position = Vector3.Slerp(playerCam.transform.position, objectToFollow.transform.position + offset, 5 * Time.deltaTime);
    }
}
