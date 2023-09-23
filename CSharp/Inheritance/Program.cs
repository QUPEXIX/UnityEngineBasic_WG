namespace Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SwordMan swordMan = new SwordMan();

            // 공변성(Covariant): 하위 타입 객체를 기반 타입으로 참조할 수 있는 성질. 객체가 할당될 때, 기반 타입의 데이터로부터 차례대로 할당을 하기 때문에 가능함.
            PlayableCharacter swordMan1 = new SwordMan();
            PlayableCharacter wizard1 = new Wizard();
            PlayableCharacter intermidiateWizard1 = new IntermidiateWizard();

            swordMan.Move();
            swordMan1.Move();
            wizard1.Move();
            intermidiateWizard1.Move();

            // object 타입: 모든 타입의 기반 타입
            // object 타입 변수에 어떤 데이터를 대입하면 heap 메모리 영역에 object 타입 객체를 생성하고, 데이터의 타입 참조 테이블 인덱스와 데이터를 씀. 이 과정을 Boxing이라 함.
            object int1 = 3; // object 타입 객체 생성 -> 맨 앞에 int 타입 작성 -> 3 작성
            object str = "zㅣ존검사";
            object wizard = new Wizard();

            // int a = int1; // 오류 발생
            // unboxing: object 객체에서 원래 데이터를 읽어오는 과정
            int b = (int)int1;
        }
    }
}