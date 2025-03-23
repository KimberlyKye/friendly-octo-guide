using Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ValueObjects
{
    /// <summary>
    /// Временные рамки курса/домашнего задания. 
    /// </summary>
    public class Duration : IValueObject
    {
        /// <summary>
        /// Время начала.
        /// </summary>
        private DateOnly _startDate;
        /// <summary>
        /// Время окончания.
        /// </summary>
        private DateOnly _endDate;

        /// <summary>
        ///  Конструктор.
        /// </summary>
        /// <param name="startDate">Время начала.</param>
        /// <param name="endDate">Время окончания.</param>
        /// <exception cref="ArgumentException"></exception>
        public Duration(DateOnly startDate, DateOnly endDate)
        {            
            // Проверка дата начала не может быть больше/равна даты окончания
            if (startDate >= endDate)
            {
                throw new ArgumentException("Дата начала не может быть больше/равна даты окончания", nameof(startDate));
            }
            _startDate = startDate;
            _endDate = endDate;
        }

        /// <summary>
        /// Геттеры.
        /// </summary>
        public DateOnly StartDate => _startDate;
        public DateOnly EndDate => _endDate;

        /// <summary>
        /// Метод получения полного имени.
        /// </summary>
        /// <returns></returns>
        public (DateOnly startDate, DateOnly endDate) GetDuration()
        {
            return (_startDate, _endDate);
        }
    }
}
