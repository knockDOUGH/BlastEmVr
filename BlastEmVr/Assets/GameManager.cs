using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _cannonBall;
    public GameObject _playerCannonBallPlaceHolderTip;
    public GameObject _playerCannonBallPlaceHolderBase;
    public GameObject _playerTower;

    public GameObject _compCannonBallPlaceHolderTip;
    public GameObject _compCannonBallPlaceHolderBase;
    public GameObject _compTower;

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
            StartCoroutine(GenerateRandomComputerShot());
            isPlayersTurn = true;
        }

        if (isPlayersTurn && pulling && power < maxPower)
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
        Debug.Log("start pull");
        this.pulling = true;
        this.power = 0;
    }

    private void stopPull(object sender)
    {
        Debug.Log("stop pull");
        this.pulling = false;
        this.ShootCannonBall(_playerCannonBallPlaceHolderTip, _playerCannonBallPlaceHolderBase);
        isPlayersTurn = false;
    }

    private IEnumerator GenerateRandomComputerShot()
    {
        yield return new WaitForSeconds(0);

        _compTower.transform.LookAt(_playerTower.transform);

        Vector3 rot = _compTower.transform.rotation.eulerAngles;
        float degreesVarianceY = 10;
        float degreesVarianceX = 10;

        rot.y += Random.Range(-degreesVarianceY, degreesVarianceY);
        rot.x += Random.Range(-degreesVarianceX, degreesVarianceX);

        _compTower.transform.rotation = Quaternion.Euler(rot);

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
