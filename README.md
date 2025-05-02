# 스파르타코딩클럽 10기_19조 설민우 TextRPG 개인 프로젝트 입니다

# 스파르타 메타버스 만들기 과제 - DungeonBus with RogueLike

스파르타 코딩클럽 10기, 유니티 입문 개인 프로젝트 스파르타 메타버스 만들기 과제작업물입니다.

## 📷 스크린샷

![Main](https://github.com/user-attachments/assets/25fa942a-2b07-4b82-b491-630843dbf651)

## 🕹️ 기능
<details>
<summary><input type="checkbox" checked disabled> 1. (필수)캐릭터 이동 및 탐색 </summary>

![Move](https://github.com/user-attachments/assets/ea8d8f70-0d03-4c3a-8e1f-8bca2609c455)
  
![image](https://github.com/user-attachments/assets/c4de5105-adf8-4e1f-ba7a-4c055438d392)


```
void Update()
{
    switch (State)
    {
        case Define.State.Idle:
            UpdateIdle();
            break;
        case Define.State.Skill:
            UpdateSkill();
            break;

    }

    HandleAttackDelay();
}

private void FixedUpdate()
{
    switch (State)
    {
        case Define.State.Moving:
            UpdateMoving();
            break;
        case Define.State.Knockback:
            UpdateKnockBack();
            break;
    }

    // 점프 관련연산
    UpdateJump();
}
```
- 뉴 인풋 시스템을 이용하여 입력을 받았습니다.
- BaseController -> PlayerController를 이용하여 State로 입력을 받아 움직임을 내보냅니다.
- 점프는 실제 Player 최상위 객체가 아닌, 내부의 Sprite를 가진 게임 오브젝트를 위로 올리고 내리는, 가상의 중력을 이용한 점프로 구현했습니다.
- 방향키를 이용해 움직이고, 이때 쉬프트키를 누르면 제자리에서 방향만을 지정할 수 있습니다. 이 바라보는 방향은 빨간색 화살표로 표기됩니다.
- Z키를 누르면 해당 방향으로 공격을 발사합니다.
- C키를 누르면 점프합니다.

</details>

<details>
<summary><input type="checkbox" checked disabled> 2. (필수)맵 설계 및 상호작용 영역 </summary>
  
![Map](https://github.com/user-attachments/assets/85934a47-9b2f-463f-b250-102e99f1c30e)

  ```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour, ITimeTriggerable
{
    [SerializeField]
    private string _targetScene;
    [SerializeField]
    private float _waitTime = 1.0f;
    [SerializeField]
    private Transform _pivot;

    //같은 씬에서 이동할때
    public bool isMoveInSameScene =false;
    [SerializeField]
    private Transform targetPositon;
    [SerializeField]
    private GameObject targetPivot;
    CameraManager _camera;
    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RoomController controller = collision.GetComponent<RoomController>();
            controller.StartTriggerCountdown(this,_waitTime, _pivot.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RoomController controller = collision.GetComponent<RoomController>();
            controller.CancelTriggerCountdown();
        }
    }

    // 콜백으로 호출되는 메서드
    public  void OnTriggerTimeComplete()
    {
        if(!isMoveInSameScene)
            SceneManager.LoadScene(_targetScene);
        else
        {
            _camera.ChangePivot(targetPivot);
            LobbyScene.Instance.StartLogueGame(targetPositon);
        }
    }
}
```
- 기존의 원거리 무기를 이용해 상호작용 할 수 있는 투사체를 만들었습니다.
- 대화기능을 가진 NPC는 투사체에 맞으면 대사를 말합니다.
- 씬 전환을 Door 스크립트를 통해 구현했고 ProgressBar 기능을 추가하여 머리 위에 진행도를 표현 할 수 있도록 했습니다.

</details>

  
<details>
<summary><input type="checkbox" checked disabled> 3. (필수)미니 게임 실행</summary>
  
![Mini](https://github.com/user-attachments/assets/78835607-38bc-4f53-998b-9fde144006b4)

- 기존에 작업했던 플래피 버그 미니게임을 연결

</details>

<details>
<summary><input type="checkbox" checked disabled> 4. (필수)점수 시스템 & 5. (필수)게임 종료 및 복귀 </summary>

![Load](https://github.com/user-attachments/assets/9d4f1730-be93-45fb-aa33-45af7a122dde)


```
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private string _scoreName;
    [SerializeField]
    private TextMeshProUGUI _scoreTxt;
    private void Awake()
    {
        LobbyScene.OnScoreUpdate += UpdateScoreBoard;
    }

    public void UpdateScoreBoard()
    {
        _scoreTxt.text = PlayerPrefs.GetInt(_scoreName, 0).ToString();
    }
}


```
- playerprefs 를 이용해서 점수를 저장하고 불러오는 것으로 미니게임 점수를 저장하고, 이를 이벤트 패턴을 이용해서 점수 갱신을 진행 했습니다.


</details>


<details>
<summary><input type="checkbox" checked disabled> 6. (필수)카메라 추적 기능 </summary>

![Cam](https://github.com/user-attachments/assets/ebfae8c7-9462-43c0-a51f-250ad2af81e3)

```

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float lerpSpeed = 5f;
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private GameObject cameraPivot;
    private float  minX = 0;
    private float  maxX = 0;
    private float  minY = 0;
    private float  maxY = 0;

    public bool isFixedY = false;

    private void Awake()
    {
        ChangePivot();
    }
    public void ChangePivot(GameObject targetPivot = null)
    {
        if(targetPivot == null)
        {
            cameraPivot = LobbyScene.Instance.baseMap;
        }
        else
        {
            cameraPivot = targetPivot;
        }


        // 카메라 경계 지정 함수, 추후 별도의 함수로 만들어야함
        // 맵로드 이후 피벗 설정
        if (cameraPivot != null)
        {
            GameObject leftUpPivot = cameraPivot.transform.GetChild(0).gameObject;
            GameObject rightDownPivot = cameraPivot.transform.GetChild(1).gameObject;

            float camHalfHeight = Camera.main.orthographicSize;
            float camHalfWidth = camHalfHeight * ((float)Screen.width / Screen.height);


            minX = leftUpPivot.transform.position.x + camHalfWidth;
            maxX = rightDownPivot.transform.position.x - camHalfWidth;
            minY = rightDownPivot.transform.position.y + camHalfHeight;
            maxY = leftUpPivot.transform.position.y - camHalfHeight;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 destinationPosition = target.position + offset;
        Vector3 lerpPosition = Vector3.Lerp(transform.position, destinationPosition,
            lerpSpeed * Time.deltaTime);

        if (cameraPivot != null)
        {
            lerpPosition.x = Mathf.Clamp(lerpPosition.x, minX, maxX);
            lerpPosition.y = Mathf.Clamp(lerpPosition.y, minY, maxY);
        }

        if(!isFixedY)
            transform.position = lerpPosition;
        else
            transform.position = new Vector3(lerpPosition.x, transform.position.y, transform.position.z);
    }
}

```
- 카메라는 Lerp를 통해서 부드럽게 플레이어를 추적합니다.
- 이때 맵에 붙어있는 2개의 피벗을 통해 각자의 최대값, 최소값을 확인합니다.
- 해당 범위 내에서만 카메라가 이동하게 됩니다.

</details>

<details>
<summary><input type="checkbox" checked disabled> 1. (도전)추가 미니게임 &  3. (도전)리더보드 시스템 </summary>

![Rogue](https://github.com/user-attachments/assets/311b0f69-8849-4b3d-8e3e-8410ef6bd3ae)

![image](https://github.com/user-attachments/assets/1c6e18df-8e62-4c88-a7f2-a7174b646587)

- 기존의 탑다운 슈팅게임의 요소를 가져와 간단한 로그라이크 게임을 만들었습니다.
- 라운드가 시작되면 적이 나타나고 -> 적을 모두 처치하면 보상이 등장합니다. -> 다시 문을 통해서 나가면 다음 라운드가 시작됩니다.
- 최대 라운드를 최대 점수로 저장하여 UI 로 보여주게 됩니다.
- 보상은 체력증가, 투사체 증가, 공격력 증가, 이동속도 증가의 4가지 입니다.
- 또한 PlayerPrebs를 이용하여 리더보드 시스템을 만들었습니다.

</details>

<details>
<summary><input type="checkbox" checked disabled> 2. (도전)커스텀 캐릭터 및 저장 </summary>

![Change](https://github.com/user-attachments/assets/626bac43-ea04-4898-b348-f6d03fdad6d8)

```
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Save(string[] animatorAddresses, string savePath)
    {
        var dataList = new AnimatorSaveDataList();

        foreach (var addr in animatorAddresses)
            dataList.animators.Add(new AnimatorSaveData { animatorAddress = addr });

        string json = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(savePath, json);
    }

    public string[] Load(Animator[] targetAnimators, string savePath)
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("저장 파일이 없습니다.");
            return null;
        }

        string json = File.ReadAllText(savePath);
        var dataList = JsonUtility.FromJson<AnimatorSaveDataList>(json);

        if (dataList.animators.Count != targetAnimators.Length)
        {
            Debug.LogError("Animator 개수 불일치");
            return null;
        }

        for (int i = 0; i < dataList.animators.Count; i++)
        {
            string addr = dataList.animators[i].animatorAddress;
            var animator = targetAnimators[i];

            Addressables.LoadAssetAsync<RuntimeAnimatorController>(addr).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    animator.runtimeAnimatorController = handle.Result;
                else
                    Debug.LogError($"Animator {i} 로드 실패: {addr}");
            };
        }

        // 주소만 추출해서 반환
        return dataList.animators.Select(a => a.animatorAddress).ToArray();
    }
}


```

- 상호작용시 캐릭터의 런타임 애니메이터 컨트롤러를 교체하는 방식으로 구현했습니다
- 또한 이 변경을 Adressable 과 Json 을 이용해 저장했습니다.

</details>

<details>
<summary><input type="checkbox" checked disabled> 4. (도전)NPC와 대화 시스템 </summary>

![ezgif-2d6f5a23c610da](https://github.com/user-attachments/assets/7e8908fc-8b0f-4371-be9d-bd83a2b37fd6)

![image](https://github.com/user-attachments/assets/e04d6ac8-63bb-4e9c-b723-46e84d85893f)

```
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string key;
    [TextArea]
    public string text;
}

[CreateAssetMenu(menuName = "Data/NPCDialogue")]
public class DialogueData : ScriptableObject
{
    public string npcId;
    public List<DialogueEntry> data;
}


```
- 상호작용에 Scriptable을 이용하여 저장된 대사를 출력하도록 구성했습니다.

</details>

## 🛠️ 기술 스택

- C#
- .NET Core 3.1
- Newtonsoft.Json (데이터 직렬화/역직렬화)

## 🧙 사용법

1. 이 저장소를 클론하거나 다운로드합니다.
2. 빌드를 진행하여 실행합니다.
3. 방향키로 조작하고, 쉬프트로 멈춘 상태에서 방향만 조작할 수 있습니다.
4. C 키를 이용해 점프, Z키를 이용해 공격합니다.

## 🗂️ 프로젝트 구조
<details>
<summary><input type="checkbox" checked disabled> 펼쳐보기 </summary>

```
TextRpg/
├── Data/
│   ├── DataManager.cs
│   ├── DialogueData.cs
│   ├── PlayerData.cs
│
├── Database/
│   └── SlotDataManager.cs
│
├── GameLogic/
│   ├── Creature/
│   │   ├── Enemy/
│   │   │   ├── EnemyController.cs
│   │   │   └── EnemyStatHandler.cs
│   │   ├── Player/
│   │   │   ├── PlayerController.cs
│   │   │   ├── PlayerStatHandler.cs
│   │   │   └── RoomController.cs
│   │   ├── BaseController.cs
│   │   ├── ResourceController.cs
│   │   └── StatHandler.cs
│   │
│   ├── NPC/
│   │   ├── BaseNpc.cs
│   │   ├── ChangePlayerNpc.cs
│   │   ├── DialogueNpc.cs
│   │   ├── MiniGameNpc.cs
│   │   ├── NpcDialogueController.cs
│   │   └── RogueNpc.cs
│   │
│   ├── Weapon/
│   │   ├── InteractiveController.cs
│   │   ├── ProjectileController.cs
│   │   ├── RangeAttackController.cs
│   │   ├── RangeWeaponHandler.cs
│   │   └── WeaponHandler.cs
│
├── Manager/
│   ├── CameraManager.cs
│   └── RoundManager.cs
│
├── Map/
│   ├── Door.cs
│   ├── ITimeTriggerable.cs
│   ├── ScoreBoard.cs
│   ├── Result.cs
│   ├── ResultAttack.cs
│   ├── ResultHealth.cs
│   ├── ResultProjectileCount.cs
│   ├── ResultSpeed.cs
│   └── RogueDoor.cs
│
├── Scene/
│   ├── LobbyScene.cs
│   └── LogueGameScene.cs
│
├── MiniGame/
│   ├── Player/
│   │   └── Player.cs
│   ├── UI/
│   │   └── MiniGameUI.cs
│   ├── Wall/
│   │   ├── BgLooper.cs
│   │   └── Obstacle.cs
│   └── MiniGameScene.cs
│
├── Utils/
│   └── Define.cs

```
</details>


## 🙋 개발자 정보

- 이름: SulMinWoo
- 연락처 : sataka1853@naver.com
