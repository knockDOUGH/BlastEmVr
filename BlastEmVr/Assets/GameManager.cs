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
    private bool isComputerShooting = false;

    // Use this for initialization
    void Start ()
	{
        cardboardControl.trigger.OnDown += delegate (object sender)
        {
            if (isPlayersTurn) startPull();
        };
        cardboardControl.trigger.OnUp += delegate(object sender)
        {
            if (isPlayersTurn) stopPull();
        };
	}

    // Update is called once per frame
    void Update()
    {
        if (!isPlayersTurn && !isComputerShooting)
        {
            StartCoroutine(GenerateRandomComputerShot());
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

    private void startPull()
    {
        Debug.Log("start pull");
        this.pulling = true;
        this.power = 0;
    }

    private void stopPull()
    {
        Debug.Log("stop pull");
        this.pulling = false;
        this.ShootCannonBall(_playerCannonBallPlaceHolderTip, _playerCannonBallPlaceHolderBase, power);
        isPlayersTurn = false;
    }

    private IEnumerator GenerateRandomComputerShot()
    {
        isComputerShooting = true;
        yield return new WaitForSeconds(3);
        _compTower.transform.LookAt(_playerTower.transform);

        Vector3 rot = _compTower.transform.rotation.eulerAngles;
        float degreesVarianceY = 7;
        float degreesVarianceX = 5;
        float baselineX = 5;

        rot.y += Random.Range(-degreesVarianceY, degreesVarianceY);
        rot.x -= Random.Range(baselineX - degreesVarianceX, baselineX + degreesVarianceX);

        _compTower.transform.rotation = Quaternion.Euler(rot);

        ShootCannonBall(_compCannonBallPlaceHolderTip, _compCannonBallPlaceHolderBase, 700);
        isComputerShooting = false;
        isPlayersTurn = true;
    }

    private void ShootCannonBall(GameObject cannonBallPlaceholderTip, GameObject cannonBallPlaceHolderBase, float power)
    {
        Vector3 position = cannonBallPlaceholderTip.transform.position;
        Quaternion rotation = cannonBallPlaceholderTip.transform.rotation;

        GameObject ball = this.spawnBall(position, rotation);
        Vector3 projectionVector = ball.transform.position - cannonBallPlaceHolderBase.transform.position;
        ball.GetComponent<Rigidbody>().AddForce(projectionVector * power);

        Collider[] colliders = Physics.OverlapSphere(position, 20);

        foreach (Collider c in colliders)
        {
            if (c.attachedRigidbody == null)
            {
                continue;
            }
            //c.attachedRigidbody.AddExplosionForce(2, cannonBallPlaceHolderBase.transform.position, 10, 1, ForceMode.Impulse);
        }
    }


    private GameObject spawnBall(Vector3 position, Quaternion rotation)
    {
        GameObject g = (GameObject)Instantiate(_cannonBall, position, rotation);
        return g;
    }
}
