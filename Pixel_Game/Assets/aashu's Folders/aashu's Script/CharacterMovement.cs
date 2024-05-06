using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterMovement : MonoBehaviour
{
    public Animator anim;
    public bool isWatering = false;
    public GameObject rose;
    public GameObject crop;
    private float speed = 1.5f;
    public bool isAxe = false;
    public bool isPlantation = false;
    public bool isCrop = false;

    public bool activeProcess = false;
    public Text startProcess;
    public GameObject text;

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized * Time.deltaTime;
        transform.Translate(movement * speed);

        anim.SetFloat("Vertical", verticalInput);
        anim.SetFloat("Horizontal", horizontalInput);

        if (activeProcess)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {

                anim.SetTrigger("PickAxe");
                isAxe = true;
                StartCoroutine(Crop());
            }

        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isAxe)
            {
                anim.SetTrigger("Plantation");
                isPlantation = true;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPlantation)
            {
                isWatering = true;
                anim.SetBool("Watering", isWatering);
                StartCoroutine(RoseActive());

            }


        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isWatering = false;
            anim.SetBool("Watering", isWatering);
            
        }
    }

    IEnumerator Crop()
    {

        yield return new WaitForSeconds(0.5f);
        crop.SetActive(true);
        isCrop = true;

    }

    IEnumerator RoseActive()
    {

        yield return new WaitForSeconds(0.5f);
        rose.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crop"))
        {
            activeProcess = true;
            startProcess.text = "Start Process";
            text.SetActive(true);
            Debug.Log("Active Process: " + activeProcess);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Crop"))
        {
            activeProcess = false;
            text.SetActive(false);
            Debug.Log("Active Process: " + activeProcess);
        }
    }
}
