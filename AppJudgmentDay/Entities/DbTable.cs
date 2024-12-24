using AppJudgmentDay.Interfaces;
using System.Collections.Generic;
using System;
using System.IO;

namespace AppJudgmentDay.Entities
{
    /// <summary>
    /// A storage of given IEntities
    /// </summary>
    public class DbTable<T> where T : IEntity 
    {
        /// <summary>
        /// The list of entities stored in the table
        /// </summary>
        private List<T> Data;

        /// <summary>
        /// The location of the file data stored in
        /// </summary>
        private string FilePath = null;

        /// <param name="filePath">The location of the file data stored in</param>
        public DbTable(string filePath) 
        {
            // Если файла нет, то создаём его
            if(!File.Exists(filePath))
                File.Create(filePath).Close();
            FilePath = filePath;
            LoadFromFile();
        }

        /// <summary>
        /// Reads the file and writes the contents to the table
        /// Считывает файл и записывает его содержимое в таблицу
        /// </summary>
        public void LoadFromFile()
        {
            Data = new List<T>();
            foreach (var item in File.ReadAllLines(FilePath))
            {
                try
                {
                    var newEntity = Activator.CreateInstance<T>();
                    if(newEntity.InitializeFromString(item))
                        Data.Add(newEntity);
                }
                catch
                { 
                    // TODO: Add error handling logic here
                }
            }
        }

        /// <summary>
        /// Overrides the file with the existing data
        /// Заменяет файл существующими данными
        /// </summary>
        public void SaveAll()
        {
            if (FilePath is null)
                throw new Exception("The file location is not set");
            // Создаём временный файл сохранения
            var swapName = $"{FilePath}.swp";
            File.Create($"{FilePath}.swp").Close();
            using (var stream = new StreamWriter(swapName))
            {
                foreach(var entity in Data)
                    stream.WriteLine(entity.Serialize());
                stream.Close();
            }
            // Меняем файл
            if(!File.Exists(FilePath))
            {
                File.Move(swapName, FilePath);
                return;
            }
            File.Move(FilePath, $"{FilePath}.tmp");
            File.Move(swapName, FilePath);
            File.Delete($"{FilePath}.tmp");
        }

        /// <summary>
        /// Adds an entity to a table
        /// Добавляет объект в таблицу
        /// </summary>
        /// <param name="entity"></param>
        public void Append(T entity)
        {
            Data.Add(entity);
            // Добавляем запись в файл
            using (var stream = new StreamWriter(FilePath, true))
            {
                stream.WriteLine(entity.Serialize());
                stream.Close();
            }
        }

        /// <summary>
        /// Returns all known entities
        /// Возвращает все известные объекты
        /// </summary>
        public IEnumerable<T> GetAll() 
        { 
            return Data;
        }

        /// <summary>
        /// Modifies the given entity in the RAM. This function does not save the result. WARNING: Uses .Equals() function for search
        /// Изменяет заданный объект в оперативной памяти. Эта функция не сохраняет результат. ВНИМАНИЕ: для поиска используется функция .Equals()
        /// </summary>
        public void Modify(T oldEntity, T updatedEntity)
        {
            var temp = Data.FindIndex(x => x.Equals(oldEntity));
            // Nothing found
            if (temp == -1)
                return;
            Data[temp] = updatedEntity;
        }

        /// <summary>
        /// Deletes the data from the set. Rewrites the save file
        /// Удаляет данные из набора. Перезаписывает сохраненный файл
        /// </summary>
        public void Delete(T entity)
        {
            if(Data.Remove(entity))
                SaveAll();
        }

        public static implicit operator DbTable<T>(Reader v)
        {
            throw new NotImplementedException();
        }
    }
}
