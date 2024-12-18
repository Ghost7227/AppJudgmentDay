using AppJudgmentDay.Interfaces;

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

        public string Year;

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
            // Здесь сделай парсер из строки по своему формату
            return false;
        }

        public string Serialize()
        {
            // Здесь сделай сериализацию в строку по своему формату
            return "";
        }
    }
}
