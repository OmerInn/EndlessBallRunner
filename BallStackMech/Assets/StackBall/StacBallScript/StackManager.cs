using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class StackManager : MonoBehaviour
{
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
            GameManager.GameManagerInstance.Balls.Add(other.transform);
        }
        if (other.CompareTag("add"))
        {
            var NoAdd = int.Parse(other.transform.GetChild(0).name); //çarpan nesnenin 1. çocuðu noAdd te depolanacak
            for (int i = 0; i < NoAdd; i++)
            {
                GameObject Ball = Instantiate(GameManager.GameManagerInstance.NewBall,GameManager.GameManagerInstance.Balls.ElementAt(GameManager.GameManagerInstance.Balls.Count - 1).position + new Vector3(0, 0, 0.5f), Quaternion.identity);
                GameManager.GameManagerInstance.Balls.Add(Ball.transform);
            }
            other.GetComponent<Collider>().enabled = false;
        }
        if (other.CompareTag("sub"))
        {
            var NoSub = int.Parse(other.transform.GetChild(0).name);

            if (GameManager.GameManagerInstance.Balls.Count > NoSub)
            {
                for (int i = 0; i < NoSub; i++)
                {
                    GameManager.GameManagerInstance.Balls.ElementAt(GameManager.GameManagerInstance.Balls.Count - 1).gameObject.SetActive(false);
                    GameManager.GameManagerInstance.Balls.RemoveAt(GameManager.GameManagerInstance.Balls.Count - 1);
                }
                Instantiate(GameManager.GameManagerInstance.Explosion, GameManager.GameManagerInstance.
                Balls.ElementAt(GameManager.GameManagerInstance.Balls.Count - 1).position, Quaternion.identity);
            }

            if (GameManager.GameManagerInstance.Balls.Count == 0)
            {
                GameManager.GameManagerInstance.StartTheGame = false;
            }
            other.GetComponent<Collider>().enabled = false;
        }
    }
}

