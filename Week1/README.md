# [유데미x스나이퍼팩토리] 10주 완성 프로젝트 캠프 - 유니티(Unity) 기초 학습 1주차

## 1주차 1회 9월 11일

프로젝트 캠프 첫 날이다. 오전엔 OT를 진행하고 점심을 먹은 후 오후엔 유니티와 게임 개발에 대한 설명과 함께 유니티 설치를 진행했다. 오늘은 인터넷이 계속 끊겨서 설치하는 것만 몇 시간 걸린 것 같다. 원래 유니티가 설치되어 있지만 유니티 수업을 들으면서 문제가 생기거나 함께 프로젝트를 진행하기 위해선 유니티 버전을 맞추는 것이 중요하다.

- 유니티
    
    [Unity 실시간 개발 플랫폼 | 3D, 2D, VR 및 AR 엔진](https://unity.com/kr)
    

2022.3.8 버전을 설치하고 기본 도형으로 간단한 캐릭터를 만들고 Material을 이용해 색을 입혀보는 수업을 진행했다.

![1-1](https://github.com/dailybear/UdemyxSniperfactory/assets/95425769/52a49cae-0cc6-433a-838e-7d6d610c3ed4)

---

# 1주차 2회 9월 15일

---

## 수업 요약

> 플래피 버드와 유사한 미니게임 제작
> 

## 1. 개념

### **조명의 종류**

- Point Light
- Spot Light

### 중력가속도

물리학에서 중력에 의해 운동하는 물체가 지니는 가속도

### 무게

물체의 질량에 중력가속도를 곱한 값. 중력이 작용하는 모든 강체에 적용되는 힘

### **프리팹**

게임 오브젝트를 생성, 설정 및 저장해 재사용 가능한 에셋으로 만든다.

<aside>
✨ **유니티는 물리 엔진이 자체적으로 존재하므로 따로 구현할 필요가 없다.**

</aside>

---

## 2. 주요 **코드**

- **기능**
    - 스페이스 바를 누르면 점프 한다.
    - 여러 종류의 장애물이 랜덤으로 생성된다.
    - 장애물과 부딪히면 게임이 종료된다.

- **추가 기능**
    - 게임 타이틀과 게임 오버 씬 제작
    - 장애물을 무사히 지나면 점수 증가
    - 장애물과 3번 부딪히면 게임 종료
    - 스카이박스로 배경 추가
    
- **Scripts**

```csharp
// Player Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb; 
    public float jumpPower = 5; 
    float go = 0.1f;
    TextMesh scoreOutput; // 점수
    TextMesh heart; // 남은 생명
    int score = 0;
    int h = 3;
    

    public float lowWarn = -4;
    public float jumpBoost = 2.5f; 

    void Start()
    {
        scoreOutput = GameObject.Find(name: "Score1").GetComponent<TextMesh>(); // "Score1" 게임오브젝트의 TextMesh 컴포넌트를 scoreOutput에 대입
        rb = gameObject.GetComponent<Rigidbody>();
        go = 0.5f;
        heart = GameObject.Find(name: "Heart").GetComponent<TextMesh>(); // "Heart" 게임 오브젝트를 heart에 대입(남은 생명)

    }

    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * go, 0); // 해당 오브젝트의 trasnform값의 y 값을 Time.deltaTime * go 값 만큼 증가
        if (Input.GetButtonDown("Jump")) // Jump 버튼, 보통 space
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
            if(transform.position.y < lowWarn)
            {
                rb.AddForce(Vector3.up * jumpBoost , ForceMode.Impulse); // 점프 부스트를 받는 지점에서 Velocity 대신 AddForce 사용해봄
            }
        }

    }
    private void OnCollisionEnter(Collision collision)  // 충돌체크 (물리적 현상 O)
    {
        h -= 1; // 장애물과 충돌 시 생명이 하나씩 줄어듬
        heart.text = "생명 : " + h; // 텍스트 출력
        if ( h < 1 ) // h가 0개 이하 시 gameOver씬 호출
        {
            SceneManager.LoadScene("gameOver");
            
        }
    }

    private void OnTriggerEnter(Collider other) // 충돌체크 (물리 현상 X)
    {
        if (other.gameObject.tag == "Coin") // 태그가 Coin인 오브젝트와 충돌 시
        {
            Destroy(other.gameObject); // 부딪힌 게임 오브젝트를 지운다.
            addScore(10); // addScore() 호출. 점수를 10점 씩 올림
        }

    }

    // 점수 더하기
    public void addScore(int s)
    {
        score += s;
        scoreOutput.text = "점수 : " + score;
    }
}
```

```csharp
// Wall 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float speed = -5;
    Player player;
    void Start()
    {
        player = GameObject.Find(name: "Player").GetComponent<Player>(); // player라는 변수를 만들어 "Player"라는 게임 오브젝트에서 Player라는 컴포넌트를 가져와 변수에 넣는다.

    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);  // x값을 speed * time.deltaTime만큼 이동

        if (transform.position.x < -10) // transform.position이 -10 미만이면
        {
            Destroy(gameObject); // 게임 오브젝트 삭제
        }
    }

}
```

```csharp
// Spawner
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] wallPrefab; // 장애물 프리팹들을 게임오브젝트 배열에 추가
    public GameObject dropPrefab; // 떨어지는 장애물
    public float interval = 2.5f; // 일정 시간마다
    public float range = 3;
    float term; // 장애물 생성 텀

    void Start()
    {
        term = interval; // 시작부터 벽이 하나 나오기 위해
    }

    void Update()
    {
        term += Time.deltaTime; term에 Time.deltaTime만큼 계속 더함
        if (term >= interval) // term의 값이 interval을 넘어가면 
        {
            Vector3 pos = transform.position; // pos에 transform.position 값 대입
            pos.y += Random.Range(-range, range);
            int wallType = Random.Range(0, wallPrefab.Length);
            Instantiate(wallPrefab[wallType], pos, transform.rotation); // 실시간으로 wallPrefab을 만들어준다. 
            if (Random.Range(0, 5) == 0) // 50%의 확률로 떨어지는 장애물 생성
            {
                Instantiate(dropPrefab);
            }
            
            term -= interval; 
        }
    }
}
```

```csharp
// 씬 전환
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    Text AnyKeyDown;
    
    //Player player;
    void Start()
    {
        AnyKeyDown = GetComponent<Text>();
    }

    void Update()
    {
        LoadGame(); // LoadGame() 호출
    }

    public IEnumerator BlinkText() // 텍스트가 깜빡이는 표현
    {
        while (true)
        {
            AnyKeyDown.text = "";
            yield return new WaitForSeconds(0.5f);
            AnyKeyDown.text = "Press Any Key";
            yield return new WaitForSeconds(0.3f);
        }
    }

        public void LoadGame()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("game"); // 아무 키나 눌면 game 씬 호출

        }
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("Start"); // gameOver 씬에서 restart 버튼 클릭 시start 씬 호출(재시작 시)

    }

```

![1-2-1](https://github.com/dailybear/UdemyxSniperfactory/assets/95425769/8138baf9-e744-4429-960a-4f320a0ea2a8)

---

## 3. 추가 공부

### Instantiate

- Instantiate(wallPrefab[wallType], pos, transform.rotation);
- Destroy(gameObject);

이미 만들어진 게임 오브젝트를 필요할 때마다 실시간으로 만들어낸다.

유니티 자체적으로 생성과 파괴를 반복하는 것은 메모리를 많이 잡아먹음.

가급적 오브젝트 풀링을 사용하는 것이 좋다.

### **Vector3**

x, y, z 3개의 값을 가진다. 

### 네임 스페이스

- 씬을 로드할 때 필요하다.

using.UnityEngine.SceneManagement;

UnityEngine.SceneMangaement.SceneManager.LoadScene(SceneManager

### Range

사용 가능한 범위를 설정한다.

Random.Range() 함수를 사용해 Range의 범위를 랜덤으로 설정해 장애물이 랜덤으로 나오게 했다.

### new 연산자 : 재사용 할 수 있는 객체 생성 코드

Vector3에 새로운 값을 넣기 위해 사용함

transform.positon += new Vector(0, step * time.deltaTime, 0);

→

transform.position = transform.position + new Vector(0, step * time.deltaTime, 0)

현재 위치(transform.positon)에 new Vector의 값만큼을 더해 위치를 업데이트

### Time.deltaTime

Time.deltaTime은 마지막 프레임이 완료된 후 경과한 시간을 초 단위로 반환한다. 이 값은 게임이나 앱이 실행될 때 초당 프레임(FPS) 속도에 따라 다르다.

프레임에 상관 없이 오브젝트는 동일한 속도로 움직이게 된다.

프레임 차이의 격차를 줄일 수 있다.

### World와 Local

Transform.positon은 월드에서 오브젝트의 **절대 좌표값**이다.

Transform.LocalPosition은 부모 오브젝트에 대한 자식 오젝트의 **상대적 위치**이다.

---

## 9월 15일 1주차 3회

## 수업요약

> 유니티짱 패키지를 활용해 미니게임에 유니티짱 모델 + 애니메이터 적용, 미니프로젝트 제작
> 
- 유니티짱
    
    [유니티 짱! 공식 홈페이지 (unity-chan.com)](https://unity-chan.com/)
    

## 유니티짱(UnityChan)

![1-2-2](https://github.com/dailybear/UdemyxSniperfactory/assets/95425769/cdecb941-9b8d-4c06-ad65-9d043087b759)

유니티짱 패키지에는 애니메이터, 다양한 모션, 보이스 예제 씬 등이 포함되어 있다.

- 유니티짱 애니메이터 추가

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseChanger : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }
            else
                anim.SetTrigger("Next");
        }
    }
}
```

- 기존 플레이어인 Cube에서 유니티짱으로 플레이어를 변경했다.

![1-3](https://github.com/dailybear/UdemyxSniperfactory/assets/95425769/db79651c-cd12-41e8-9fe8-9c813c36ad15)

---

본 후기는 유데미-스나이퍼팩토리 10주 완성 프로젝트캠프 학습 일지 후기로 작성 되었습니다.

#프로젝트캠프 #프로젝트캠프후기 #유데미 #udemy #스나이퍼팩토리 #웅진씽크빅 #인사이드아웃 #IT개발캠프 #개발자부트캠프 #unity #유니티 #게임개발 #메타버스