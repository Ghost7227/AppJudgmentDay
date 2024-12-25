using AppJudgmentDay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJudgmentDay
{
    /// <summary>
    /// Возможно не лучший способ, но для наших целей пойдёт
    /// </summary>
    public static class DB
    {
        /// <summary>
        /// A table that contains all the known readers
        /// </summary>
        public static DbTable<Reader> Readers = new DbTable<Reader>("DataBaseUsers.txt");
        public static DbTable<Book> Books = new DbTable<Book>("DataBaseBooks.txt");
        public static DbTable<Worker> Workers = new DbTable<Worker>("DataBaseWorkers.txt");
    }
}
