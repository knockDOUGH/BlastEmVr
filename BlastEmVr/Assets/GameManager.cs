using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _cannonBall;
    public GameObject _cannonBallPlaceholder;

    public float maxPower;
    public float powerIncreaseRate;
    public GameObject _playerTankBody;
    public CardboardControl cardboardControl;

    private bool pulling = false;
    private float power = 0;

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
        this.ShootCannonBall();
    }

    private void ShootCannonBall()
    {
        Vector3 position = _cannonBallPlaceholder.transform.position;
        Quaternion rotation = _cannonBallPlaceholder.transform.rotation;

        GameObject ball = this.spawnBall(position, rotation);
        Vector3 projectionVector = ball.transform.position - _playerTankBody.transform.position;
        ball.GetComponent<Rigidbody>().AddForce(projectionVector * power);
    }


    private GameObject spawnBall(Vector3 position, Quaternion rotation)
    {
        // spawn rocket
        // - Instantiate means 'throw the prefab into the game world'
        // - (GameObject) cast is required because unity is stupid
        // - transform.position because we want to instantiate it exactly
        //   where the weapon is
        // - transform.parent.rotation because the rocket should face the
        //   same direction that the player faces (which is the weapon's
        //   parent.
        //   we can't just use the weapon's rotation because the weapon is
        //   always rotated like 45° which would make the bullet fly really
        //   weird
        GameObject g = (GameObject)Instantiate(_cannonBall, position, rotation);
        return g;
    }
}
