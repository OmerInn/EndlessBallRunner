using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool MoveByTouch, StartTheGame;
    private Vector3 _mouseStartPos, PlayerStartPos;
    [SerializeField] private float RoadSpeed, SwipeSpeed, Distance;
    [SerializeField] private GameObject Road;
    public static GameManager GameManagerInstance;
    private Camera mainCam;

    [Header("Stack Settings")]
    public List<Transform> Balls = new List<Transform>();
    public GameObject NewBall;

    public ParticleSystem Explosion;

    void Start()
    {
        GameManagerInstance = this;
        mainCam = Camera.main;
        Balls.Add(gameObject.transform);
    }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartTheGame = MoveByTouch = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                MoveByTouch = false;
            }

            if (MoveByTouch)
            {
                var plane = new Plane(Vector3.up, 0f);

                float distance;

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (plane.Raycast(ray, out distance))
                {
                    Vector3 mousePos = ray.GetPoint(distance);
                    Vector3 desirePso = mousePos - _mouseStartPos;
                    Vector3 move = PlayerStartPos + desirePso;

                    move.x = Mathf.Clamp(move.x, -2.2f, 2.2f);
                    move.z = -7f;

                    var player = transform.position;

                    player = new Vector3(Mathf.Lerp(player.x, move.x, Time.deltaTime * (SwipeSpeed + 10f)), player.y, player.z);

                    transform.position = player;
                }
            }

            if (StartTheGame)
                Road.transform.Translate(Vector3.forward * (RoadSpeed * -1 * Time.deltaTime));
        if (Balls.Count>1)
        {
            for (int i = 1; i < Balls.Count; i++)
            {
                var FirstBall = Balls.ElementAt(i - 1);
                var SectBall = Balls.ElementAt(i);

                var DesireDistance = Vector3.Distance(FirstBall.position, SectBall.position);

                if (DesireDistance <= Distance)
                {
                    SectBall.position = new Vector3(Mathf.Lerp(SectBall.position.x, FirstBall.position.x, SwipeSpeed * Time.deltaTime)
                    , SectBall.position.y, Mathf.Lerp(SectBall.position.z, FirstBall.position.z + 0.5f, SwipeSpeed * Time.deltaTime));
                } //+0.5f toplarýn arasýndaki boþluklar 
            }
        }

        }
        private void LateUpdate()
        {
            if (StartTheGame)
                mainCam.transform.position = new Vector3(Mathf.Lerp(mainCam.transform.position.x, transform.position.x, (SwipeSpeed - 5f) * Time.deltaTime),
                        mainCam.transform.position.y, mainCam.transform.position.z);
        }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blue"))
        {
            other.transform.parent = null;
            other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
            other.gameObject.AddComponent<StackManager>();
            other.gameObject.GetComponent<Collider>().isTrigger = true;
            other.tag = gameObject.tag;
            other.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
            Balls.Add(other.transform);
        }
        if (other.CompareTag("add"))
        {
            var NoAdd = int.Parse(other.transform.GetChild(0).name); //çarpan nesnenin 1. çocuðu noAdd te depolanacak
            for (int i = 0; i < NoAdd; i++)
            {
                GameObject Ball = Instantiate(NewBall, Balls.ElementAt(Balls.Count - 1).position + new Vector3(0, 0, 0.5f), Quaternion.identity);
                Balls.Add(Ball.transform);
            }
        }
        if (other.CompareTag("sub") && Balls.Count > 0)
        {
            Instantiate(Explosion, Balls.ElementAt(Balls.Count - 1).position, Quaternion.identity);
            Balls.ElementAt(Balls.Count - 1).gameObject.SetActive(false);
            Balls.RemoveAt(Balls.Count - 1);
        }

        if (Balls.Count == 0)
        {
            StartTheGame = false;
        }
    }
}
