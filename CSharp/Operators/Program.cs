using System;

namespace Operators // Operators와 같은 것을 식별문자라고 함. 게임 id와 같은 개념이며, using Operators;와 같은 형식으로 namespace를 갖다 쓸 수 있음.
{
    internal class Program // Operator라는 식별문자를 통해서만 Program에 접근이 가능함.
    {
        static void Main(string[] args)
        {
            int a = 14;
            int b = 6;
            int c = 0;

            // 산술 연산자
            c = a + b; Console.WriteLine(c); // a + b 연산 후 c 메모리(변수)에다 결과값(데이터)를 쓰겠다는 의미
            c = a - b; Console.WriteLine(c);
            c = a * b; Console.WriteLine(c);
            c = a / b; Console.WriteLine(c); // int끼리 나눴을 때는 몫만 반환됨.
            c = a % b; Console.WriteLine(c); // 나머지 연산

            // 복합 대입 연산자
            c += a; Console.WriteLine(c); // c = c + a;와 같은 의미
            c -= a; Console.WriteLine(c);
            c *= a; Console.WriteLine(c);
            c /= a; Console.WriteLine(c);
            c %= a; Console.WriteLine(c);

            // 증감 연산자
            ++c; Console.WriteLine(c); // 전위 연산자. c += 1;과 같은 의미임. 연산 내용은 c = c + 1이고, 반환값도 c + 1임.
            --c; Console.WriteLine(c);
            c++; Console.WriteLine(c); // 후위 연산자. c += 1;과 같은 의미임. 연산 내용은 c = c + 1이지만, 반환값은 c임.
            c--; Console.WriteLine(c);

            // 관계 연산자(비교 연산자)
            // 두 피연산자의 관계를 비교해서 결과가 참인지 거짓인지(bool 타입) 반환
            bool result;
            result = a == b; Console.WriteLine(result); // a와 b가 같으면 true, 다르면 false
            result = a != b; Console.WriteLine(result); // a와 b가 다르면 true, 같으면 false
            result = a > b; Console.WriteLine(result);
            result = a < b; Console.WriteLine(result);
            result = a >= b; Console.WriteLine(result);
            result = a <= b; Console.WriteLine(result);

            // 논리 연산자
            // 논리형의 피연산자에 대해서만 연산 수행
            bool A = true;
            bool B = false;
            result = A | B; Console.WriteLine(result); // or: A와 B  중에 하나라도 true면 true, 아니면 false
            result = A & B; Console.WriteLine(result); // and: A와 B 둘 다 true면 true, 아니면 false
            result = A ^ B; Console.WriteLine(result); // xor: A와 B 중에 하나만 true면 true, 아니면 false.
            result = !A; Console.WriteLine(result); // not: A가 true면 false, false면 true
            result = A == false; // not과 동일, !가 잘 안 보이기 때문에 사용

            // 조건부 논리 연산자
            result = A || B; // Conditional or: A가 true일 경우 논리 연산을 수행하지 않고 true 반환, 아니면 A | B를 수행
            result = A && B; // Conditional and: A가 false일 경우 논리 연산을 수행하지 않고 false 반환, 아니면 A & B를 수행

            // 비트 연산자
            // 정수형의 피연산자에 대해서만 수행
            int d = 0;
            d = a | b; Console.WriteLine(d); // or: 같은 비트에서 하나라도 1이면 1, 아니면 0
            // a == 14 == 2^3 + 2^2 + 2^1 == ...00001110
            // b == 6 == 2^2 + 2^1        == ...00000110
            // d                          == ...00001110 == 14
            d = a & b; Console.WriteLine(d); // and: 같은 비트에서 둘 다 1이면 1, 아니면 0
            d = a ^ b; Console.WriteLine(d); // xor: 같은 비트에서 A와 B 중에 하나만 1이면 1, 아니면 false
            d = ~a; Console.WriteLine(d); // not: 비트가 1이면 0, 0이면 1
            d = a << 2; Console.WriteLine(d); // shift -left: 비트를 n자리만큼 왼쪽으로 밂.
            // a = ...00001110
            // d = ...00111000 = 56
            d = a >> 2; Console.WriteLine(d); // shift -right: 비트를 n자리만큼 오른쪽으로 밂.
        }

        // 전위 연산자를 함수로 표현한 것.
        int BeforePP(ref int variable) // ref 키워드: 파라미터를 참조 타입으로 쓰겠다는 키워드. 즉, 인자로 넣어줄 수 있는 것은 주소를 가지고 있는 변수 같은 메모리 영역이고 상수는 넣을 수 없음.
        {
            variable += 1;
            return variable;
        }

        // 후위 연산자를 함수로 표현한 것.
        int  AfterPP(ref int variable)
        {
            int origin = variable;
            variable += 1;
            return origin;
        }
    }
}