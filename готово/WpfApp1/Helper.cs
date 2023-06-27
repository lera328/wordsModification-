using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Helper
    {
        public string Valid(string word1, string word2)
        {
            if (word1==""||word2=="")
            {
                return "Поля не заполнены";
            }

            if (word1.Length!=word2.Length)
            {
                return "Слова не могут быть разной длины!";
            }

            string pattern="[а-яА-Я]+$";
            
            if (!Regex.IsMatch(word1,pattern) || !Regex.IsMatch(word2, pattern))
            {
                return "Ошибка ввода: слова должны содержать только буквы русского алфавита";
            }
            if (word1.Contains(" ") || word2.Contains(" ")) return "В каждом поле должно содержаться только одно слово";
            return null;
        }

        private List<string> GetCondidates(List<string> distionary, string word)
        {
            int len = word.Length;
            List<string> condidates = new List<string>();
            for(int i = 0; i < distionary.Count; i++)
            {
                if(distionary.ElementAt(i).Length== len)
                {
                    condidates.Add(distionary.ElementAt(i));
                }
            }
            return condidates;
        }
        // Функция для проверки, можно ли получить новое слово из старого, меняя одну букву
        private bool CanTransform(string word1, string word2)
        {
            int count = 0;

            for (int i = 0; i < word1.Length; i++) //слова должны одной буквой отличаться!!
            {
                if (word1[i] != word2[i])
                {
                    count++;
                }
            }

            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public List<string> Find(string startWord, string endWord, List<string> words) //начальное слово, конечное слово и словарь
        {
            
            words = GetCondidates(words, startWord);
            var visited = new List<string>();// Создаем лист посещенных слов

            
            List<List<string>> list = new List<List<string>>();// Создаем лист листов ...

            
            list.Add(new List<string> { startWord });// Добавляем в лист лист, содержащий только начальное слово

            
            while (list.Count > 0) // Пока в листе что-то есть
            {
                // Получаем лист1 из листа
                var path = list.ElementAt(0);

                // Удаляем лист1 из листа
                list.Remove(path);

                // Получаем последнее слово в лист1
                string word = path.ElementAt(path.Count - 1);

                // Если это конечное слово, возвращаем список слов
                if (word == endWord)
                    return path;

                // Если этого слова еще нет в списке посещенных
                if (!visited.Contains(word))
                {
                    // Добавляем его в список посещенных 
                    visited.Add(word);

                    // перебираем слова в листе words (в нашем словаре)
                    foreach (var nextWord in words)
                    {
                        // Если есть подходящее нам слово и оно не содержится в лист1
                        if (CanTransform(word, nextWord) && !path.Contains(nextWord))
                        {
                            // Создаем новый лист2, копируя все элементы из старого списка, добавляем туда найденное слово
                            var newPath = new List<string>(path);
                            newPath.Add(nextWord);

                            // Добавляем новый лист2 в основной лист
                            list.Add(newPath);
                        }
                    }
                }
            }
            // Если цепочка не найдена, возвращаем null
            return null;
        }
    }
}
