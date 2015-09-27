using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _cannonBall;
    public GameObject _playerCannonBallPlaceHolderTip;
    public GameObject _playerCannonBallPlaceHolderBase;

    public GameObject _compCannonBallPlaceHolderTip;
    public GameObject _compCannonBallPlaceHolderBase;

    public float maxPower;
    public float powerIncreaseRate;
    public CardboardControl cardboardControl;

    private bool pulling = false;
    private float power = 0;
    private bool isPlayersTurn = true;

    // Use this for initialization
    void Start ()
	{
        cardboardControl.trigger.OnDown += startPull;
        cardboardControl.trigger.OnUp += stopPull;
	}

    void OnDestroy()
    {
        cardboardControl.trigger.OnDown -= startPull;
        cardboardControl.trigger.OnUp -= stopPull;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayersTurn)
        {
            GenerateRandomComputerShot();
            isPlayersTurn = true;
        }

        if (pulling && power < maxPower)
        {
            power += powerIncreaseRate;
            if (power > maxPower) power = maxPower;
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 50), this.power.ToString());
    }

    private void startPull(object sender)
    {
        this.pulling = true;
        this.power = 0;
    }

    private void stopPull(object sender)
    {
        this.pulling = false;
        this.ShootCannonBall(_playerCannonBallPlaceHolderTip, _playerCannonBallPlaceHolderBase);
        isPlayersTurn = false;
    }

    private void GenerateRandomComputerShot()
    {
        ShootCannonBall(_compCannonBallPlaceHolderTip, _compCannonBallPlaceHolderBase);
    }

    private void ShootCannonBall(GameObject cannonBallPlaceholder, GameObject tankTower)
    {
        Vector3 position = cannonBallPlaceholder.transform.position;
        Quaternion rotation = cannonBallPlaceholder.transform.rotation;

        GameObject ball = this.spawnBall(position, rotation);
        Vector3 projectionVector = ball.transform.position - tankTower.transform.position;
        ball.GetComponent<Rigidbody>().AddForce(projectionVector * power);
    }


    private GameObject spawnBall(Vector3 position, Quaternion rotation)
    {
        GameObject g = (GameObject)Instantiate(_cannonBall, position, rotation);
        return g;
    }
}
