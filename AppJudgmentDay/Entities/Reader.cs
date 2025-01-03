﻿using AppJudgmentDay.Interfaces;
using System.Windows.Data;

namespace AppJudgmentDay.Entities
{
    /// <summary>
    /// The person that uses the library
    /// </summary>
    public class Reader : IEntity
    {
        // Если смущает убери
        #region Fields

        public string Id;

        public string FirstName;

        public string LastName;

        public string MiddleName;

        public string Age;

        public string PhoneNumber;

        public string Email;
        #endregion

        public Reader(string orig)
        {
            InitializeFromString(orig);
        }

        // For us to be able to invoke the constructor
        public Reader() { } 

        public bool InitializeFromString(string inp)
        {
            var parts = inp.Split('#');
            if (parts.Length < 7 ) 
                return false;
            Id = parts[0];
            FirstName = parts[1];
            MiddleName = parts[2];
            LastName = parts[3];
            Age = parts[4];
            PhoneNumber = parts[5];
            Email = parts[6];
            return true;
        }

        public string Serialize()
        {
            return $"{Id}#{FirstName}#{MiddleName}#{LastName}#{Age}#{PhoneNumber}#{Email}";
        }
    }
}
