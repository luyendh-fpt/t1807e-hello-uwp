using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T1807EHelloUWP.Entity;

namespace T1807EHelloUWP.Service
{
    interface MemberService
    {
        String Login(String username, String password);

        Member Register(Member member);

        Member GetInformation(String token);
    }
}
