using AppJudgmentDay.Interfaces;

namespace AppJudgmentDay.Entities
{
    public class Book : IEntity
    {
        // Если смущает убери
        #region Fields

        // TODO: Добавить поля книги

        #endregion

        public Book() { }

        public Book(string orig) 
        {
            InitializeFromString(orig);
        }

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
