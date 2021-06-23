using System;
using System.Globalization;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class DateTimeManager : MonoBehaviour
    {
        [SerializeField] private Text date;
        [SerializeField] private Text time;

        private CultureInfo cultureInfo;
        private DateTimeFormatInfo dateTimeFormat;
        private int currentDay;
        
        private void Start()
        {
            // Get Indonesian culture
            cultureInfo = new CultureInfo("id-ID");
            dateTimeFormat = cultureInfo.DateTimeFormat;
            
            // Get current date
            currentDay = (int) DateTime.Now.DayOfWeek;
            
            // Update date text
            string dayName = dateTimeFormat.DayNames[currentDay];
            date.text = $"{dayName}, {DateTime.Now.Day:00}/{DateTime.Now.Month:00}/{DateTime.Now.Year}";
        }

        private void Update()
        {
            // Update time text
            time.text = $"{DateTime.Now.Hour:00}:{DateTime.Now.Minute:00}.{DateTime.Now.Second:00}";
            
            // If currentDay is still the same day as now, don't update date text
            if (currentDay == (int) DateTime.Now.DayOfWeek) return;
            
            // Update date text if change day only
            string dayName = dateTimeFormat.DayNames[currentDay];
            date.text = $"{dayName}, {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
        }
    }
}
