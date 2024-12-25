using AppJudgmentDay.Interfaces;
using System.Windows.Controls;

namespace AppJudgmentDay.Entities
{
    public class Book : IEntity
    {
        #region Fields
        public string Author;
        public string Title;
        public string Janre;
        public string Produser;
        public string Isbn;
        public string Idbook;
        #endregion

        public Book() { }

        public Book(string orig) 
        {
            InitializeFromString(orig);
        }

        public bool InitializeFromString(string inp)
        {
            var parts = inp.Split('#');
            if (parts.Length < 7)
                return false;
            Idbook = parts[0];
            Isbn = parts[1];
            Author = parts[2];
            Title = parts[3];
            Janre = parts[4];
            Produser = parts[5];
            return true;
        }

        public string Serialize()
        {
            return $"{Idbook}#{Isbn}#{Author}#{Title}#{Janre}#{Produser}";
        }
    }
}
