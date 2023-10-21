using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        //_hpBar = GetComponent<RectTransform>().GetChild(0).GetComponent<Slider>();
        //GetComponentInChildren<Slider>();
        //// 이 컴포넌트를 가지는 GameObject가 가지는 컴포넌트들 중에 해당 타입을 찾아서 반환함.
        //GetComponents<RectTransform>();

        _hpBar.minValue = 0.0f;
        _hpBar.maxValue = _player.hpMax;
        _hpBar.value = _player.hp;
        //_player.onHpChanged += RefreshHPBar;
        // 인라인 함수 : 함수 오버헤드를 줄이기 위해 기능 구현부를 해당 라인에 직접 삽입하는 함수 
        // C# 에서의 인라인 함수 구현 : 익명함수 (람다식) 으로 구현함.
        // 람다식 : 컴파일러가 판단할 수 있는 코드를 생략하고 이름을 생략한 함수식

        // ex ) RefreshHPBar
        // 1. 인라인함수는 접근제한자가 의미 없으므로 private 생략
        // 2. 구독하려는 대리자의 형식이 float 파라미터 1개와 void 반환이므로 void 및 float 타입 생략
        // 3. 인라인이므로 이름으로 함수검색할 일 없으므로 이름 생략
        // 4. 구현부 한줄이면 그다음은 반드시 함수 리턴이 일어나야하므로 함수범위 지정 필요없으므로 중괄호 생략
        // 5. 람다식 명시를 위해 => 추가
        _player.onHpChanged += (value) => _hpBar.value = value;
    }

    private void RefreshHPBar(float value)
    {
        _hpBar.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
