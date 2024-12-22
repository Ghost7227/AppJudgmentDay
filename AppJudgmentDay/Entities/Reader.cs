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
            if (parts.Length < 6 ) 
                return false;
            FirstName = parts[0];
            LastName = parts[1];
            MiddleName = parts[2];
            Age = parts[3];
            PhoneNumber = parts[4];
            Email = parts[5];
            return true;
        }

        public string Serialize()
        {
            return $"{FirstName}#{LastName}#{MiddleName}#{Age}#{PhoneNumber}#{Email}";
        }
    }
}
