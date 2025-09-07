using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    private GameObject playerStage;
    private GameObject playerBackground;
    private GameObject playerBGM;
    private GameObject enemyStage;
    private GameObject enemyBackground;
    private GameObject enemyBGM;

    public GameObject currentStage;
    private GameObject currentBackground;
    private GameObject currentBGM;

    private Vector3 lineFirstPos;
    private Vector3 lineLastPos;
    private void Awake()
    {

    }

    private void Start()
    {

    }

    public void ChageStage(playerType playerType) // 스테이지 변경
    {
        if (playerType == playerType.Red)
        {
            lineFirstPos = new Vector3(0, 0, 0);
            lineLastPos = new Vector3(3000f, 0, 0);
            playerStage.SetActive(true);
            StartCoroutine(ChangeStageObject(playerType.Red, playerStage));
        }
        else
        {
            lineFirstPos = new Vector3(3000f, 0, 0);
            lineLastPos = new Vector3(0, 0, 0);
            enemyStage.SetActive(true);
            StartCoroutine(ChangeStageObject(playerType.Blue, enemyStage));
        }

        
    }

    IEnumerator ChangeStageObject(playerType playerType, GameObject stage) //시간에 따라 스테이지가 점차 변경
    {
        Renderer renderer = stage.GetComponent<Renderer>();
        Material stageMaterial = renderer.materials[0];
        Renderer Currentrenderer = currentStage.GetComponent<Renderer>();
        Material currentStageMaterial = Currentrenderer.materials[0];
        Vector3 pos;
        float changeTime = 1f;
        float time = 0;
        if(playerType == playerType.Blue)
        {
            stageMaterial.SetFloat("_ClipType", 1); //선의 왼쪽부분을 렌더링
            currentStageMaterial.SetFloat("_ClipType", 0); //선의 오른쪽부분을 렌더링
        }
        else
        {
            stageMaterial.SetFloat("_ClipType", 0);
            currentStageMaterial.SetFloat("_ClipType", 1);
        }
        while (time < changeTime)
        {
            pos = Vector3.Lerp(lineFirstPos, lineLastPos, time / changeTime);
            time += Time.unscaledDeltaTime;
            stageMaterial.SetFloat("_ClipX", pos.x); //선이동
            currentStageMaterial.SetFloat("_ClipX", pos.x);
            yield return null;
        }
        currentStage.SetActive(false);
        currentStage = stage;
    }


    public void AddBackground(playerType playerType) // 배경 변경
    {
        if (playerType == playerType.Red)
        {
            lineFirstPos = new Vector3(0, 0, 0);
            lineLastPos = new Vector3(3000f, 0, 0);
            playerBackground.SetActive(true);
            StartCoroutine(ChangeStageBackgroundObject(playerType.Red, playerBackground));
        }
        else
        {
            lineFirstPos = new Vector3(3000f, 0, 0);
            lineLastPos = new Vector3(0, 0, 0);
            enemyBackground.SetActive(true);
            StartCoroutine(ChangeStageBackgroundObject(playerType.Blue, enemyBackground));
        }

    }

    IEnumerator ChangeStageBackgroundObject(playerType playerType, GameObject background) //시간에 따라 배경이 점차 변경
    {
        Renderer[] renderers = background.GetComponentsInChildren<Renderer>();
        List<Material> materials = new List<Material>();
        for(int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] != null)
            {
                Material backgroundMaterial = renderers[i].materials[0];
                materials.Add(backgroundMaterial);
            }

        }

        Vector3 pos;
        float changeTime = 1f;
        float time = 0;
        if (playerType == playerType.Blue)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_ClipType", 1);
            }

        }
        else
        {
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_ClipType", 0);
            }
        }
        while (time < changeTime)
        {
            pos = Vector3.Lerp(lineFirstPos, lineLastPos, time / changeTime);
            time += Time.unscaledDeltaTime;
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_ClipX", pos.x);
            }
            yield return null;
        }
    }

    public void MusicOn(playerType playerType) //Bgm을 킨다
    {
        if (playerType == playerType.Red)
        {
            playerBGM.gameObject.SetActive(true);
            AudioSource audioSource = playerBGM.GetComponentInChildren<AudioSource>();
            if(audioSource != null)
            {
                audioSource.Play();
                audioSource.volume = 0.3f;
            }

        }
        else
        {
            enemyBGM.gameObject.SetActive(true);
            AudioSource audioSource = enemyBGM.GetComponentInChildren<AudioSource>();
            audioSource.Play();
            if (audioSource != null)
            {
                audioSource.Play();
                audioSource.volume = 0.3f;
            }
        }
    }

    public void GetPlayerItem(GameObject stage, GameObject background, GameObject bgM) //플레이어 설정을 받아옴
    {
        playerStage = Instantiate(stage);
        playerStage.SetActive(false);
        playerBackground = Instantiate(background);
        playerBackground.SetActive(false);
        playerBGM = Instantiate(bgM);
        playerBGM.SetActive(false);
    }

    public void GetEnemyItem(GameObject stage, GameObject background, GameObject bgM) //적 설정을 받아옴
    {
        enemyStage = Instantiate(stage);
        enemyStage.SetActive(false);
        enemyBackground = Instantiate(background);
        enemyBackground.SetActive(false);
        enemyBGM = Instantiate(bgM);
        enemyBGM.SetActive(false);
    }

}
