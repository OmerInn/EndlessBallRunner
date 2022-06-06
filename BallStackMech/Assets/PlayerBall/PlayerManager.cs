using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform ball;
    private Vector3 startMousePos, startBallPos;
    private bool moveTheBall, gameState, DetectNewPath;
    [Range(0f, 1f)] public float maxSpeed;
    [Range(0f, 1f)] public float camSpeed;
    [Range(0f, 50f)] public float pathSpeed;
    [Range(0f, 1000f)] public float ballRotateSpeed;
    private float velocity, camVelocity_x, camVelocity_y;
    private Camera mainCam;
    public  List<Transform> path;

    private Rigidbody rb;
    private Collider _collider;
    private Renderer BallRenderer;
    private MeshFilter BallFilter;

    public ParticleSystem CollideParticle;
    public ParticleSystem airEffect;
    public ParticleSystem Dust;
    public ParticleSystem BallTrail;
    Vector3 ballStartPos;

    public int RoadFinishSize;
    public static PlayerManager playerManager;

    private void Awake()
    {
        playerManager = this;
    }
    private void Start()
    {
        ball = transform;
        ballStartPos = transform.position;
        mainCam = Camera.main;
        rb=GetComponent<Rigidbody>();
        _collider=GetComponent<Collider>();
        BallRenderer = GetComponent<Renderer>();
        BallFilter = GetComponent<MeshFilter>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && MenuManager.MenuManagerInstance.GameState)
        {
            moveTheBall = true;
            Plane newPlan = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (newPlan.Raycast(ray, out var distance))
            {
                startMousePos = ray.GetPoint(distance);
                startBallPos = ball.position;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moveTheBall = false;
        }

        if (moveTheBall)
        {
            Plane newPlan = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (newPlan.Raycast(ray, out var distance))
            {
                Vector3 mouseNewPos = ray.GetPoint(distance);
                Vector3 MouseNewPos = mouseNewPos - startMousePos;
                Vector3 DesireBallPos = MouseNewPos + startBallPos;

                DesireBallPos.x = Mathf.Clamp(DesireBallPos.x, -1.5f, 1.5f);

                ball.position = new Vector3(Mathf.SmoothDamp(ball.position.x, DesireBallPos.x, ref velocity, maxSpeed)
                    , ball.position.y, ball.position.z);

            }
        }

        if (MenuManager.MenuManagerInstance.GameState)
        {
            for (int i = 0; i < path.Count; i++)
            {
                var pathPosition = path[i].position;
                path[i].position = Vector3.MoveTowards(pathPosition, new Vector3(pathPosition.x, pathPosition.y,RoadFinishSize), Time.deltaTime * pathSpeed);
            }

            // ball.GetChild(1).Rotate(Vector3.right * ballRotateSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        var cameraNewPos = mainCam.transform.position;

        if (rb.isKinematic)
            mainCam.transform.position = new Vector3(Mathf.SmoothDamp(cameraNewPos.x, ball.transform.position.x, ref camVelocity_x, camSpeed)
                , Mathf.SmoothDamp(cameraNewPos.y, ball.transform.position.y + 3f, ref camVelocity_y, camSpeed), cameraNewPos.z);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            MenuManager.MenuManagerInstance.GameState = false;
            MenuManager.MenuManagerInstance.menuElement[2].SetActive(true);
            

        }

        switch (other.tag)
        {
            case "red":
                other.gameObject.SetActive(false);
                BallRenderer.materials = other.GetComponent<Renderer>().materials;
                BallFilter.mesh = other.GetComponent<MeshFilter>().mesh;
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);

                var NewParticle = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                NewParticle.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                Destroy(NewParticle.gameObject, 1f);
                var BallTrailColor = BallTrail.trails;
                BallTrailColor.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            case "yellow":
                other.gameObject.SetActive(false);
                BallRenderer.materials = other.GetComponent<Renderer>().materials;
                BallFilter.mesh = other.GetComponent<MeshFilter>().mesh;
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);

                var NewParticle1 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                NewParticle1.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                Destroy(NewParticle1.gameObject, 1f);
                var BallTrailColor_1 = BallTrail.trails;
                BallTrailColor_1.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            case "blue":
                other.gameObject.SetActive(false);
                BallRenderer.materials = other.GetComponent<Renderer>().materials;
                BallFilter.mesh = other.GetComponent<MeshFilter>().mesh;
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);

                var NewParticle2 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                NewParticle2.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                Destroy(NewParticle2.gameObject, 1f);
                var BallTrailColor_2 = BallTrail.trails;
                BallTrailColor_2.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            case "green":
                other.gameObject.SetActive(false);
                BallRenderer.materials = other.GetComponent<Renderer>().materials;
                BallFilter.mesh = other.GetComponent<MeshFilter>().mesh;
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                var NewParticle3 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                NewParticle3.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                Destroy(NewParticle3.gameObject, 1f);
                var BallTrailColor_3 = BallTrail.trails;
                BallTrailColor_3.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            case "football":
                other.gameObject.SetActive(false);
                BallRenderer.materials = other.GetComponent<Renderer>().materials;
                BallFilter.mesh = other.GetComponent<MeshFilter>().mesh;
                this.gameObject.transform.localScale = new Vector3(2, 2, 2);
               // var NewParticle4 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
               // NewParticle4.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                var BallTrailColor_4 = BallTrail.trails;
                BallTrailColor_4.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            case "basketball":
                other.gameObject.SetActive(false);
                BallRenderer.materials = other.GetComponent<Renderer>().materials;
                BallFilter.mesh = other.GetComponent<MeshFilter>().mesh;
                this.gameObject.transform.localScale = new Vector3(2, 2, 2);
               // var NewParticle5 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                //NewParticle5.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                var BallTrailColor_5 = BallTrail.trails;
                BallTrailColor_5.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            case "Tennis":
                other.gameObject.SetActive(false);
                BallRenderer.materials = other.GetComponent<Renderer>().materials;
                BallFilter.mesh = other.GetComponent<MeshFilter>().mesh;
                this.gameObject.transform.localScale = new Vector3(3, 3, 3);
               // var NewParticle6 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                //NewParticle6.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                var BallTrailColor_6 = BallTrail.trails;
                BallTrailColor_6.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
        }
        if (other.gameObject.name.Contains("ColorBall"))
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
            MenuManager.MenuManagerInstance.menuElement[1].GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("score").ToString();
        }

        if (other.CompareTag("path"))
        {
            rb.isKinematic = _collider.isTrigger = true;
            transform.position =new Vector3(transform.position.x, ballStartPos.y,transform.position.z);
            pathSpeed = 30;
            var airEffectMain = airEffect.main;
            airEffectMain.simulationSpeed = 4f;
            BallTrail.Play();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("path"))
        {
            rb.isKinematic = _collider.isTrigger = false;
            rb.velocity = new Vector3(0f, 8f, 0f);
            pathSpeed = pathSpeed * 1.5f;

            var airEffectMain = airEffect.main;
            airEffectMain.simulationSpeed = 10f;
            BallTrail.Stop();

        }
    }

}

