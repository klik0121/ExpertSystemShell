﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diggins.Jigsaw;

namespace ExpertSystemShell
{
    /// <summary>
    /// Класс расширений.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Проверяет, удовлетворяет ли заданная строка правилу.
        /// </summary>
        /// <param name="rule">Правило.</param>
        /// <param name="input">Строка для проверки.</param>
        /// <returns>Возвращает <c>true</c>, если данная строка удовлетворяет правилу,
        /// иначе - <c>false</c>.</returns>
        public static bool ExactMatch(this Rule rule, string input)
        {
            //данный метод устраняет недостаток стандартного метода Rule.Match
            //метод Rule.Match может найти совпадение в части строки, но при этом
            //проигнорировать оставшуюся часть строки
            if (!rule.Match(input)) return false; //если правило не подходит возвращаем false
            try
            {
                //пытаемся распарсить ввод
                Node n = rule.Parse(input)[0];
                //если распаршенный текст совпадает с вводом, возвращаем true
                return n.Text.Length == input.Length;
            }
            catch
            {
                //при возникновении исключительной ситуации возвращаем false
                return false;
            }
        }
        public static T Convert<T>(this object obj)
        {
            if (obj == null)
                return default(T);
            return (T)System.Convert.ChangeType(obj, typeof(T));
        }

        public static dynamic Convert(this object obj, Type type)
        {
            return System.Convert.ChangeType(obj, type);
        }

        public static T[] Convert<T>(this dynamic[] arr)
        {
            T[] result = new T[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                result[i] = (T)arr[i];
            return result;
        }
    }
}
