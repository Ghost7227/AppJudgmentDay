using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJudgmentDay.Interfaces
{
    public interface IEntity
    {
        // ! СПОРНОЕ РЕШЕНИЕ, ВОЗМОЖНО НАДО СДЕЛАТЬ КОНСТРУКТОРОМ

        /// <summary>
        /// Parses the string and fills the fields. Returns true if successful
        /// </summary>
        /// <param name="inp"> String to deserialize </param>
        bool InitializeFromString(string inp);

        /// <summary>
        /// Returns the string deserializable into the object
        /// </summary>
        string Serialize();
    }
}
