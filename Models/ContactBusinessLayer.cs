using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lengkeng.Models
{
    public class ContactBusinessLayer
    {
        public string[] getTimeArr(string timestartend)
        {
            return timestartend.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }
        public string getTime(string timeDB)
        {
            string[] myarr = getTimeArr(timeDB);

            if (myarr.Count() == 2)
            {
                return "Mỗi ngày: Từ " + myarr[0] + " - " + myarr[1];
            }
            else if (myarr.Count() >= 4)
            {
                return "Thứ 2-6: " + myarr[0] + " - " + myarr[1] + ", Chủ Nhật: " + myarr[2] + " - " + myarr[3];
            }
            return null;
        }
    }
}