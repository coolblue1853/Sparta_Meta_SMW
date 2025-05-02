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

```

```
- 

</details>

<details>
<summary><input type="checkbox" checked disabled> 4. (필수)점수 시스템 </summary>

```

```
- 


</details>


<details>
<summary><input type="checkbox" checked disabled> 5. (필수)게임 종료 및 복귀 </summary>

```

```
- 

</details>

<details>
<summary><input type="checkbox" checked disabled> 6. (필수)카메라 추적 기능 </summary>

```

```
- 

</details>

<details>
<summary><input type="checkbox" checked disabled> 1. (도전)추가 미니게임 </summary>

```

```
- 

</details>

<details>
<summary><input type="checkbox" checked disabled> 2. (도전)커스텀 캐릭터 </summary>

```

```
- 

</details>

<details>
<summary><input type="checkbox" checked disabled> 3. (도전)리더보드 시스템 </summary>

```

```
- 

</details>
<details>
<summary><input type="checkbox" checked disabled> 4. (도전)NPC와 대화 시스템 </summary>

```

```
- 

</details>

## 🛠️ 기술 스택

- C#
- .NET Core 3.1
- Newtonsoft.Json (데이터 직렬화/역직렬화)

## 🧙 사용법

1. 이 저장소를 클론하거나 다운로드합니다.
2. Visual Studio / Rider로 열고 실행합니다.
3. 콘솔에서 안내에 따라 게임을 진행합니다.
4. 플레이 도중 자동 저장됩니다. `player_save.json`, `inventory_save.json` 참고.

## 💾 저장 데이터

- 저장 경로: `bin/Debug/netcoreapp3.1/`
- `player_save.json`: 플레이어 정보
- `inventory_save.json`: 인벤토리 정보

## 🗂️ 프로젝트 구조
<details>
<summary><input type="checkbox" checked disabled> 펼쳐보기 </summary>

```
TextRpg/
├── Data/
│   ├── DungeonData.cs
│   ├── ItemData.cs
│   ├── JobData.cs
│   └── SceneData.cs
│
├── Database/
│   ├── Database.cs
│   └── DataLoader.cs
│
├── GameLogic/
│   ├── Dungeon.cs
│   ├── GameManager.cs
│   ├── Inventory.cs
│   ├── Item.cs
│   ├── Lobby.cs
│   ├── SaveManager.cs
│   └── Shop.cs
│
├── Json/
│   ├── dungeons.json
│   ├── items.json
│   ├── Jobs.json
│   └── sceneText.json
│
├── Player/
│   ├── Player.cs
│   └── Stats.cs
│
├── Utility/
│   ├── Define.cs
│   └── Utils.cs
```
</details>


## 🙋 개발자 정보

- 이름: SulMinWoo
- 연락처 : sataka1853@naver.com
