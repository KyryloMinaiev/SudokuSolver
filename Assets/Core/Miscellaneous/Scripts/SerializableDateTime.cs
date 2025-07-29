using System;
using UnityEngine;

namespace Content.Features.UIModule.Scripts
{
    [Serializable]
    public struct SerializableDateTime
    {
        [SerializeField] private int _year;
        [SerializeField] private int _month;
        [SerializeField] private int _day;
        [SerializeField] private int _hour;
        [SerializeField] private int _minute;
        [SerializeField] private int _second;
        [SerializeField] private int _millisecond;

        public SerializableDateTime(DateTime dateTime)
        {
            _year = dateTime.Year;
            _month = dateTime.Month;
            _day = dateTime.Day;
            _hour = dateTime.Hour;
            _minute = dateTime.Minute;
            _second = dateTime.Second;
            _millisecond = dateTime.Millisecond;
        }
        
        public static implicit operator SerializableDateTime(DateTime dateTime) => new(dateTime);

        public static implicit operator DateTime(SerializableDateTime dateTime) =>
            new(dateTime._year, dateTime._month, dateTime._day, dateTime._hour, dateTime._minute, dateTime._second,
                dateTime._millisecond);
    }
}