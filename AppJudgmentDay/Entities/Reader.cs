using AppJudgmentDay.Interfaces;
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
            FirstName = parts[0];
            MiddleName = parts[1];
            LastName = parts[2];
            Age = parts[3];
            PhoneNumber = parts[4];
            Email = parts[5];
            Id = parts[6];
            return true;
        }

        public string Serialize()
        {
            return $"{Id}#{FirstName}#{LastName}#{MiddleName}#{Age}#{PhoneNumber}#{Email}";
        }
    }
}
