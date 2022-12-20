using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Individuals
    {
        public string FirstName
        { get; set; }
        public string LastName
        { get; set; }
        public string PhoneNumber
        { get; set; }
        public string EmpId
        { get; set; }
        public string Email
        { get; set; }
        public string AccName
        { get; set; }
        public int Applicant
        { get; set; }

        // defineing statics
        static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        static string nums = "0123456789";
        // dictionaries to track previous records
        static IDictionary<string, int> emailCheck = new Dictionary<string, int>();
        static IDictionary<string, bool> accCheck = new Dictionary<string, bool>();

        public static void clearStaticChecks()
        {
            emailCheck = new Dictionary<string, int>();
            accCheck = new Dictionary<string, bool>();
        }
        

        // Initialize 
        public Individuals(string rawRecord)
        {
            string[] data = rawRecord.Split(',');
            FirstName = data[0];
            LastName = data[1];
            PhoneNumber = data[2];
            EmpId = data[3];
            Applicant = Convert.ToInt16(data[4]);

            Email = (data[5] == "")? genEmail() : data[5];
            AccName = (data[6] == "")? genAccName() : data[6];

        }
        // generates email 
        private String genEmail()
        {
            string mail;
            int num = 0;

            if (emailCheck.ContainsKey($"{FirstName}{LastName}"))
            {
                num = emailCheck[$"{FirstName}{LastName}"];
                emailCheck[$"{FirstName}{LastName}"] += 1;
            }
            else
            {
          
                emailCheck[$"{FirstName}{LastName}"] = 1;
            }
            mail = $"{FirstName.ToLower()}.{LastName.ToLower()}{(num == 0 ? "" : $"{num}")}@{(Applicant == 1 ? "" : "student.")}sl.on.ca";
            return mail;
        }

        // generates account name
        private string genAccName()
        {
            string possibileAccName = genRandChar();
            while (accCheck.ContainsKey(possibileAccName))
            {
                possibileAccName = genRandChar();
            }
            accCheck[possibileAccName] = true;
            return possibileAccName;
        }

        private string genRandChar()
        {
            var strChar = new char[5];
            var random = new Random();
            for (int i = 0; i < strChar.Length; i++)
            {
                if (i == 0 || i == strChar.Length - 1)
                {
                    strChar[i] = nums[random.Next(nums.Length)];
                }
                else
                {
                    strChar[i] = chars[random.Next(chars.Length)];
                }
            }

            return new String(strChar);
        }


        public override string ToString()
        {
            string str = $"{FirstName},{LastName},{PhoneNumber},{EmpId},{Applicant},{Email},{AccName}";
            return str;
        }
    }
}
