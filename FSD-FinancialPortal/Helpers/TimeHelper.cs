﻿using FSD_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSD_FinancialPortal.Helpers
{
    public class TimeHelper
    {
        public static string TimeSinceNotification(Notification notification)
        {
            if (notification.Created.Date == DateTime.Today)
            {
                if (DateTimeOffset.Now.Subtract(notification.Created).TotalHours >= 1)
                {
                    var hoursSince = (int)DateTimeOffset.Now.Subtract(notification.Created).TotalHours;
                    return hoursSince.ToString() + " hours ago";
                }
                else
                {
                    var minutesSince = (int)DateTimeOffset.Now.Subtract(notification.Created).TotalMinutes;
                    return minutesSince.ToString() + " minutes ago";
                }
            }
            else if (notification.Created.Year == DateTimeOffset.Now.Year && notification.Created.DayOfYear == (DateTime.Today.DayOfYear - 1))
            {
                return "Yesterday";
            }
            else
            {
                var daysSince = DateTimeOffset.Now.Subtract(notification.Created).Days;
                if (daysSince == 1)
                {
                    return daysSince.ToString() + " day ago";
                }

                return daysSince.ToString() + " days ago";
            }
        }
    }
}