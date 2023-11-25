using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class CutHandlerScript : MonoBehaviour
{
    public int numOfFruitsTotal;
    public int numOfFruitsCut;
    public GameObject CutlinesPrefab;
    private GameObject _CurrentCutLinesObject;
    private bool _isSliced;
    public int totalFruitscut = 0;

    public int numOfCutLinesTotal;
    public int numOfCutLinesCut;
    public float waitTime;

    public List<GameObject> fruits;
    private GameObject _FruitObject;
    private float r;
    public float rotationSpeed = 0.1f;

    float timeElapsed = 0;
    public float duration = 3;
    private Vector3 _StartPos = new Vector3(-16, 0 , 0);
    private Vector3 _EndPos = new Vector3(16, 0, 0);

    public enum FruitStates
    {
        MOVINGIN,
        CHILLING,
        ROTATING,
        MOVINGOUT
    }

    public FruitStates _FruitState;

    private BoilScoreHandler boilScoreHandler;


    // Start is called before the first frame update
    void Start()
    {
        boilScoreHandler = GameObject.Find("ScoreHandler").GetComponent<BoilScoreHandler>();
        totalFruitscut = 0;
        _isSliced = false;
        _FruitState = FruitStates.MOVINGIN;
        numOfFruitsCut = 0;
        SpawnFruit();
        StartCoroutine(InstantiateNewCutlinePackage());
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfCutLinesCut == numOfCutLinesTotal)
        {
            if( _isSliced)
            { // har hackat frukt två gånger spawnar helt ny frukt
                totalFruitscut++;
                _FruitState = FruitStates.MOVINGOUT;
            } 
            else
            { // har hackat en gång, roterar frukten
                numOfCutLinesCut = 0;
                _FruitState = FruitStates.ROTATING;
                Debug.Log("called rotating");
                Destroy(_CurrentCutLinesObject);
                StartCoroutine(InstantiateNewCutlinePackage());
                _isSliced = true;
            }
        }

        switch (_FruitState)
        {
            case FruitStates.MOVINGIN:
                if(_FruitObject != null)
                {
                    float t = timeElapsed / duration;
                    _FruitObject.transform.position = Vector3.Lerp(_StartPos, Vector3.zero, t);
                    timeElapsed += Time.deltaTime;
                    if (_FruitObject.transform.position == Vector3.zero)
                    {
                        Debug.Log("deez nuts");
                        _FruitState = FruitStates.CHILLING;

                        timeElapsed = 0;
                    }
                }
                break;
            case FruitStates.CHILLING:
                break;
            case FruitStates.ROTATING:
                float angle = Mathf.SmoothDampAngle(_FruitObject.transform.eulerAngles.z, 90, ref r, rotationSpeed);
                _FruitObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                if(_FruitObject.transform.rotation == Quaternion.Euler(0, 0, 90))
                {
                    _FruitState = FruitStates.CHILLING;
                    r = 0;
                }
                break;
            case FruitStates.MOVINGOUT:
                float uuuh = timeElapsed / duration;
                _FruitObject.transform.position = Vector3.Lerp(Vector3.zero, _EndPos, uuuh);
                timeElapsed += Time.deltaTime;
                if (_FruitObject.transform.position == _EndPos)
                {
                    Destroy(_FruitObject);
                    _isSliced = false;
                    _FruitState = FruitStates.CHILLING;
                    timeElapsed = 0;
                    SpawnFruit();
                    StartCoroutine(InstantiateNewCutlinePackage());
                }
                break;
        }
    }

    public void EndGame()
    {
        Debug.Log("game ended");
    }

    private IEnumerator InstantiateNewCutlinePackage()
    {
        numOfCutLinesTotal = 3;
        numOfCutLinesCut = 0;
        yield return new WaitForSeconds(waitTime);
        _CurrentCutLinesObject = Instantiate(CutlinesPrefab, Vector3.zero, Quaternion.identity);
        
    }

    private void SpawnFruit()
    {

        numOfCutLinesTotal = 3;
        numOfCutLinesCut = 0;
        _FruitState = FruitStates.MOVINGIN;
        if (_FruitObject != null)
        {
            Destroy(_FruitObject);
            boilScoreHandler.points += 300;
            boilScoreHandler.totalBerriesProcessed++;
        }
        _FruitObject = Instantiate(fruits[Random.Range(0, fruits.Count-1)], _StartPos, Quaternion.identity);
        _FruitState = FruitStates.MOVINGIN;

    }

}
