using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRelationships.Models;

namespace EFRelationships.Services;

public static class GetUserService
{
    private static User? _user;

    public static User GetUser()
    {
        return _user!;
    }

    public static void SetUser(User newUser)
    {
        _user = newUser;
    }
}
