using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    internal class Wizard : PlayableCharacter
    {
        // override: 재정의 키워드. 기반 타입의 멤버를 재정의 할 때 사용하는 키워드.
        public override void Move()
        {
            Console.WriteLine("Move");
        }

        public void Fireball()
        {
            Console.WriteLine("Fireball");
        }
    }
}