namespace EnumType
{
    // enum: 사용자 정의 자료형(보통 열거형이라고 함)
    // 정수값에 대한 이름 목록을 작성할 수 있음.
    // 기본적으로는 uint 데이터와 같이 생김.
    public enum State
    {
        None, // State.None = 0
        Idle,
        Move,
        Jump,
        Fall,
        Attack = 20 // 값을 직접 집어넣을 수도 있음.
    }

    // 열거형으로; Bit Flags를 정의하는 방법
    public enum LayerMask
    {
        Default = 0 << 0, // 0: ...00000000
        Ground = 1 << 0,  // 1: ...00000001
        Player = 1 << 1,  // 2: ...00000010
        Enemy = 1 << 2,   // 4: ...00000100
    }

    class Player
    {
        // const 상수 키워드: 해당 변수를 상수로써 사용하겠다고 명시하는 키워드. 상수 취급이므로 런타임 중에 값을 대입할 수 없다.
        // public const int STATE_JUMP = 3;
        // 1: idle
        // 2: Move
        // 3: Jump
        // 4: Fall
        // public int state;
        public State state;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            // player.state = 3 // 매직넘버는 안 쓰는 게 좋음.
            // player.state = Player.STATE_JUMP;
            player.state = State.Jump;

            // Enum 클래스: 열거형 타입에 대한 편의기능들을 제공하는 클래스
            // Array 클래스: 배열에 대한 편의기능들을 제공하는 클래스
            // Type 클래스: 어떤 타입을 대표하는 정보를 가질 수 있는 클래스
            // 어떤 타입을 Type 클래스 타입으로 반환받고 싶으면 typeof라는 키워드를 쓸 수 있다.
            Array array = Enum.GetValues(typeof(State));

            // 일단은 in 뒤에 있는 것을 차례대로 순회하면서 현재 순회 중인 아이템을 반환하는 구문이라고 알고 있으면 됨.
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }

            // return: 현재 할당된 함수를 종료하고 메모리 해제
            // continue: 현재 라인에서 코드 흐름을 끊고 다시 현재 구문 처음으로 돌아가서 실행
            // break: 현재 구문을 탈출

            // switch-case 구문
            //switch (변수)
            //{
            //    case 값1:
            //        break;
            //    case 값2:
            //        {
            //            변수가 값1 또는 값2일 대 실행할 내용
            //                break;
            //        }
            //    default:
            //        break;

            switch (player.state)
            {
                case State.None:
                    break;
                case State.Idle:
                    {
                        // 플레이어가 idle일 때 실행할 내용
                    }
                    break;
                case State.Move:
                    break;
                case State.Jump:
                    break;
                case State.Fall:
                    break;
                case State.Attack:
                    break;
                default:
                    break;
            }

            string names = string.Empty;
            switch (names)
            {
                case "철수":
                    break;
                case "영희":
                    break;
                default:
                    break;
            }

            int colliderLayer = 3;
            int colliderMask = 1 << 1 | 1 << 2;
            if ((1 << colliderLayer & colliderMask) > 0)
            {

            }

            LayerMask mask = LayerMask.Player | LayerMask.Enemy;
            if ((1 << colliderLayer & (uint)mask) > 0)
            {

            }
        }
    }
}