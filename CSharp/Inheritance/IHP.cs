using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // interface: 기능을 추상화하는 사용자 정의 자료형. 이름은 대문자 I + PascalCase
    internal interface IHP
    {
        float hpValue { get; }
        float hpMax { get; }
        float hpMin { get; }

        void RecoverHp(object subject, float amount);
        void DepleteHp(object subject, float amount);
    }
}
