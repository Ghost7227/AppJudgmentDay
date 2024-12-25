using AppJudgmentDay.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AppJudgmentDay.Entities
{

    public class Worker : IEntity
    {
        #region Fields

        public string Id;

        public string FirstName;

        public string LastName;

        public string MiddleName;

        public string Age;

        public string PhoneNumber;

        public string Email;

        public string Post;
        #endregion

        public Worker(string orig) 
        { 
            InitializeFromString(orig);
        }

        public Worker() { }

        public bool InitializeFromString(string inp)
        {
            var parts = inp.Split('#');
            if (parts.Length < 8)
                return false;
            Id = parts[0];
            FirstName = parts[1];
            MiddleName = parts[2];
            LastName = parts[3];
            Age = parts[4];
            PhoneNumber = parts[5];
            Email = parts[6];
            Post = parts[7];
            return true;
        }

        public string Serialize()
        {
            return $"{Id}#{FirstName}#{MiddleName}#{LastName}#{Age}#{PhoneNumber}#{Email}#{Post}";
        }
    }
    
}
