using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lengkeng.DataAccessLayer;

namespace lengkeng.Models
{
    public class AccountBusinessLayer
    {
        DBContext db = new DBContext();

        public UserStatus validUser(UserDetails userDetails)
        {
            try
            {
                Account acc = db.Accounts.Where(a => a.Username == userDetails.Username).FirstOrDefault();
                if (acc != null)
                {
                    if (acc.Userstatus == UserStatus.Admin)
                    {
                        return UserStatus.Admin;
                    }
                    if (acc.Userstatus == UserStatus.Quanly)
                    {
                        return UserStatus.Quanly;
                    }
                    if (acc.Userstatus == UserStatus.Thungan)
                    {
                        return UserStatus.Thungan;
                    }
                    if (acc.Userstatus == UserStatus.Boiban)
                    {
                        return UserStatus.Boiban;
                    }
                    return UserStatus.Khach;
                }
                return UserStatus.Khach;
            }
            catch (Exception)
            {
                return UserStatus.Khach;
            }
        }
    }
}