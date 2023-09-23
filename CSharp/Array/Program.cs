namespace Array

{
    internal class Program
    {
        // 2차원 배열
        static int[,] map = new int[6, 5] // 아이템이 6개 있고 각 아이템은 5개의 서브 아이템을 가짐. or 6개의 행과 5개의 열이 있음(메모리 상에서는 표 형식이 아니고 6개의 아이템이 일렬로 붙어 있음).
        {
                {0,0,0,0,1},
                {0,1,1,1,1},
                {0,0,0,1,1},
                {1,1,0,1,1},
                {1,1,0,1,1},
                {1,1,0,0,2} // 0: 길 / 1: 벽 / 2: 도착 지점 / 3: 플레이어
        };

        static int y, x;

        static void Main(string[] args)
        {
            #region 1차원 배열 및 반복문
            int[] arr = new int[5]; // 배열은 참조 타입임.
            arr[0] = 1; // []를 인덱서(인덱스에 접근하기 위한 연산자), 0을 인덱스(인덱서에서 몇 번째 인덱스에 접근할 것인지에 대한 입력)라고 함.
            arr[1] = 3;

            // 배열의 인덱스 접근: "배열 참조 주소 + 인덱스 * 자료형 크기"부터 자료형 크기(여기서는 int이므로 4byte)만큼 접근
            // 배열을 만들 때 초기값을 명시하지 않으면 전부 defalt 값으로 초기화됨.
            // 배열도 멤버 변수, 함수 등을 가지는 클래스 형태임.
            Console.WriteLine(arr[0]);
            Console.WriteLine(arr[1]);
            Console.WriteLine(arr[2]);
            Console.WriteLine(arr[3]);
            Console.WriteLine(arr[4]); // 배열의 인덱스 범위는 0부터 Array.Length - 1

            // while문: 조건이 참일 경우 반복 수행
            int index = 0;
            while (index < arr.Length)
            {
                Console.WriteLine(arr[index++]); // 후위 연산자를 활용하여, 대입한 후 index가 1만큼 늘어나게 함.
            }

            // do while문: 일단 한 번 실행한 후 조건이 참일 경우 반복 수행
            //do
            //{
            //   반복할 내용
            //} while (반복할 조건);

            // for문: 
            //for (반복 시작 전 실행할 내용; 반복할 조건; 반복 수행 완료 시마다 실행할 내용)
            //{
            //   반복할 내용
            //}

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

            // 배열 복사
            int[] arr2 = new int[8];
            System.Array.Copy(arr, arr2, arr.Length);

            // 배열 복사를 for문으로 구현
            for (int i = 0; i < arr.Length; i++)
            {
                arr2[i] = arr[i];
            }
            #endregion

            map[y, x] = 3;
            int goaly = 5;
            int goalx = 4;
            DrawMap();
            while (y != goaly || x != goalx)
            {
                Console.WriteLine("w, a, s, d 중 하나를 입력하세요.");
                string input = Console.ReadLine(); // 입력이 들어올때까지 기다리다가 입력값을 문자열로 반환
                input = input.ToUpper(); // 문자열을 대문자로 변환
                if (input == "W") MoveUp();
                else if (input == "S") MoveDown();
                else if (input == "D") MoveRight();
                else if (input == "A") MoveLeft();
                else Console.WriteLine("잘못된 입력입니다. w, a, s, d중 하나를 입력하세요.");
            }
            Console.WriteLine("보물찾기 끝! 게임 종료.");
        }

        static void DrawMap()
        {
            Console.WriteLine("--------------------");
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0)
                        Console.Write("□");
                    else if (map[i, j] == 1)
                        Console.Write("■");
                    else if (map[i, j] == 2)
                        Console.Write("☆");
                    else if (map[i, j] == 3)
                        Console.Write("▣");
                }
                Console.WriteLine();
            }
            Console.WriteLine("--------------------");
        }

        static void MoveRight()
        {
            // 맵 범위 초과 확인
            if (x >= map.GetLength(1) - 1)
            {
                Console.WriteLine("맵 바깥으로 나갈 수는 없다.");
                return;
            }

            // 벽 확인
            if (map[y, x + 1] == 1)
            {
                Console.WriteLine("벽으로 막혀있다.");
                return;
            }

            map[y, x] = 0;
            x++;
            map[y, x] = 3;
            DrawMap();
        }

        static void MoveLeft()
        {
            // 맵 범위 초과 확인
            if (x <= 0)
            {
                Console.WriteLine("맵 바깥으로 나갈 수는 없다.");
                return;
            }

            // 벽 확인
            if (map[y, x - 1] == 1)
            {
                Console.WriteLine("벽으로 막혀있다.");
                return;
            }

            map[y, x] = 0;
            x--;
            map[y, x] = 3;
            DrawMap();
        }

        static void MoveUp()
        {
            // 맵 범위 초과 확인
            if (y <= 0)
            {
                Console.WriteLine("맵 바깥으로 나갈 수는 없다.");
                return;
            }

            // 벽 확인
            if (map[y - 1, x] == 1)
            {
                Console.WriteLine("벽으로 막혀있다.");
                return;
            }

            map[y, x] = 0;
            y--;
            map[y, x] = 3;
            DrawMap();
        }

        static void MoveDown()
        {
            // 맵 범위 초과 확인
            if (y >= map.GetLength(0) - 1)
            {
                Console.WriteLine("맵 바깥으로 나갈 수는 없다.");
                return;
            }

            // 벽 확인
            if (map[y + 1, x] == 1)
            {
                Console.WriteLine("벽으로 막혀있다.");
                return;
            }

            map[y, x] = 0;
            y++;
            map[y, x] = 3;
            DrawMap();
        }
    }
}